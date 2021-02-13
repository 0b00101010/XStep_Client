namespace Slash.Unity.DataBind.Examples.Dropdown
{
    using Slash.Unity.DataBind.Core.Data;

    public class DropdownItemContext : Context
    {
        private readonly Property<string> textProperty = new Property<string>();

        public string Text
        {
            get
            {
                return this.textProperty.Value;
            }
            set
            {
                this.textProperty.Value = value;
            }
        }
    }
}