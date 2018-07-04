using System.Collections.Generic;
using ColorMine.ColorSpaces;
using ColorThings;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
        print("MID; red: " + Colors[4][0] + " green: " + Colors[4][1] + "blue: " + Colors[4][2]);
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[0]));
        print("0 red: " + Colors[0][0] + " green: " + Colors[0][1] + "blue: " + Colors[0][2]);
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[1]));
        print("1 red: " + Colors[1][0] + " green: " + Colors[1][1] + "blue: " + Colors[1][2]);
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[2]));
        print("2 red: " + Colors[2][0] + " green: " + Colors[2][1] + "blue: " + Colors[2][2]);
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[5]));
        print("5 red: " + Colors[5][0] + " green: " + Colors[5][1] + "blue: " + Colors[5][2]);
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[8]));
        print("8 red: " + Colors[8][0] + " green: " + Colors[8][1] + "blue: " + Colors[8][2]);
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[7]));
        print("7 red: " + Colors[7][0] + " green: " + Colors[7][1] + "blue: " + Colors[7][2]);
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[6]));
        print("6 red: " + Colors[6][0] + " green: " + Colors[6][1] + "blue: " + Colors[6][2]);
        Face.Colors.Add(_colorDetection.ColorEnumFromScalarColor(Colors[3]));
        print("3 red: " + Colors[3][0] + " green: " + Colors[3][1] + "blue: " + Colors[3][2]);

        //Is this actually just coloring the cube that was spawned out of sight?
        /* for (int i = 0; i < 9; i++)
         {
             var scalar = Colors[i];

             var eColor = _colorDetection.ColorEnumFromScalarColor(scalar);
             var color = ColorDetection.UnityColorFromEnum(eColor);

             var pice = transform.Find("Cube" + (i + 1));
             var text = pice.transform.Find("Text");
             var rgb = new Rgb(scalar[0], scalar[1], scalar[2]);
             var hsv = rgb.To<Hsv>();

             text.GetComponent<TextMesh>().name = string.Format("{0}\n{1}\n{2}\n", (int) hsv.H, (int) (hsv.S * 100),
                 (int) (hsv.V * 100));
             pice.GetComponent<Renderer>().material.color = color;*/
    }

    
    private void Update()
    {
        _colorDetection.PauseOnColorNotFound = PauseOnColorNotFound;
    }


}
#if UNITY_EDITOR
//todo expand this code for easier color reqognition

[CustomEditor(typeof(ColorMap))]
public class ColorDetectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myTarget = (ColorMap)target;
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.EndHorizontal();

    }
}
#endif