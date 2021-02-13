namespace Slash.Unity.DataBind.Foundation.Providers.VR
{
    using Slash.Unity.DataBind.Core.Presentation;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Serialization;    
    using UnityEngine.XR;

    /// <summary>
    ///   Provides the local position of a specific vr node.
    ///   See https://docs.unity3d.com/ScriptReference/VR.InputTracking.GetLocalPosition.html
    /// </summary>
    [AddComponentMenu("Data Bind/Foundation/Providers/VR/[DB] Input Tracking Local Position Provider")]
    public class InputTrackingLocalPositionProvider : DataProvider
    {
        /// <summary>
        ///   Node to get local position for.
        /// </summary>
        [FormerlySerializedAs("VRNode")]
        public XRNode XRNode;

        private Vector3 position;

        /// <inheritdoc />
        public override object Value
        {
            get
            {
                return this.GetPosition();
            }
        }

        /// <summary>
        ///   Unity callback.
        /// </summary>
        protected void Update()
        {
            var newPosition = this.GetPosition();
            if (this.position != newPosition)
            {
                this.position = newPosition;
                this.OnValueChanged();
            }
        }

        /// <inheritdoc />
        protected override void UpdateValue()
        {
            this.OnValueChanged();
        }

        private Vector3 GetPosition()
        {
            List<XRNodeState> nodeStates = new List<XRNodeState>();
            InputTracking.GetNodeStates(nodeStates);
            XRNodeState nodeState = nodeStates.Find(n => n.nodeType == this.XRNode);
            nodeState.TryGetRotation(out Quaternion localRotation);
            nodeState.TryGetPosition(out Vector3 localPosition);
            return localPosition;
        }
    }
}