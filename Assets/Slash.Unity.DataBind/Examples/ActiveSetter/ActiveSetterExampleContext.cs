namespace Slash.Unity.DataBind.Examples.ActiveSetter
{
    using Slash.Unity.DataBind.Core.Data;

    public class ActiveSetterExampleContext : Context
    {
        private readonly Property<bool> isObjectActiveProperty = new Property<bool>();

        /// <inheritdoc />
        public ActiveSetterExampleContext()
        {
            this.isObjectActiveProperty.Value = false;
        }

        public bool IsObjectActive
        {
            get
            {
                return this.isObjectActiveProperty.Value;
            }
            set
            {
                this.isObjectActiveProperty.Value = value;
            }
        }
    }
}