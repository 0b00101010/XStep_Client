// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToUpperFormatter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.StringToUpperFormatter
{
    using Slash.Unity.DataBind.Core.Presentation;

    /// <summary>
    ///     Formatter for converting strings to upper-case.
    /// </summary>
    public class StringToUpperFormatter : DataProvider
    {
        /// <summary>
        ///     Data value to convert to upper-case.
        /// </summary>
        public DataBinding Argument;

        /// <inheritdoc />
        public override object Value
        {
            get
            {
                var argument = this.Argument.GetValue<string>();
                return string.IsNullOrEmpty(argument) ? string.Empty : argument.ToUpper();
            }
        }

        /// <inheritdoc />
        public override void Init()
        {
            // Add bindings.
            this.AddBinding(this.Argument);
        }

        /// <inheritdoc />
        protected override void UpdateValue()
        {
            this.OnValueChanged();
        }
    }
}