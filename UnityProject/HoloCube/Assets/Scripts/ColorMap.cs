using System.Collections.Generic;
using ColorMine.ColorSpaces;
using OpenCVForUnityExample.ColorDetection;
using UnityEditor;
using UnityEngine;
using Face = Backend.Face;

public class ColorMap : MonoBehaviour
{
    [HideInInspector] public List<double[]> Colors;

    public bool PauseOnColorNotFound;

    private ColorDetection _colorDetection;

    [HideInInspector] public Face Face;
    
    private void Start()
    {
        _colorDetection = new ColorDetection();
        Face = new Face();
    }

    public void Redraw()
    {
        //Reset the colors
        Face.Colors = new List<RubicColors>();
        
        Face.MiddleColor = _colorDetection.ColorEnumFromScalarColor(Colors[4]);

        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[0]));
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[1]));
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[2]));
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[5]));
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[8]));
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[7]));
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[6]));
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[3]));

        
        for (int i = 0; i < 9; i++)
        {
            var scalar = Colors[i];

            var eColor = _colorDetection.ColorEnumFromScalarColor(scalar);
            var color = ColorDetection.UnityColorFromEnum(eColor);

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

[CustomEditor(typeof(ColorMap))]
public class ColorDetectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myTarget = (ColorMap)target;
        
        EditorGUILayout.BeginHorizontal();
//        myTarget.Width = EditorGUILayout.IntField("Hue", myTarget.Width);
//        myTarget.Length = EditorGUILayout.IntField("S", myTarget.Length);
//        myTarget.Length = EditorGUILayout.IntField("V", myTarget.Length);
        EditorGUILayout.EndHorizontal();

    }
}