// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AggregationsExampleTextContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.Aggregations
{
    using System;
    using Slash.Unity.DataBind.Core.Data;

    public class AggregationsExampleTextContext : Context
    {
        /// <inheritdoc />
        public AggregationsExampleTextContext(string text)
        {
            this.Text = text;
        }

        public event Action<AggregationsExampleTextContext> Remove;

        private readonly Property<string> textProperty =
            new Property<string>();

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

        public Property<string> TextProperty
        {
            get
            {
                return this.textProperty;
            }
        }

        public void DoRemove()
        {
            this.OnRemove();
        }

        private void OnRemove()
        {
            var handler = this.Remove;
            if (handler != null)
            {
                handler(this);
            }
        }
    }
}