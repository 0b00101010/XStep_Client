namespace Slash.Unity.DataBind.Editor.Editors
{
    using System.Collections;
    using Slash.Unity.DataBind.Core.Presentation;
    using Slash.Unity.DataBind.Core.Utils;
    using UnityEditor;

    [CustomEditor(typeof(DataProvider), true)]
    public class DataProviderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var dataProvider = (DataProvider)this.target;

            if (dataProvider.isActiveAndEnabled && dataProvider.IsInitialized)
            {
                var value = dataProvider.Value;
                var collectionValue = value as IEnumerable;
                var stringValue = collectionValue != null ? collectionValue.Implode(", ") : value;
                EditorGUILayout.LabelField("Current Value: " + stringValue);
            }

            base.OnInspectorGUI();
        }
    }
}