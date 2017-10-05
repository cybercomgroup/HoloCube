using System.Collections;
using System.Collections.Generic;
using OpenCVForUnity;
using UnityEngine;

public class ColorMap : MonoBehaviour
{
    [HideInInspector] public List<double[]> Colors;

    private static float RgbToFloat(double d)
    {
        return (float) (d/255.0f);
    }

    public void Redraw()
    {

        for (int i = 0; i < 9; i++)
        {
            var scalar = Colors[i];

            var pice = transform.Find("Cube"+(i+1));
            var text = pice.transform.Find("Text");

            var col = new Color(RgbToFloat(scalar[0]),RgbToFloat(scalar[1]),RgbToFloat(scalar[2]));
            
            text.GetComponent<TextMesh>().text = string.Format("{0}\n{1}\n{2}\n",scalar[0],scalar[1],scalar[2]);
            pice.GetComponent<Renderer>().material.color = col;    
        }
        
        
    }
}