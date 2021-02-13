// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextPropertyItemContext.cs.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.ContextProperty
{
    using Slash.Unity.DataBind.Core.Data;

    using UnityEngine;

    /// <summary>
    ///   Item context for the items in the ContextProperty example.
    /// </summary>
    public class ContextPropertyItemContext : Context
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

        #region Public Methods and Operators

        /// <summary>
        ///   Prints the text to the console.
        /// </summary>
        public void OnPrint()
        {
            Debug.Log("Printing selection: " + this);
        }

        /// <summary>
        ///   Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Text: {0}", this.Text);
        }

        #endregion
    }
}