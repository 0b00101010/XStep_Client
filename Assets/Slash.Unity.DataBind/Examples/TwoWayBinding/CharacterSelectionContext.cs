// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterSelectionContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.TwoWayBinding
{
    using System;
    using Slash.Unity.DataBind.Core.Data;

    public class CharacterSelectionContext : Context
    {
        private readonly Property<Collection<CharacterContext>> charactersProperty
            = new Property<Collection<CharacterContext>>(
                new Collection<CharacterContext>());

        public Collection<CharacterContext> Characters
        {
            get
            {
                return this.charactersProperty.Value;
            }
            set
            {
                this.charactersProperty.Value = value;
            }
        }

        public event Action<CharacterContext> SelectCharacter;

        public void DoSelectCharacter(CharacterContext character)
        {
            this.OnSelectCharacter(character);
        }

        private void OnSelectCharacter(CharacterContext character)
        {
            var handler = this.SelectCharacter;
            if (handler != null)
            {
                handler(character);
            }
        }
    }
}