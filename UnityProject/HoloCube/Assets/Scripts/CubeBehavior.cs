using System.Collections.Generic;
using Backend;
using UnityEditor.Animations;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    public CubeHudMap CubeHudMap;

    private Cube _cube;

    private Dictionary<int,Face> _faces = new Dictionary<int, Face>();
    
    private static Color _yellow = new Color(1.0f, 1.0f, 0.0f);
    private static Color _orange = new Color(1f, 0.39f, 0f);
    private static Color _white = new Color(1.0f, 1.0f, 1.0f);
    
    private static Color _green = new Color(0.0f, 1.0f, 0.0f);
    private static Color _red = new Color(1.0f, 0.0f, 0.0f);
    private static Color _blue = new Color(0.0f, 0.0f, 1.0f);


    // Use this for initialization
    void Start()
    {
        _cube = new Cube();
        
        SetColor(_cube.Front,_yellow);
        SetColor(_cube.Top,_orange);
        SetColor(_cube.Left,_white);
        SetColor(_cube.Right,_green);
        SetColor(_cube.Back,_red);
        SetColor(_cube.Bottom,_blue);

        
        _faces.Add(0,_cube.Front);
        _faces.Add(1,_cube.Left);
        _faces.Add(2,_cube.Bottom);
        _faces.Add(3,_cube.Right);
        _faces.Add(4,_cube.Top);
        _faces.Add(5,_cube.Back);
      
        
        CubeHudMap.Cube = _cube;
        CubeHudMap.Draw();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);

            if (child.name == "Front")
                child.gameObject.GetComponent<Renderer>().material.mainTexture =
                    CubeHudMap.DrawFaceTexture(_cube.Front, 10);
        }
    }

    static string[] options = new string[] {"Front", "Left", "Bottom", "right", "Top", "Back"};
    static Rect position = new Rect(10, 10, 400, 20);
    int selected = 0;

    private void OnGUI()
    {
        selected = GUI.SelectionGrid(position, selected, options, options.Length, GUI.skin.toggle);

        if (GUI.Button(new Rect(10, 50, 150, 100), "Rotate Front"))
        {
            _cube.MoveFace(false, _faces[selected]);
            CubeHudMap.Draw();
        }
        if (GUI.Button(new Rect(10, 160, 150, 100), "Rotate Front Inverse"))
        {
            _cube.MoveFace(true, _faces[selected]);
            CubeHudMap.Draw();
        }
        if (GUI.Button(new Rect(10, 320, 150, 100), "test"))
        {
            var masterFace = _faces[selected];
            var right = CubeNavigationHelper.GetFaceRightOfCurrentFace(masterFace, _cube);
            var left = CubeNavigationHelper.GetFaceLeftOfCurrentFace(masterFace, _cube);
            var top = CubeNavigationHelper.GetFaceUpOfCurrentFace(masterFace, _cube);
            var bottom = CubeNavigationHelper.GetFaceDownOfCurrentFace(masterFace, _cube);
            _cube.MoveFace(masterFace, right, left, top, bottom);
            CubeHudMap.Draw();
        }
    }
    

    private static void SetColor(Face face, Color color)
    {
        face.Colors = new Color[3, 3];
        
        
        face.Colors[0, 0] = color;
        face.Colors[0, 1] = color;
        face.Colors[0, 2] = color;
       
        face.Colors[1, 0] = color;
        face.Colors[1, 1] = color;
        face.Colors[1, 2] = color;
        
        face.Colors[2, 0] = color;
        face.Colors[2, 1] = color;
        face.Colors[2, 2] = color;
       
        /*
           face.Colors[0, 0] = new Color(1.0f, 1.0f, 0.0f);
        face.Colors[0, 1] = new Color(1f, 0.39f, 0f);
        face.Colors[0, 2] = new Color(0.0f, 0.0f, 1.0f);


        face.Colors[1, 0] = new Color(1.0f, 0.0f, 0.0f);
        face.Colors[1, 1] = new Color(1f, 1.0f, 1.0f);
        face.Colors[1, 2] = new Color(0.0f, 0.0f, 1.0f);


        face.Colors[2, 0] = new Color(1.0f, 1.0f, 0.0f);
        face.Colors[2, 1] = new Color(0.0f, 1.0f, 0.0f);
        face.Colors[2, 2] = new Color(1f, 0.39f, 0f);
           
          */
        
        
    }
}