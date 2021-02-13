namespace Slash.Unity.DataBind.Editor.Editors
{
    using System;
    using System.Reflection;
    using Slash.Unity.DataBind.Editor.Utils;
    using UnityEditor;
    using UnityEngine;

    public class InvokeEventPopup : PopupWindowContent
    {
        private readonly Action<object[]> invokeAction;

        private readonly ParameterInfo[] parameterInfos;

        private readonly object[] parameterValues;

        public InvokeEventPopup(ParameterInfo[] parameterInfos, Action<object[]> invokeAction)
        {
            this.parameterInfos = parameterInfos;
            this.invokeAction = invokeAction;
            this.parameterValues = new object[parameterInfos.Length];
        }

        public override Vector2 GetWindowSize()
        {
            var lineCount = this.parameterInfos.Length + 2;
            return new Vector2(200, lineCount * EditorGUIUtility.singleLineHeight + 20);
        }

        public override void OnGUI(Rect rect)
        {
            GUILayout.Label("Parameters", EditorStyles.boldLabel);

            for (var index = 0; index < this.parameterInfos.Length; index++)
            {
                var parameterInfo = this.parameterInfos[index];

                var parameterValue = this.parameterValues[index];

                this.parameterValues[index] = InspectorUtils.DrawValueField(ObjectNames.NicifyVariableName(parameterInfo.Name),
                    parameterInfo.ParameterType,
                    parameterValue);
            }

            if (GUILayout.Button("Invoke"))
            {
                this.invokeAction(this.parameterValues);
            }
        }
    }
}