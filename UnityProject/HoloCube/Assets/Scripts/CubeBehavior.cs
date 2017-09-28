using System.Collections;
using System.Collections.Generic;
using Backend;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
	public CubeHudMap CubeHudMap;

	private Cube _cube;
	
	
	// Use this for initialization
	void Start ()
	{
		_cube = new Cube();
		
		SetColor(_cube.Front);
		SetColor(_cube.Top);
		SetColor(_cube.Left);
		SetColor(_cube.Right);
		SetColor(_cube.Back);
		SetColor(_cube.Bottom);

		CubeHudMap.Cube = _cube;
		CubeHudMap.Draw();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private static void SetColor(Face face)
	{
		face.Colors = new Color[3, 3];
		face.Colors[0, 0] = new Color(1.0f, 1.0f, 0.0f);
		face.Colors[0, 1] = new Color(1f, 0.39f, 0f);
		face.Colors[0, 2] = new Color(0.0f, 0.0f, 1.0f);


		face.Colors[1, 0] = new Color(1.0f, 0.0f, 0.0f);
		face.Colors[1, 1] = new Color(1f, 1.0f, 1.0f);
		face.Colors[1, 2] = new Color(0.0f, 0.0f, 1.0f);


		face.Colors[2, 0] = new Color(1.0f, 1.0f, 0.0f);
		face.Colors[2, 1] = new Color(0.0f, 1.0f, 0.0f);
		face.Colors[2, 2] = new Color(1f, 0.39f, 0f);
	}

	
	
	
}
