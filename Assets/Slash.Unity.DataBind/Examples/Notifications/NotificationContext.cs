// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.Notifications
{
    using System;

    using Slash.Unity.DataBind.Core.Data;

    public class NotificationContext : Context
    {
        private readonly Property<string> textProperty = new Property<string>();

        public event Action<NotificationContext> FadedOut;

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

        public void OnFadedOut()
        {
            var handler = this.FadedOut;
            if (handler != null)
            {
                handler(this);
            }
        }
    }
}