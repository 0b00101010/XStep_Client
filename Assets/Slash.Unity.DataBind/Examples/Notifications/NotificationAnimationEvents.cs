// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationAnimationEvents.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.Notifications
{
    using UnityEngine;
    using UnityEngine.Events;

    public class NotificationAnimationEvents : MonoBehaviour
    {
        /// <summary>
        ///   Called when notification faded out.
        /// </summary>
        [Tooltip("Called when notification faded out")]
        public UnityEvent FadedOut;

        public void OnFadedOut()
        {
            this.FadedOut.Invoke();
        }
    }
}