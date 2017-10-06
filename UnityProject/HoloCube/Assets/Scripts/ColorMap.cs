using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ColorMine.ColorSpaces;
using OpenCVForUnity;
using OpenCVForUnityExample.ColorDetection;
using UnityEngine;

public class ColorMap : MonoBehaviour
{
    [HideInInspector] public List<double[]> Colors;

    public bool PauseOnColorNotFound;

    private ColorDetection _colorDetection;
    
    private void Start()
    {
        _colorDetection = new ColorDetection();
    }

    public void Redraw()
    {
        for (int i = 0; i < 9; i++)
        {
            var scalar = Colors[i];

            var eColor = _colorDetection.ColorEnumFromScalarColor(scalar);
            var color = _colorDetection.UnityColorFromEnum(eColor);

            var pice = transform.Find("Cube" + (i + 1));
            var text = pice.transform.Find("Text");
            var rgb = new Rgb(scalar[0], scalar[1], scalar[2]);
            var hsv = rgb.To<Hsv>();

            text.GetComponent<TextMesh>().text = string.Format("{0}\n{1}\n{2}\n", (int) hsv.H, (int) (hsv.S * 100),
                (int) (hsv.V * 100));
            pice.GetComponent<Renderer>().material.color = color;
        }
    }
    
    private void Update()
    {
        _colorDetection.PauseOnColorNotFound = PauseOnColorNotFound;
    }


}