// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextTypeCache.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Editor.Utils
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using Slash.Unity.DataBind.Core.Data;
    using Slash.Unity.DataBind.Core.Utils;

    /// <summary>
    ///     Cache for context type reflection data.
    /// </summary>
    public static class ContextTypeCache
    {
        /// <summary>
        ///     Maximum path depth to avoid infinite loops.
        /// </summary>
        private const int MaxPathDepth = 10;

        private static readonly List<PathNode> CachedPaths = new List<PathNode>();

        static ContextTypeCache()
        {
            ContextTypes = new List<Type> {null};
            var settings = DataBindingSettingsProvider.Settings;

            var dataProviderDataContextTypes = ReflectionUtils.FindTypesWithBase<Context>()
                .Where(type => !type.IsAbstract);
            var notifyPropertyChangedDataContextTypes = ReflectionUtils.FindTypesWithBase<INotifyPropertyChanged>()
                .Where(type =>
                    !type.IsAbstract && !type.FullName.StartsWith("JetBrains") &&
                    !type.FullName.StartsWith("Newtonsoft") &&
                    !type.FullName.StartsWith("System"));

            var ignoreNamespaces = settings.IgnoreContextTypesInNamespaces;

            var availableContextTypes = dataProviderDataContextTypes
                .Union(notifyPropertyChangedDataContextTypes)
                .Where(type => ignoreNamespaces == null || !ignoreNamespaces.Any(type.FullName.StartsWith))
                .ToList(); 
            availableContextTypes.Sort(
                (typeA, typeB) => string.Compare(typeA.FullName, typeB.FullName, StringComparison.Ordinal));
            ContextTypes.AddRange(availableContextTypes);
            ContextTypeNames = ContextTypes.Select(type => type != null ? type.FullName : "None").ToArray();

            var root = new TypePath();
            for (var index = 0; index < ContextTypeNames.Length; index++)
            {
                var contextTypeName = ContextTypeNames[index];
                var contextTypeNameParts = contextTypeName.Split('.');

                var path = root;
                foreach (var contextTypeNamePart in contextTypeNameParts)
                {
                    var newPath = path.SubPaths.FirstOrDefault(subPath => subPath.Name == contextTypeNamePart);
                    if (newPath == null)
                    {
                        newPath = new TypePath {Name = contextTypeNamePart};
                        path.SubPaths.Add(newPath);
                    }

                    path = newPath;
                }
            }

            var paths = new List<string>();
            foreach (var rootPath in root.SubPaths)
            {
                AddTypePaths(paths, string.Empty, rootPath);
            }

            ContextTypePaths = paths.ToArray();
        }

        /// <summary>
        ///     Names of context types.
        /// </summary>
        public static string[] ContextTypeNames { get; private set; }

        /// <summary>
        ///     Names of context types.
        ///     But using slashes instead of dots to provide a (namespace) path to the class.
        /// </summary>
        public static string[] ContextTypePaths { get; private set; }

        public static List<Type> ContextTypes { get; private set; }

        public static IEnumerable<ContextMemberInfo> GetMemberInfos(Type type)
        {
            // Collect all public members.
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in propertyInfos)
            {
                // Skip data property.
                var dataPropertyType = typeof(Property);
                if (dataPropertyType.IsAssignableFrom(propertyInfo.PropertyType))
                {
                    continue;
                }

                // Skip indexer.
                if (propertyInfo.GetIndexParameters().Length > 0)
                {
                    continue;
                }

                yield return new ContextMemberInfo {Property = propertyInfo};
            }

            var fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var fieldInfo in fieldInfos)
            {
                // Skip data property.
                var dataPropertyType = typeof(Property);
                if (!dataPropertyType.IsAssignableFrom(fieldInfo.FieldType))
                {
                    yield return new ContextMemberInfo {Field = fieldInfo};
                }
            }

            var methodInfos =
                type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Where(
                        methodInfo =>
                            !methodInfo.IsSpecialName && methodInfo.DeclaringType != typeof(object)
                                                      && methodInfo.DeclaringType != typeof(Context) &&
                                                      methodInfo.ReturnType == typeof(void));
            foreach (var methodInfo in methodInfos)
            {
                yield return new ContextMemberInfo {Method = methodInfo};
            }
        }

        /// <summary>
        ///     Returns all available paths of the specified <see cref="IDataContext" /> type.
        /// </summary>
        /// <param name="type">Type of context to get paths for.</param>
        /// <param name="filter">Filter to only return specific members of the context.</param>
        /// <returns>List of available paths of the specified <see cref="IDataContext" /> type.</returns>
        public static List<string> GetPaths(Type type, ContextMemberFilter filter = ContextMemberFilter.All)
        {
            if (type == null)
            {
                return null;
            }

            var pathNode = GetPathNode(type);
            return CreatePaths(pathNode, filter, 0);
        }

        private static void AddTypePaths(List<string> paths, string path, TypePath typePath)
        {
            path = string.IsNullOrEmpty(path) ? typePath.Name : path + typePath.Name;
            if (typePath.SubPaths.Count == 0)
            {
                paths.Add(path);
            }
            else if (typePath.SubPaths.Count == 1)
            {
                // Skip adding separate node for this path part as there is only one choice and it is no end node.
                var typeSubPath = typePath.SubPaths.First();
                if (typeSubPath.SubPaths.Count > 0)
                {
                    AddTypePaths(paths, path + ".", typeSubPath);
                }
                else
                {
                    AddTypePaths(paths, path + "/", typeSubPath);
                }
            }
            else
            {
                foreach (var typeSubPath in typePath.SubPaths)
                {
                    AddTypePaths(paths, path + "/", typeSubPath);
                }
            }
        }

        private static bool CheckFilter(PathNode pathNode, ContextMemberFilter filter)
        {
            if (filter.IsOptionSet(ContextMemberFilter.Fields) && pathNode.Info.Field != null)
            {
                return true;
            }

            if (filter.IsOptionSet(ContextMemberFilter.Properties) && pathNode.Info.Property != null)
            {
                return true;
            }

            if (filter.IsOptionSet(ContextMemberFilter.Methods) && pathNode.Info.Method != null)
            {
                return true;
            }

            if (filter.IsOptionSet(ContextMemberFilter.Contexts) &&
                typeof(IDataContext).IsAssignableFrom(pathNode.Type))
            {
                return true;
            }

            if (filter.IsOptionSet(ContextMemberFilter.Triggers) && typeof(DataTrigger).IsAssignableFrom(pathNode.Type))
            {
                return true;
            }

            return false;
        }

        private static List<string> CreatePaths(PathNode pathNode, ContextMemberFilter filter, int depth)
        {
            var paths = new List<string>();

            // Check for maximum path depth.
            if (depth > MaxPathDepth)
            {
                return paths;
            }

            // Check if node has children.
            if (pathNode.Children == null)
            {
                return paths;
            }

            foreach (var childNode in pathNode.Children)
            {
                // Check if node should be considered.
                if (CheckFilter(childNode, filter))
                {
                    var childName = childNode.Info.Name;
                    paths.Add(childName);
                }

                // Check if to step into child nodes.
                if (filter.IsOptionSet(ContextMemberFilter.Recursive))
                {
                    var subContextPaths = CreatePaths(childNode, filter, depth + 1);
                    paths.AddRange(
                        subContextPaths.Select(subContextPath => childNode.Info.Name + "." + subContextPath));
                }
            }

            return paths;
        }

        private static PathNode GetPathNode(Type type)
        {
            // Check if cached.
            var pathNode = CachedPaths.FirstOrDefault(node => node.Type == type);
            if (pathNode != null)
            {
                return pathNode;
            }

            // Create new.
            pathNode = new PathNode {Type = type};
            CachedPaths.Add(pathNode);

            // Check if children should be considered.
            if (!ShouldConsiderChildrenOfType(type))
            {
                return pathNode;
            }

            pathNode.Children = new List<PathNode>();

            var memberInfos = GetMemberInfos(type);
            foreach (var memberInfo in memberInfos)
            {
                // Skip "IsReadOnly" property.
                if (memberInfo.Name == "IsReadOnly")
                {
                    continue;
                }

                var childNode = GetPathNode(memberInfo);
                pathNode.Children.Add(childNode);
            }

            return pathNode;
        }

        private static PathNode GetPathNode(ContextMemberInfo memberInfo)
        {
            PathNode pathNode;
            if (memberInfo.Field != null)
            {
                pathNode = new PathNode(GetPathNode(memberInfo.Field.FieldType));
            }
            else if (memberInfo.Property != null)
            {
                pathNode = new PathNode(GetPathNode(memberInfo.Property.PropertyType));
            }
            else
            {
                pathNode = new PathNode();
            }

            pathNode.Info = memberInfo;
            return pathNode;
        }

        private static bool ShouldConsiderChildrenOfType(Type type)
        {
            // Check if context.
            var dataContextType = typeof(Context);
            var isContext = dataContextType.IsAssignableFrom(type);
            if (isContext)
            {
                return true;
            }

            // Check if collection.
            var collectionType = typeof(Collection);
            var isCollection = collectionType.IsAssignableFrom(type);
            if (isCollection)
            {
                return true;
            }

            // Skip system types.
            var isSystemType = type.Namespace != null && type.Namespace.StartsWith("System");
            if (isSystemType)
            {
                return false;
            }

            // Skip Unity types.
            var isUnityType = type.Namespace != null && type.Namespace.StartsWith("Unity");
            if (isUnityType)
            {
                return false;
            }

            // Check if implements INotifyPropertyChanged.
            var notifyPropertyChangedType = typeof(INotifyPropertyChanged);
            var isNotifyPropertyChangedType = notifyPropertyChangedType.IsAssignableFrom(type);
            if (isNotifyPropertyChangedType)
            {
                return true;
            }

            // Consider custom serializable types.
            var isSerializable = type.GetCustomAttributes(typeof(SerializableAttribute), true).Length > 0;
            if (isSerializable)
            {
                return true;
            }

            return false;
        }

        public class ContextMemberInfo
        {
            public FieldInfo Field { get; set; }

            public MethodInfo Method { get; set; }

            public string Name
            {
                get
                {
                    if (this.Field != null)
                    {
                        return this.Field.Name;
                    }

                    if (this.Property != null)
                    {
                        return this.Property.Name;
                    }

                    if (this.Method != null)
                    {
                        return this.Method.Name;
                    }

                    return null;
                }
            }

            public PropertyInfo Property { get; set; }
        }

        private class PathNode
        {
            public PathNode(PathNode pathNode)
            {
                this.Children = pathNode.Children;
                this.Type = pathNode.Type;
                this.Info = pathNode.Info;
            }

            public PathNode()
            {
            }

            public List<PathNode> Children { get; set; }

            public ContextMemberInfo Info { get; set; }

            public Type Type { get; set; }
        }

        private class TypePath
        {
            public readonly List<TypePath> SubPaths = new List<TypePath>();

            public string Name;
        }
    }
}