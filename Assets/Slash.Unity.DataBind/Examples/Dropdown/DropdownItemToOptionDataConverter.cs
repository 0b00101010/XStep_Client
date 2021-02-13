// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DropdownItemToOptionDataConverter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.Dropdown
{
    using Slash.Unity.DataBind.Foundation.Providers.Converters;

    using UnityEngine.UI;

    public class DropdownItemToOptionDataConverter : ValueConverter<DropdownItemContext, Dropdown.OptionData>
    {
        /// <inheritdoc />
        protected override Dropdown.OptionData Convert(DropdownItemContext value)
        {
            return new Dropdown.OptionData(value.Text);
        }
    }
}