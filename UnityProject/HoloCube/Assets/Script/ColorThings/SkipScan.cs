using System;
using System.Collections.Generic;
using OpenCVForUnity;
using OpenCVForUnityExample;
using UnityEngine;
using ColorMine.ColorSpaces;
using Rect = OpenCVForUnity.Rect;
using System.Collections;
using System.Linq;

public class SkipScan : MonoBehaviour
{
    public static double[] orange = { 255, 69, 0 };
    public static double[] blue = { 0, 0, 255 };
    public static ColorMap cm = new ColorMap();

    public static List<List<double[]>> VadDuVill = new List<List<double[]>>();
    public static List<double[]> C1 = new List<double[]>(){orange, orange, orange, orange, orange, orange, orange, orange, orange};
    public static List<double[]> C2 = new List<double[]>() {blue, blue, blue, blue, blue, blue, blue, blue, blue};
    

    public static double[] color = new double[3];

    // Use this for initialization
    void Start()
    {


    }
    public static double[] getColorDouble()
        
    {
        /*
        ColorThings.RubicColors asd = cm.Face.MiddleColor;
        print(asd.ToString());
        color = C1[0];
        C1.RemoveAt(0); */

        return orange;
    }

    public static void GenerateList() { 
        VadDuVill.Add(C1);
        VadDuVill.Add(C2);

    }


    // Update is called once per frame
    void Update()
    {
        print("2");
    }
}




