using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ColorMine.ColorSpaces;
using OpenCVForUnity;
using UnityEngine;

public class ColorMap : MonoBehaviour
{
    [HideInInspector] public List<double[]> Colors;

    private static float RgbToFloat(double d)
    {
        return (float) (d / 255.0f);
    }

    public void Redraw()
    {
        for (int i = 0; i < 9; i++)
        {
            var scalar = Colors[i];

            var eColor = GetColorFromScalarColor(scalar);
            var color = ColorFromEnum(eColor);

            var pice = transform.Find("Cube" + (i + 1));
            var text = pice.transform.Find("Text");

            text.GetComponent<TextMesh>().text = string.Format("{0}\n{1}\n{2}\n", scalar[0], scalar[1], scalar[2]);
            pice.GetComponent<Renderer>().material.color = color;
        }
    }

    private Color ColorFromEnum(RubicColors c)
    {
        switch (c)
        {
            case RubicColors.White: return Color.white;
            case RubicColors.Red: return Color.red;
            case RubicColors.Green: return Color.green;
            case RubicColors.Blue: return Color.blue;
            case RubicColors.Yellow: return Color.yellow;
            case RubicColors.Orange: return new Color(1f, 0.39f, 0f);
            default:
                throw new ArgumentOutOfRangeException("c", c, null);
        }
    }

    private RubicColors GetColorFromScalarColor(double[] scalar)
    {

        var red = (int) scalar[0];
        var green = (int) scalar[1];
        var blue = (int) scalar[2];
        
        var dominantColor = GetDominantColorFromScalar(red, green, blue);

        switch (dominantColor)
        {
            case RgbEnums.Red:
                return CalculateRed(red, green, blue);
            case RgbEnums.Green:
                return RubicColors.Green;
            case RgbEnums.Blue:
                return RubicColors.Blue;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    //red,orange and yellow
    private RubicColors CalculateRed(int red, int green, int blue)
    {
        var deltaGreenBlue = Math.Abs(green - blue);
        var deltaGreenRed = Math.Abs(green - red);


        if (deltaGreenBlue > 40 && deltaGreenRed < 60)
            return RubicColors.Yellow;

        if (deltaGreenBlue > 40)
            return RubicColors.Orange;

        return RubicColors.Red;
    }

    //Green
    private RubicColors CalculateGreen(int red, int blue)
    {
        return RubicColors.Green;
    }

    //blue and white
    private RubicColors CalculateBlue(int red, int green)
    {
        return RubicColors.Blue;
    }

    
    private RgbEnums GetDominantColorFromScalar(int red, int green, int blue)
    {
        var dict = new Dictionary<RgbEnums,int>
        {
            {RgbEnums.Blue,blue},
            {RgbEnums.Green,green},
            {RgbEnums.Red,red}
        };

        var max = Math.Max(red, Math.Max(green, blue));
        var key = dict.FirstOrDefault(kvp => kvp.Value == max).Key;
        return key;
    }

    public enum RgbEnums
    {
        Red,
        Green,
        Blue
    }

    public enum RubicColors
    {
        White,
        Red,
        Green,
        Blue,
        Yellow,
        Orange
    }
}