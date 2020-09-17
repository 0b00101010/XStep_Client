using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This script can only be used in 2020 or later versions. 
/// if you want be used in other versions, you must use "EventTools.FormerEvent" 
/// </summary>
namespace EventTools.Event{
    [Serializable]
    public class UniEvent : UnityEvent { }

    [Serializable]
    public class UniEvent<T> : UnityEvent<T> { }

    [Serializable]
    public class UniEvent<T1, T2> : UnityEvent<T1, T2> { }

    [Serializable]
    public class UniEvent<T1, T2, T3> : UnityEvent<T1, T2, T3> { }

    [Serializable]
    public class UniEvent<T1, T2, T3, T4> : UnityEvent<T1, T2, T3, T4> { }
}

/// <summary>
/// This script can be used in 2020 and than previous versions.
/// but, if you use 2020 or later version, I recommend you to use EventTools.Events 
/// </summary>
namespace EventTools.FormerEvent{
    [Serializable]
    public class VoidEvent : UnityEvent { }

    [Serializable]
    public class IntEvent : UnityEvent<int> { }

    [Serializable]
    public class FloatEvent : UnityEvent<float> { }

    [Serializable]
    public class StringEvent : UnityEvent<string> { }

    [Serializable]
    public class DoubleIntEvent : UnityEvent<int, int> { }
}