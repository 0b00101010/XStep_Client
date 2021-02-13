namespace Slash.Unity.DataBind.Examples.FunWithBooleans
{
    using Slash.Unity.DataBind.Core.Data;

    public class FunWithBooleansContext : Context
    {
        private readonly Property<bool> booleanProperty = new Property<bool>();

        private readonly Property<int> numberProperty = new Property<int>();

        public FunWithBooleansContext()
        {
            this.Number = 40;
        }

        public bool Boolean
        {
            get
            {
                return this.booleanProperty.Value;
            }
            set
            {
                this.booleanProperty.Value = value;
            }
        }

        public int Number
        {
            get
            {
                return this.numberProperty.Value;
            }
            set
            {
                this.numberProperty.Value = value;
            }
        }

        public void DecreaseNumber()
        {
            --this.Number;
        }

        public void IncreaseNumber()
        {
            ++this.Number;
        }
    }
}