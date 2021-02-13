namespace Slash.Unity.DataBind.Examples.SpriteLoading
{
    using Slash.Unity.DataBind.Core.Data;

    public class SpriteLoadingContext : Context
    {
        private readonly Property<string> spriteNameProperty =
            new Property<string>();

        public string SpriteName
        {
            get
            {
                return this.spriteNameProperty.Value;
            }
            set
            {
                this.spriteNameProperty.Value = value;
            }
        }
    }
}