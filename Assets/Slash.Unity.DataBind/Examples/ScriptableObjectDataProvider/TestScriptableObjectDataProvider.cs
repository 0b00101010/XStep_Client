// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScriptableObjectDataProvider.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.ScriptableObjectDataProvider
{
    using Slash.Unity.DataBind.Core.Presentation;

    /// <summary>
    ///     Formatter for converting strings to upper-case.
    /// </summary>
    public class TestScriptableObjectDataProvider : DataProvider
    {
        /// <summary>
        ///     Data value to convert to upper-case.
        /// </summary>
        public TestScriptableObject ScriptableObject;

        /// <inheritdoc />
        public override object Value
        {
            get
            {
                return this.ScriptableObject.StringData;
            }
        }
    }
}