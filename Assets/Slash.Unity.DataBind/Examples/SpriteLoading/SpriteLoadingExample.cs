// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpriteLoadingExample.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.SpriteLoading
{
    using Slash.Unity.DataBind.Core.Presentation;
    using UnityEngine;

    public class SpriteLoadingExample : MonoBehaviour
    {
        public ContextHolder ContextHolder;

        public string SpriteName;

        public void Awake()
        {
            if (this.ContextHolder != null)
            {
                var context = new SpriteLoadingContext {SpriteName = this.SpriteName};
                this.ContextHolder.Context = context;
            }
        }
    }
}