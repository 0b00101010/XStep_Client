// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationsExample.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.Notifications
{
    using System.Collections;

    using Slash.Unity.DataBind.Core.Presentation;

    using UnityEngine;

    public class NotificationsExample : MonoBehaviour
    {
        public ContextHolder NotificationsContextHolder;

        [Range(0.5f, 5.0f)]
        public float NotificationSpawnTimeMax = 3;

        [Range(0.5f, 5.0f)]
        public float NotificationSpawnTimeMin = 1;

        public string[] NotificationTexts;

        private NotificationsContext notificationsContext;

        public void Start()
        {
            if (this.NotificationsContextHolder == null)
            {
                Debug.LogWarning("No context holder set");
                return;
            }

            if (this.NotificationTexts.Length == 0)
            {
                Debug.LogWarning("No notification texts set");
                return;
            }

            this.notificationsContext = new NotificationsContext();
            this.NotificationsContextHolder.Context = this.notificationsContext;

            // Add dummy notifications over time.
            this.StartCoroutine(this.SpawnNotifications());
        }

        private void SpawnNotification()
        {
            var notification = new NotificationContext
            {
                Text = this.NotificationTexts[Random.Range(0, this.NotificationTexts.Length)]
            };
            this.notificationsContext.Notifications.Add(notification);
        }

        private IEnumerator SpawnNotifications()
        {
            while (true)
            {
                var nextNotificationDelay = Random.Range(this.NotificationSpawnTimeMin, this.NotificationSpawnTimeMax);
                yield return new WaitForSeconds(nextNotificationDelay);

                this.SpawnNotification();
            }
        }
    }
}