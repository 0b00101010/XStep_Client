// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextHolderEditor.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Editor.Editors
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Slash.Unity.DataBind.Core.Data;
    using Slash.Unity.DataBind.Core.Presentation;
    using Slash.Unity.DataBind.Core.Utils;
    using Slash.Unity.DataBind.Editor.Utils;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    ///     Custom editor for <see cref="ContextHolder" />.
    /// </summary>
    [CustomEditor(typeof(ContextHolder))]
    public class ContextHolderEditor : Editor
    {
        private const int MaxLevel = 5;

        private readonly Dictionary<string, bool> foldoutDictionary =
            new Dictionary<string, bool>();

        private readonly Dictionary<DataDictionary, object> newKeys = new Dictionary<DataDictionary, object>();

        private Rect invokeEventPopupRect;

        /// <summary>
        ///     Unity callback.
        /// </summary>
        public override void OnInspectorGUI()
        {
            var contextHolder = this.target as ContextHolder;
            if (contextHolder == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                var context = contextHolder.Context;

                // Use data class if a wrapper is used.
                var notifyPropertyChangedDataContext = context as NotifyPropertyChangedDataContext;
                if (notifyPropertyChangedDataContext != null)
                {
                    context = notifyPropertyChangedDataContext.DataObject;
                }

                if (context != null)
                {
                    var contextType = context.GetType().ToString();

                    EditorGUILayout.LabelField("Context", contextType);

                    // Reflect data in context.
                    this.DrawObjectData(context);

                    EditorUtility.SetDirty(contextHolder);
                }
                else
                {
                    if (contextHolder.ContextType != null && GUILayout.Button("Create context"))
                    {
                        contextHolder.Context = Activator.CreateInstance(contextHolder.ContextType);
                    }
                }
            }
            else
            {
                base.OnInspectorGUI();
            }
        }

        private void DrawObjectData(object obj)
        {
            if (obj == null)
            {
                return;
            }

            this.DrawObjectData(obj, 1);
        }

        private void DrawObjectData(object obj, int level)
        {
            if (obj == null)
            {
                return;
            }

            var prevIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = level;

            var memberInfos = ContextTypeCache.GetMemberInfos(obj.GetType());
            foreach (var memberInfo in memberInfos)
            {
                if (memberInfo.Property != null)
                {
                    var propertySetMethod = memberInfo.Property.GetSetMethod();
                    if (memberInfo.Property.CanWrite && propertySetMethod != null && propertySetMethod.IsPublic)
                    {
                        var memberValue = memberInfo.Property.GetValue(obj, null);
                        var newMemberValue = this.DrawMemberData(memberInfo.Name,
                            memberInfo.Property.PropertyType, memberValue, level);
                        if (!Equals(newMemberValue, memberValue))
                        {
                            memberInfo.Property.SetValue(obj, newMemberValue, null);
                        }
                    }
                }
                else if (memberInfo.Method != null)
                {
                    this.DrawObjectMethod(obj, memberInfo.Method);
                }
                else if (memberInfo.Field != null)
                {
                    if (memberInfo.Field.IsPublic)
                    {
                        var memberValue = memberInfo.Field.GetValue(obj);
                        var newMemberValue = this.DrawMemberData(memberInfo.Name,
                            memberInfo.Field.FieldType, memberValue, level);
                        if (!Equals(newMemberValue, memberValue))
                        {
                            memberInfo.Field.SetValue(obj, newMemberValue);
                        }
                    }
                }
            }

            EditorGUI.indentLevel = prevIndentLevel;
        }

        private object DrawMemberData(string memberName, Type memberType, object memberValue, int level)
        {
            if (level < MaxLevel)
            {
                var context = memberValue as Context;
                if (context != null)
                {
                    bool foldout;
                    this.foldoutDictionary.TryGetValue(level + memberName, out foldout);
                    foldout = this.foldoutDictionary[level + memberName] = EditorGUILayout.Foldout(foldout, memberName);
                    if (foldout)
                    {
                        this.DrawObjectData(context, level + 1);
                    }

                    return context;
                }

                var dictionary = memberValue as DataDictionary;
                if (dictionary != null)
                {
                    bool foldout;
                    this.foldoutDictionary.TryGetValue(level + memberName, out foldout);
                    foldout = this.foldoutDictionary[level + memberName] = EditorGUILayout.Foldout(foldout, memberName);
                    if (foldout)
                    {
                        this.DrawDictionaryData(dictionary, level + 1);
                    }

                    return dictionary;
                }

                var enumerable = memberValue as IEnumerable;
                if (enumerable != null && memberType.IsGenericType)
                {
                    var itemType = ReflectionUtils.GetEnumerableItemType(memberType);

                    bool foldout;
                    this.foldoutDictionary.TryGetValue(level + memberName, out foldout);
                    foldout = this.foldoutDictionary[level + memberName] = EditorGUILayout.Foldout(foldout, memberName);
                    if (foldout)
                    {
                        this.DrawEnumerableData(enumerable, memberType, itemType, level + 1);
                    }

                    return enumerable;
                }
            }

            // Draw data trigger.
            var dataTrigger = memberValue as DataTrigger;
            if (dataTrigger != null)
            {
                InspectorUtils.DrawDataTrigger(memberName, dataTrigger);
                return dataTrigger;
            }

            return InspectorUtils.DrawValueField(memberName, memberType, memberValue,
                (name, type, value) => this.DrawCustomTypeData(name, type, value, level));
        }

        private void DrawEnumerableData(IEnumerable enumerable, Type enumerableType, Type itemType, int level)
        {
            var isWriteable = enumerableType.GetInterfaces()
                .Any(x => x.IsGenericType &&
                          x.GetGenericTypeDefinition() == typeof(ICollection<>));

            var prevIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = level;

            var index = 0;
            List<object> itemsToRemove = null;
            foreach (var item in enumerable)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Item " + index);

                if (isWriteable && GUILayout.Button("Remove"))
                {
                    if (itemsToRemove == null)
                    {
                        itemsToRemove = new List<object>();
                    }

                    itemsToRemove.Add(item);
                }

                EditorGUILayout.EndHorizontal();
                this.DrawMemberData("Item " + index, itemType, item, level);
                ++index;
            }

            if (itemsToRemove != null)
            {
                var removeMethod = enumerableType.GetMethods()
                    .FirstOrDefault(m => m.Name == "Remove" && m.GetParameters().Length == 1);
                if (removeMethod != null)
                {
                    foreach (var itemToRemove in itemsToRemove)
                    {
                        removeMethod.Invoke(enumerable, new[] {itemToRemove});
                    }
                }
            }

            if (isWriteable && GUILayout.Button("New Item"))
            {
                var addMethod = enumerableType.GetMethods()
                    .FirstOrDefault(m => m.Name == "Add" && m.GetParameters().Length == 1);
                if (addMethod != null)
                {
                    var newItem = Activator.CreateInstance(itemType);
                    addMethod.Invoke(enumerable, new[] {newItem});
                }
            }

            EditorGUI.indentLevel = prevIndentLevel;
        }

        private void DrawDictionaryData(DataDictionary dataDictionary, int level)
        {
            var prevIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = level;

            Dictionary<object, object> changedValues = null;
            foreach (var key in dataDictionary.Keys)
            {
                var value = dataDictionary[key];
                var newValue = this.DrawMemberData("Item " + key, dataDictionary.ValueType, value, level);
                if (!Equals(value, newValue))
                {
                    if (changedValues == null)
                    {
                        changedValues = new Dictionary<object, object>();
                    }

                    changedValues[key] = newValue;
                }
            }

            if (changedValues != null)
            {
                foreach (var key in changedValues.Keys)
                {
                    dataDictionary[key] = changedValues[key];
                }
            }

            GUILayout.BeginHorizontal();

            object newKey;
            this.newKeys.TryGetValue(dataDictionary, out newKey);
            var keyType = dataDictionary.KeyType;
            const string NewKeyLabel = "New:";
            if (keyType == typeof(string))
            {
                this.newKeys[dataDictionary] = newKey = EditorGUILayout.TextField(NewKeyLabel, (string) newKey);
            }
            else if (TypeInfoUtils.IsEnum(keyType))
            {
                if (newKey == null)
                {
                    newKey = Enum.GetValues(keyType).GetValue(0);
                }

                this.newKeys[dataDictionary] = newKey = EditorGUILayout.EnumPopup(NewKeyLabel, (Enum) newKey);
            }

            if (GUILayout.Button("+") && newKey != null)
            {
                var valueType = dataDictionary.ValueType;
                dataDictionary.Add(newKey, valueType.IsValueType ? Activator.CreateInstance(valueType) : null);
            }

            GUILayout.EndHorizontal();

            EditorGUI.indentLevel = prevIndentLevel;
        }

        private void DrawObjectMethod(object context, MethodInfo method)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(ObjectNames.NicifyVariableName(method.Name));

            if (GUILayout.Button("Invoke"))
            {
                var parameterInfos = method.GetParameters();
                if (parameterInfos.Length > 0)
                {
                    var invokeEventPopup = new InvokeEventPopup(parameterInfos,
                        parameters => method.Invoke(context, parameters));
                    PopupWindow.Show(this.invokeEventPopupRect, invokeEventPopup);
                }
                else
                {
                    method.Invoke(context, null);
                }
            }

            if (Event.current.type == EventType.Repaint)
            {
                // Cover button with popup.
                var buttonRect = GUILayoutUtility.GetLastRect();
                this.invokeEventPopupRect = buttonRect;
                this.invokeEventPopupRect.position = new Vector2(buttonRect.position.x,
                    buttonRect.position.y - buttonRect.size.y);
            }

            EditorGUILayout.EndHorizontal();
        }

        private object DrawCustomTypeData(string memberName, Type memberType, object memberValue, int level)
        {
            if (memberValue == null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(memberName, "null");
                var canCreateInstance = memberType.GetConstructor(Type.EmptyTypes) != null;
                if (canCreateInstance && GUILayout.Button("Create"))
                {
                    memberValue = Activator.CreateInstance(memberType);
                }
                EditorGUILayout.EndHorizontal();

                return memberValue;
            }
            
            bool foldout;
            this.foldoutDictionary.TryGetValue(level + memberName, out foldout);
            foldout = this.foldoutDictionary[level + memberName] = EditorGUILayout.Foldout(foldout, memberName);
            if (foldout)
            {
                this.DrawObjectData(memberValue, level + 1);
            }

            return memberValue;
        }
    }
}