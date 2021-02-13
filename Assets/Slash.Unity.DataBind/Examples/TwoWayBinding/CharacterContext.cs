// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TwoWayBindingExampleCharacterContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.TwoWayBinding
{
    using Slash.Unity.DataBind.Core.Data;

    public class CharacterContext : Context
    {
        private readonly Property<int> ageProperty =
            new Property<int>();

        private readonly Property<int> charismaProperty =
            new Property<int>();

        private readonly Property<string> nameProperty =
            new Property<string>();

        private readonly Property<int> strengthProperty =
            new Property<int>();

        public int Age
        {
            get
            {
                return this.ageProperty.Value;
            }
            set
            {
                this.ageProperty.Value = value;
            }
        }

        public int Charisma
        {
            get
            {
                return this.charismaProperty.Value;
            }
            set
            {
                this.charismaProperty.Value = value;
            }
        }

        public string Name
        {
            get
            {
                return this.nameProperty.Value;
            }
            set
            {
                this.nameProperty.Value = value;
            }
        }

        public int Strength
        {
            get
            {
                return this.strengthProperty.Value;
            }
            set
            {
                this.strengthProperty.Value = value;
            }
        }
    }
}