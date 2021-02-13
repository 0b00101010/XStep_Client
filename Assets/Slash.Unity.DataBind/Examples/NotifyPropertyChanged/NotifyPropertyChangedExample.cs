namespace Slash.Unity.DataBind.Examples.NotifyPropertyChangedExample
{
    using Slash.Unity.DataBind.Core.Data;
    using Slash.Unity.DataBind.Core.Presentation;
    using UnityEngine;

    public class NotifyPropertyChangedExample : MonoBehaviour
    {
        public ContextHolder ContextHolder;

        private void Start()
        {
            // Create context.
            var data = new NotifyPropertyChangedExampleData
            {
                Title = "INotifyPropertyChanged as a context",
                Integer = 23,
                Single = 42.123f
            };
            this.ContextHolder.Context = new NotifyPropertyChangedDataContext(data);
        }
    }
}