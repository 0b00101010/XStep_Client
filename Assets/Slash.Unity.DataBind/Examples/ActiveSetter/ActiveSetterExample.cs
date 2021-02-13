namespace Slash.Unity.DataBind.Examples.ActiveSetter
{
    using Slash.Unity.DataBind.Core.Presentation;
    using UnityEngine;

    public class ActiveSetterExample : MonoBehaviour
    {
        public ContextHolder ContextHolder;

        /// <summary>
        ///     Indicates if controls should start active or inactive.
        /// </summary>
        public bool StartActive;

        private int frameCount;

        public void LateUpdate()
        {
            Debug.LogFormat(this, "LateUpdate (Frame {0})", this.frameCount);
            ++this.frameCount;
        }

        public void Start()
        {
            Debug.LogFormat(this, "Start (Frame {0})", this.frameCount);

            if (this.ContextHolder != null)
            {
                // Set initial context.
                this.ContextHolder.Context = new ActiveSetterExampleContext {IsObjectActive = this.StartActive};
            }
        }

        public void Update()
        {
            Debug.LogFormat(this, "Update (Frame {0})", this.frameCount);
        }
    }
}