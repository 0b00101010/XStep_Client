// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExampleContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.Collection
{
    using System.Linq;

    using Slash.Unity.DataBind.Core.Data;

    /// <summary>
    ///   Context for Collection example.
    /// </summary>
    public class CollectionExampleContext : Context
    {
        private readonly Property<Collection<CollectionExampleItemContext>> itemsProperty =
            new Property<Collection<CollectionExampleItemContext>>(new Collection<CollectionExampleItemContext>());

        /// <summary>
        ///   Constructor.
        /// </summary>
        public CollectionExampleContext()
        {
            this.Items.Add(new CollectionExampleItemContext { Text = "This" });
            this.Items.Add(new CollectionExampleItemContext { Text = "Is" });
            this.Items.Add(new CollectionExampleItemContext { Text = "Data Bind" });
        }

        /// <summary>
        ///   Items.
        /// </summary>
        public Collection<CollectionExampleItemContext> Items
        {
            get
            {
                return this.itemsProperty.Value;
            }
            set
            {
                this.itemsProperty.Value = value;
            }
        }

        /// <summary>
        ///   Adds an item to the collection.
        /// </summary>
        public void AddItem()
        {
            this.Items.Add(new CollectionExampleItemContext { Text = this.Items.Count.ToString() });
        }

        /// <summary>
        ///   Removes an item from the collection.
        /// </summary>
        public void RemoveItem()
        {
            if (this.Items.Count > 0)
            {
                this.Items.Remove(this.Items.Last());
            }
        }

        /// <summary>
        ///   Replaces the collection completely.
        /// </summary>
        public void ReplaceCollection()
        {
            this.Items = new Collection<CollectionExampleItemContext>
            {
                new CollectionExampleItemContext { Text = "A" },
                new CollectionExampleItemContext { Text = "New" },
                new CollectionExampleItemContext { Text = "Collection" }
            };
        }
    }
}