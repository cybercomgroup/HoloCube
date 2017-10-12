using System.Collections.Generic;
using System.Linq;
using Backend;
using Color;
using ColorThings;
using UnityEngine;

public class RandomizeColors : MonoBehaviour
{
    private Dictionary<RubicColors, int> _nrOfColors;

    // Use this for initialization
    void Start()
    {
        _nrOfColors = new Dictionary<RubicColors, int>
        {
            {RubicColors.White, 0},
            {RubicColors.Yellow, 0},
            {RubicColors.Red, 0},
            {RubicColors.Orange, 0},
            {RubicColors.Green, 0},
            {RubicColors.Blue, 0},
        };


        var faces = new List<Face>();
        var dictList = _nrOfColors.ToList();
        for (int i = 0; i < _nrOfColors.Count; i++)
        {
            var list = new List<RubicColors>();
            for (var j = 0; j < 8; j++)
            {
                list.Add(GetRandomColor());
            }

            faces.Add(new Face
            {
                Colors = list,
                MiddleColor = dictList[i].Key
            });
        }

        var shader = Shader.Find("Unlit/Color");

        for (int i = 0; i < 6; i++)
        {
            var obj = transform.Find("Face" + i);
            var middle = obj.transform.Find("Middle");

            SetColor(middle,faces[i].MiddleColor,shader);
            for (int j = 0; j < 8; j++)
            {
                var pice = obj.transform.Find("Cube" + j);
                SetColor(pice,faces[i].Colors[j],shader);
            }
        }
    }
    
    private void SetColor(Transform trans,RubicColors c,Shader shader )
    {
        var material = trans.GetComponent<Renderer>().material;
        material.shader = shader;
        material.color = ColorDetection.UnityColorFromEnum(c);
    }


    private RubicColors GetRandomColor()
    {
        var avalibleColors = GetAvalibleColors();
        return avalibleColors[Random.Range(0, avalibleColors.Count)];
    }

    private List<RubicColors> GetAvalibleColors()
    {
        return _nrOfColors.Where(kvp => kvp.Value != 8).Select(kvp => kvp.Key).ToList();
    }
}