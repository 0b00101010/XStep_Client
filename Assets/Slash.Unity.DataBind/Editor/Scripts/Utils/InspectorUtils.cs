// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InspectorUtils.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Editor.Utils
{
    using System;
    using Slash.Unity.DataBind.Core.Data;
    using Slash.Unity.DataBind.Core.Utils;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public static class InspectorUtils
    {
        public static void DrawDataTrigger(string memberName, DataTrigger dataTrigger)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(memberName);
            if (GUILayout.Button("Invoke"))
            {
                dataTrigger.Invoke();
            }
            EditorGUILayout.EndHorizontal();
        }

        public static object DrawValueField(string memberName, Type memberType, object memberValue, Func<string, Type, object, object> fallbackAction = null)
        {
            if (memberValue == null)
            {
                if (memberType.IsValueType)
                {
                    memberValue = Activator.CreateInstance(memberType);
                }
            }

            if (memberType == typeof(int))
            {
                return EditorGUILayout.IntField(memberName, (int) memberValue);
            }

            if (memberType == typeof(long))
            {
                return EditorGUILayout.LongField(memberName, (long) memberValue);
            }

            if (memberType == typeof(float))
            {
                return EditorGUILayout.FloatField(memberName, (float) memberValue);
            }

            if (memberType == typeof(bool))
            {
                return EditorGUILayout.Toggle(memberName, (bool) memberValue);
            }

            if (memberType == typeof(Vector2))
            {
                return EditorGUILayout.Vector2Field(memberName, (Vector2) memberValue);
            }

            if (memberType == typeof(Vector3))
            {
                return EditorGUILayout.Vector3Field(memberName, (Vector3) memberValue);
            }

            if (memberType == typeof(Vector4))
            {
                return EditorGUILayout.Vector4Field(memberName, (Vector4) memberValue);
            }

            if (memberType == typeof(string))
            {
                return EditorGUILayout.TextField(memberName, (string) memberValue);
            }

            if (TypeInfoUtils.IsEnum(memberType))
            {
                return EditorGUILayout.EnumPopup(memberName, (Enum) memberValue);
            }

            var unityObjectType = typeof(Object);
            if (unityObjectType.IsAssignableFrom(memberType))
            {
                return EditorGUILayout.ObjectField(memberName, (Object) memberValue, memberType, true);
            }

            return fallbackAction == null
                ? DrawValueFieldFallback(memberName, memberType, memberValue)
                : fallbackAction(memberName, memberType, memberValue);
        }

        public static object DrawValueFieldFallback(string memberName, Type memberType, object memberValue)
        {
            EditorGUILayout.LabelField(memberName, memberValue != null ? memberValue.ToString() : "null");
            return memberValue;
        }
    }
}