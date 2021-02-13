// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AggregationsExample.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.Aggregations
{
    using Slash.Unity.DataBind.Core.Presentation;
    using UnityEngine;

    public class AggregationsExample : MonoBehaviour
    {
        public ContextHolder ContextHolder;

        private void Awake()
        {
            if (this.ContextHolder != null)
            {
                // Init context.
                var context = new AggregationsExampleContext();
                this.ContextHolder.Context = context;
            }
        }
    }
}