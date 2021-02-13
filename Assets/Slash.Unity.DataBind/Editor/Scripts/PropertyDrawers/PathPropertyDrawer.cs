// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PathPropertyDrawer.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Editor.PropertyDrawers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Slash.Unity.DataBind.Core.Utils;
    using Slash.Unity.DataBind.Editor.Utils;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    ///     Property Drawer for fields with <see cref="ContextPathAttribute" />.
    /// </summary>
    [CustomPropertyDrawer(typeof(ContextPathAttribute))]
    public class PathPropertyDrawer : PropertyDrawer
    {
        private const float LineHeight = 16f;

        private const float LineSpacing = 2f;

        private readonly Dictionary<string, bool> hasPropertyCustomPath = new Dictionary<string, bool>();

        private Type currentDataContextType;

        /// <summary>
        ///     Cached data context paths.
        /// </summary>
        private List<string> dataContextPaths;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return LineHeight + (this.HasCustomPath(property.propertyPath) ? LineHeight + LineSpacing : 0);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var contextPathAttribute = this.attribute as ContextPathAttribute;
            var pathDisplayName =
                contextPathAttribute != null && !string.IsNullOrEmpty(contextPathAttribute.PathDisplayName)
                    ? contextPathAttribute.PathDisplayName
                    : "Path";

            var targetObject = property.serializedObject.targetObject;

            // Check if target object has a custom context type.
            var contextTypeProperty = property.serializedObject.FindProperty("ContextType");
            var dataContextType = contextTypeProperty != null
                ? ReflectionUtils.FindType(contextTypeProperty.stringValue)
                : ContextTypeEditorUtils.GetContextType((Component) targetObject);
            if (dataContextType != this.currentDataContextType)
            {
                this.dataContextPaths = ContextTypeCache.GetPaths(
                    dataContextType,
                    contextPathAttribute != null ? contextPathAttribute.Filter : ContextMemberFilter.All);
                this.currentDataContextType = dataContextType;
            }

            var hasCustomPath = this.HasCustomPath(property.propertyPath);
            property.stringValue = PathPopup(
                position,
                property.stringValue, this.dataContextPaths,
                pathDisplayName,
                ref hasCustomPath);
            this.hasPropertyCustomPath[property.propertyPath] = hasCustomPath;
        }

        private static string ConvertPathToDisplayOption(string path)
        {
            return path.Replace('.', '/');
        }

        private bool HasCustomPath(string propertyPath)
        {
            bool hasCustomPath;
            this.hasPropertyCustomPath.TryGetValue(propertyPath, out hasCustomPath);
            return hasCustomPath;
        }

        private static string PathPopup(
            Rect position,
            string path,
            IList<string> paths,
            string pathDisplayName,
            ref bool customPath)
        {
            var selectedIndex = paths != null ? paths.IndexOf(path) : -1;
            if (selectedIndex < 0 || customPath)
            {
                // Select custom value.
                selectedIndex = 0;
                customPath = true;
            }
            else
            {
                // Custom option is prepended.
                ++selectedIndex;
            }

            var displayedOptions = new List<GUIContent> {new GUIContent {text = "CUSTOM"}};
            if (paths != null)
            {
                displayedOptions.AddRange(
                    paths.Select(existingPath => new GUIContent(ConvertPathToDisplayOption(existingPath))));
            }

            var newSelectedIndex = EditorGUI.Popup(
                new Rect(position.x, position.y, position.width, LineHeight),
                new GUIContent(pathDisplayName),
                selectedIndex,
                displayedOptions.ToArray());
            var newPath = path;
            if (newSelectedIndex != selectedIndex)
            {
                if (newSelectedIndex <= 0)
                {
                    customPath = true;
                }
                else if (paths != null)
                {
                    customPath = false;
                    newPath = paths[newSelectedIndex - 1];
                }
            }

            if (customPath)
            {
                position.y += LineHeight + LineSpacing;
                newPath = EditorGUI.TextField(
                    new Rect(position.x, position.y, position.width, LineHeight),
                    new GUIContent("Custom Path"),
                    newPath);
            }

            return newPath;
        }
    }
}