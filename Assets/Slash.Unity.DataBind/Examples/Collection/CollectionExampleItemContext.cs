// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExampleItemContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.Collection
{
    using Slash.Unity.DataBind.Core.Data;

    /// <summary>
    ///   Item context for the items in the Collection example.
    /// </summary>
    public class CollectionExampleItemContext : Context
    {
        #region Fields

        private readonly Property<string> textProperty = new Property<string>();

        #endregion

        #region Properties

        /// <summary>
        ///   Text data.
        /// </summary>
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

        #endregion
    }
}