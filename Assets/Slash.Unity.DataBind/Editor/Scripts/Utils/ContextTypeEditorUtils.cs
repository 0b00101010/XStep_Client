// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextTypeEditorUtils.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Editor.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Slash.Unity.DataBind.Core.Presentation;
    using UnityEngine;

    /// <summary>
    ///     Utility methods for context types in editor.
    /// </summary>
    public static class ContextTypeEditorUtils
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Returns the context type for the specified component. The hierarchy is searched and the type of first
        ///     found context is returned.
        ///     Considers <see cref="MasterPath" /> behaviours.
        /// </summary>
        /// <param name="targetObject">Component to get context type for.</param>
        /// <returns>Type of context for specified component.</returns>
        public static Type GetContextType(Component targetObject)
        {
            // Find first context holder.
            var contextHolders = targetObject != null ? targetObject.GetComponentsInParent<ContextHolder>(true) : null;
            if (contextHolders == null || contextHolders.Length == 0)
            {
                return null;
            }

            var contextHolder = contextHolders[0];
            if (contextHolder.ContextType == null)
            {
                return null;
            }

            // Consider master paths.
            var currentTargetObject = targetObject.transform;
            
            // For a master path, start with parent to not consider the component itself.
            if (targetObject is MasterPath)
            {
                currentTargetObject = currentTargetObject.parent;
            }

            var masterPaths = new Stack<string>();
            while (currentTargetObject != null)
            {
                // Check for master path.
                var masterPath = currentTargetObject.GetComponent<MasterPath>();
                if (masterPath != null)
                {
                    masterPaths.Push(masterPath.Path);
                }

                // Break if context holder reached.
                if (currentTargetObject.gameObject == contextHolder.gameObject)
                {
                    break;
                }

                currentTargetObject = currentTargetObject.transform.parent;
            }

            // Go down into sub contexts depending on master paths.
            var contextType = contextHolder.ContextType;
            while (masterPaths.Count > 0)
            {
                var masterPath = masterPaths.Pop();
                contextType = GetSubContextType(contextType, masterPath);
            }

            return contextType;
        }

        #endregion

        #region Methods

        private static Type GetSubContextType(Type contextType, string path)
        {
            if (contextType == null)
            {
                return null;
            }

            var pointPos = path.IndexOf('.');
            var subContextName = path;
            string pathRest = null;
            if (pointPos >= 0)
            {
                subContextName = path.Substring(0, pointPos);
                pathRest = path.Substring(pointPos + 1);
            }

            // Check public properties and fields.
            Type subContextType = null;
            var propertyInfo = contextType.GetProperty(subContextName, BindingFlags.Instance | BindingFlags.Public);
            if (propertyInfo != null)
            {
                subContextType = propertyInfo.PropertyType;
            }
            else
            {
                var fieldInfo = contextType.GetField(subContextName, BindingFlags.Instance | BindingFlags.Public);
                if (fieldInfo != null)
                {
                    subContextType = fieldInfo.FieldType;
                }
            }

            return string.IsNullOrEmpty(pathRest) ? subContextType : GetSubContextType(subContextType, pathRest);
        }

        #endregion
    }
}