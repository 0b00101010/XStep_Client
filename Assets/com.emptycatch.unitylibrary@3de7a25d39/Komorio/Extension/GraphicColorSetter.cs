using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// It is an extension script that allows you to set color values in graphic component.
/// </summary>
public static class GraphicColorSetter{
    private static Color SetColorElement(this Color color, int index, float value){
        color[index] = value;
        return color;
    }

    /// <summary>
    /// Change graphic color red value
    /// </summary>
    public static void SetRed(this Graphic graphic, float value){
        graphic.color = graphic.color.SetColorElement(0, value);
    }

    /// <summary>
    /// Change graphic color green value
    /// </summary>
    public static void SetGreen(this Graphic graphic, float value){
        graphic.color = graphic.color.SetColorElement(1, value);
    }

    /// <summary>
    /// Change graphic color blue value
    /// </summary>
    public static void SetBlue(this Graphic graphic, float value){
        graphic.color = graphic.color.SetColorElement(2, value);
    }

    /// <summary>
    /// Change graphic color alpha value
    /// </summary>
    public static void SetAlpha(this Graphic graphic, float value){
        graphic.color = graphic.color.SetColorElement(3, value);
    }
}