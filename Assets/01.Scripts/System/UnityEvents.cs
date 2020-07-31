using UnityEngine.Events;
using System;

[Serializable]
public class Event : UnityEvent { }

[Serializable]
public class Event<T> : UnityEvent<T> { }

[Serializable]
public class Event<T, T1> : UnityEvent<T, T1> { }

[Serializable]
public class Event<T, T1, T2> : UnityEvent<T, T1, T2> { }

[Serializable]
public class Event<T, T1, T2, T3> : UnityEvent<T, T1, T2, T3> { }

