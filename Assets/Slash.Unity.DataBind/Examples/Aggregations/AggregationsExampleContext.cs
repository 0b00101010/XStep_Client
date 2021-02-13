// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AggregationsExampleContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.Aggregations
{
    using Slash.Unity.DataBind.Core.Data;
    using Slash.Unity.DataBind.Foundation.Filters;
    using Slash.Unity.DataBind.Foundation.Utils;

    public class AggregationsExampleContext : Context
    {
        private readonly IDataProvider<bool> anyEqualsTestProperty;

        private readonly Property<string> newTextProperty =
            new Property<string>();

        private readonly IDataProvider<float> sumProperty;

        private readonly Property<Collection<AggregationsExampleTextContext>> textsProperty
            = new Property<Collection<AggregationsExampleTextContext>>(
                new Collection<AggregationsExampleTextContext>());

        /// <inheritdoc />
        public AggregationsExampleContext()
        {
            this.anyEqualsTestProperty =
                CollectionAggregation.Any<AggregationsExampleTextContext, string>(this.Texts, item => item.TextProperty, text => Equals(text, "Test"));
            this.sumProperty = CollectionAggregation.Sum(this.Texts, item =>
            {
                float number;
                return float.TryParse(item.Text, out number) ? number : 0.0f;
            });

            var textsObserver = new CollectionObserver<AggregationsExampleTextContext>();
            textsObserver.RegisterItem += item => item.Remove += this.OnRemoveText;
            textsObserver.UnregisterItem += item => item.Remove -= this.OnRemoveText;
            textsObserver.Init(this.Texts);
        }

        public bool AnyEqualsTest
        {
            get
            {
                return this.anyEqualsTestProperty.Value;
            }
        }

        public string NewText
        {
            get
            {
                return this.newTextProperty.Value;
            }
            set
            {
                this.newTextProperty.Value = value;
            }
        }

        public float Sum
        {
            get
            {
                return this.sumProperty.Value;
            }
        }

        public Collection<AggregationsExampleTextContext> Texts
        {
            get
            {
                return this.textsProperty.Value;
            }
            set
            {
                this.textsProperty.Value = value;
            }
        }

        public void DoAddNewText()
        {
            this.Texts.Add(new AggregationsExampleTextContext(this.NewText));
        }

        private void OnRemoveText(AggregationsExampleTextContext item)
        {
            this.Texts.Remove(item);
        }
    }
}