// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextPropertyContext.cs.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.ContextProperty
{
    using Slash.Unity.DataBind.Core.Data;

    using UnityEngine;

    /// <summary>
    ///   Context for the ContextProperty example.
    /// </summary>
    public class ContextPropertyContext : Context
    {
        #region Fields

        private readonly Collection<ContextPropertyItemContext> items = new Collection<ContextPropertyItemContext>();

        private readonly Property<ContextPropertyItemContext> selectedItemProperty =
            new Property<ContextPropertyItemContext>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Constructor.
        /// </summary>
        public ContextPropertyContext()
        {
            this.items.Add(new ContextPropertyItemContext { Text = "This" });
            this.items.Add(new ContextPropertyItemContext { Text = "Is" });
            var itemContext = new ContextPropertyItemContext { Text = "Data Bind" };
            this.items.Add(itemContext);
            this.SelectedItem = itemContext;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Items.
        /// </summary>
        public Collection<ContextPropertyItemContext> Items
        {
            get
            {
                return this.items;
            }
        }

        /// <summary>
        ///   Selcted item.
        /// </summary>
        public ContextPropertyItemContext SelectedItem
        {
            get
            {
                return this.selectedItemProperty.Value;
            }
            set
            {
                this.selectedItemProperty.Value = value;
            }
        }
        
        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Selects the specified item.
        /// </summary>
        /// <param name="item">Item to select.</param>
        public void OnItemSelected(Context item)
        {
            Debug.Log("Item selected: " + item);
            this.SelectedItem = (ContextPropertyItemContext)item;
        }

        #endregion
    }
}