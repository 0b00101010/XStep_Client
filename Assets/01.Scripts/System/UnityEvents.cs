using UnityEngine.Events;
using System;

[Serializable]
class VOIDEvent : UnityEvent{}

[Serializable]
public class NODEEvent : UnityEvent<Node, int> {}

[Serializable]
public class INTEvent : UnityEvent<int> {}