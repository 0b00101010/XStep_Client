namespace Slash.Unity.DataBind.Examples.NotifyPropertyChangedExample
{
    using System.ComponentModel;
    using JetBrains.Annotations;

    public partial class NotifyPropertyChangedExampleData : INotifyPropertyChanged
    {
        private int integer;

        private float single;

        private string title;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Integer
        {
            get
            {
                return this.integer;
            }
            set
            {
                if (value == this.integer)
                {
                    return;
                }
                this.integer = value;
                this.OnPropertyChanged("Integer");
            }
        }

        public float Single
        {
            get
            {
                return this.single;
            }
            set
            {
                if (value.Equals(this.single))
                {
                    return;
                }
                this.single = value;
                this.OnPropertyChanged("Single");
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (value == this.title)
                {
                    return;
                }
                this.title = value;
                this.OnPropertyChanged("Title");
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}