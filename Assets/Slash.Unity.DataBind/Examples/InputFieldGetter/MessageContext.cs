// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.InputFieldGetter
{
    using Slash.Unity.DataBind.Core.Data;

    /// <summary>
    ///   Context for a single message in the ContextFieldGetter example.
    /// </summary>
    public class MessageContext : Context
    {
        #region Fields

        private readonly Property<string> textProperty = new Property<string>();

        #endregion

        #region Properties

        /// <summary>
        ///   Text of the message.
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