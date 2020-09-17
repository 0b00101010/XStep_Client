using UnityEngine;

/// <summary>
/// It is an extension script that allows you to set vector values in graphic component.
/// </summary>
public static class TransformPositionSetter{
    private static void SetPositionElement(this Transform transform, int index, float value){
        Vector3 vector = transform.position;
        
        vector[index] = value;
        transform.position = vector;
    }

    /// <summary>
    /// Change graphic transform position x value
    /// </summary>
    public static void SetPositionX(this Transform transform, float value){
        transform.SetPositionElement(0, value);
    }

    /// <summary>
    /// Change graphic transform position y value
    /// </summary>
    public static void SetPositionY(this Transform transform, float value){
        transform.SetPositionElement(1, value);
    }

    /// <summary>
    /// Change graphic transform position z value
    /// </summary>
    public static void SetPositionZ(this Transform transform, float value){
        transform.SetPositionElement(2, value);
    }
}