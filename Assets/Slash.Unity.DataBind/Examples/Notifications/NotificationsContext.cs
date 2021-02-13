// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationsContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.Notifications
{
    using Slash.Unity.DataBind.Core.Data;

    public class NotificationsContext : Context
    {
        private readonly Property<Collection<NotificationContext>> notificationsProperty =
            new Property<Collection<NotificationContext>>(new Collection<NotificationContext>());

        public NotificationsContext()
        {
            this.notificationsProperty.Value.ItemAdded += this.OnNotificationAdded;
            this.notificationsProperty.Value.ItemInserted += this.OnNotificationInserted;
            this.notificationsProperty.Value.ItemRemoved += this.OnNotificationRemoved;
        }

        public Collection<NotificationContext> Notifications
        {
            get
            {
                return this.notificationsProperty.Value;
            }
        }

        private void OnNotificationAdded(object item)
        {
            var notification = (NotificationContext)item;
            notification.FadedOut += this.OnNotificationFadedOut;
        }

        private void OnNotificationFadedOut(NotificationContext notification)
        {
            this.Notifications.Remove(notification);
        }

        private void OnNotificationInserted(object item, int index)
        {
            var notification = (NotificationContext)item;
            notification.FadedOut += this.OnNotificationFadedOut;
        }

        private void OnNotificationRemoved(object item)
        {
            var notification = (NotificationContext)item;
            notification.FadedOut -= this.OnNotificationFadedOut;
        }
    }
}