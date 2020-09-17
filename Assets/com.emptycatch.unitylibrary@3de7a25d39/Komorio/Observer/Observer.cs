using System;
using System.Collections.Generic;

/// <summary>
/// The Observer is responsible for receiving values after registering somewhere.
/// </summary>
public interface Observer{
    void Notification();
}

public interface Observer<T>{
    void Notification(T value);
}

public interface Observer<T, T1>{
    void Notification(T firstValue, T1 secondValue);
}