// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputFieldGetterContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.InputFieldGetter
{
    using Slash.Unity.DataBind.Core.Data;

    using UnityEngine;

    /// <summary>
    ///   Context for the InputFieldGetter example.
    /// </summary>
    public class InputFieldGetterContext : Context
    {
        #region Fields

        private readonly Collection<string> messages = new Collection<string>();

        /// <summary>
        ///   Data binding property, used to get informed when a data change happened.
        /// </summary>
        private readonly Property<string> textProperty = new Property<string>();

        #endregion

        #region Properties

        /// <summary>
        ///   Submitted messages.
        /// </summary>
        public Collection<string> Messages
        {
            get
            {
                return this.messages;
            }
        }

        /// <summary>
        ///   Current text which is written.
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
        ///   Submits the specified message.
        /// </summary>
        /// <param name="message">Message to submit.</param>
        public void SubmitMessage(string message)
        {
            Debug.Log("Message: " + message);
            this.messages.Add(message);

            // Clear current text.
            this.Text = string.Empty;
        }

        #endregion
    }
}