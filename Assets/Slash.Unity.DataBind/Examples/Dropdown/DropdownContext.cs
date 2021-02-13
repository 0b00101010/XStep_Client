// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumEqualityCheckContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.Dropdown
{
    using Slash.Unity.DataBind.Core.Data;

    public class DropdownContext : Context
    {
        private readonly Property<Collection<DropdownItemContext>> itemsProperty =
            new Property<Collection<DropdownItemContext>>(new Collection<DropdownItemContext>());

        private readonly Property<int> selectedIndexProperty = new Property<int>();

        public DropdownContext()
        {
            this.Items = new Collection<DropdownItemContext>
            {
                new DropdownItemContext { Text = "One" },
                new DropdownItemContext { Text = "Two" }
            };
        }

        public Collection<DropdownItemContext> Items
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

        public int SelectedIndex
        {
            get
            {
                return this.selectedIndexProperty.Value;
            }
            set
            {
                this.selectedIndexProperty.Value = value;
            }
        }

        public void SelectItem(int index)
        {
            this.SelectedIndex = index;
        }
    }
}