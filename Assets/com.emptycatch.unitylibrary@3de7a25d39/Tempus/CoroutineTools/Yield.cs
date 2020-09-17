using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tempus.CoroutineTools
{
    public static class Yield
    {
        /// <summary>
        /// Waits until next frame rate update function.
        /// </summary>
        public static readonly object Frame = null;

        /// <summary>
        /// Waits until the end of the frame after all cameras and GUI is rendered, just before displaying the frame on screen.
        /// </summary>
        public static readonly WaitForEndOfFrame EndOfFrame = new WaitForEndOfFrame();

        /// <summary>
        /// Waits until next fixed frame rate update function. See Also: MonoBehaviour.FixedUpdate.
        /// </summary>
        public static readonly WaitForFixedUpdate FixedUpdate = new WaitForFixedUpdate();

        private static Dictionary<float, WaitForSeconds> secondsDictionary = new Dictionary<float, WaitForSeconds>();
        private static Dictionary<float, WaitForSecondsRealtime> secondsRealtimeDictionary = new Dictionary<float, WaitForSecondsRealtime>();
        private static Dictionary<Func<bool>, WaitUntil> untilDictionary = new Dictionary<Func<bool>, WaitUntil>();
        private static Dictionary<Func<bool>, WaitWhile> whileDictionary = new Dictionary<Func<bool>, WaitWhile>();

        /// <summary>
        /// Suspends the coroutine execution for the given amount of seconds using scaled time.
        /// </summary>
        public static WaitForSeconds Seconds(float seconds)
        {
            if (!secondsDictionary.ContainsKey(seconds))
            {
                secondsDictionary.Add(seconds, new WaitForSeconds(seconds));
            }
            return secondsDictionary[seconds];
        }

        /// <summary>
        /// Suspends the coroutine execution for the given amount of seconds using unscaled time.
        /// </summary>
        public static WaitForSecondsRealtime SecondsRealtime(float seconds)
        {
            if (!secondsRealtimeDictionary.ContainsKey(seconds))
            {
                secondsRealtimeDictionary.Add(seconds, new WaitForSecondsRealtime(seconds));
            }
            return secondsRealtimeDictionary[seconds];
        }

        /// <summary>
        /// Suspends the coroutine execution until the supplied delegate evaluates to true.
        /// </summary>
        public static WaitUntil Until(Func<bool> predicate)
        {
            if (!untilDictionary.ContainsKey(predicate))
            {
                untilDictionary.Add(predicate, new WaitUntil(predicate));
            }
            return untilDictionary[predicate];
        }

        /// <summary>
        /// Suspends the coroutine execution until the supplied delegate evaluates to false.
        /// </summary>
        public static WaitWhile While(Func<bool> predicate)
        {
            if (!whileDictionary.ContainsKey(predicate))
            {
                whileDictionary.Add(predicate, new WaitWhile(predicate));
            }
            return whileDictionary[predicate];
        }
    }
}
