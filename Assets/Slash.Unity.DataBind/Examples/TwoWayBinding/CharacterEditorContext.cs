// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterEditorContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.TwoWayBinding
{
    using Slash.Unity.DataBind.Core.Data;

    public class CharacterEditorContext : Context
    {
        private readonly Property<CharacterContext> characterProperty =
            new Property<CharacterContext>();

        public CharacterContext Character
        {
            get
            {
                return this.characterProperty.Value;
            }
            set
            {
                this.characterProperty.Value = value;
            }
        }

        public void DecreaseAge()
        {
            if (this.Character != null)
            {
                this.Character.Age -= 1;
            }
        }

        public void IncreaseAge()
        {
            if (this.Character != null)
            {
                this.Character.Age += 1;
            }
        }
    }
}