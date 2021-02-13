// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TwoWayBindingExampleContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.TwoWayBinding
{
    using System;
    using Slash.Unity.DataBind.Core.Presentation;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class TwoWayBindingExample : MonoBehaviour
    {
        private CharacterEditorContext characterEditorContext;

        public ContextHolder CharacterEditorContextHolder;

        private CharacterSelectionContext characterSelectionContext;

        public ContextHolder CharacterSelectionContextHolder;

        public void Awake()
        {
            this.characterSelectionContext = new CharacterSelectionContext();

            // Create some dummy characters.
            this.characterSelectionContext.Characters.Add(new CharacterContext
            {
                Name = "John",
                Age = 42,
                Strength = Random.Range(0, 100),
                Charisma = Random.Range(0, 100)
            });
            this.characterSelectionContext.Characters.Add(new CharacterContext
            {
                Name = "Lisa",
                Age = 23,
                Strength = Random.Range(0, 100),
                Charisma = Random.Range(0, 100)
            });
            this.characterSelectionContext.Characters.Add(new CharacterContext
            {
                Name = "Evan",
                Age = 66,
                Strength = Random.Range(0, 100),
                Charisma = Random.Range(0, 100)
            });

            this.characterEditorContext = new CharacterEditorContext();
        }

        public void OnDisable()
        {
            this.characterSelectionContext.SelectCharacter -= this.OnSelectCharacter;
        }

        public void OnEnable()
        {
            this.characterSelectionContext.SelectCharacter += this.OnSelectCharacter;

            if (this.CharacterSelectionContextHolder != null)
            {
                this.CharacterSelectionContextHolder.Context = this.characterSelectionContext;
            }
            if (this.CharacterEditorContextHolder != null)
            {
                this.CharacterEditorContextHolder.Context = this.characterEditorContext;
            }
        }

        private void OnSelectCharacter(CharacterContext character)
        {
            this.characterEditorContext.Character = character;
        }
    }
}