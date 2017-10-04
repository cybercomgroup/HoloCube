using System;
using System.Collections;
using System.Collections.Generic;
using OpenCVForUnity;
using OpenCVForUnityExample;
using UnityEngine;

public class DisplayMat : MonoBehaviour
{
	public WebCamTextureToMat WebCamTextureToMat;
	public ColorMap ColorMap;

	public int MinCubeX = 186;
	public int MaxCubeX = 500;

	public int MinCubeY = 63;
	public int MaxCubeY = 380;

	public List<Double[]> Colors;
	
	
	private Texture2D _texture;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
		var mat = WebCamTextureToMat.GetMat();
		if(mat == null) return;

		var dst = mat.clone();
		
		var color = new Scalar(0,255,0);
//		Imgproc.line(dst,new Point(MinCubeX,MaxCubeY),new Point(MinCubeX,MinCubeY),color,5);
//		Imgproc.line(dst,new Point(MaxCubeX,MaxCubeY),new Point(MaxCubeX,MinCubeY),color,5);

		var cubeWidth = MaxCubeX - MinCubeX;
		var cubeHeight = MaxCubeY - MinCubeY;

		var piceWidth = cubeWidth / 3;
		var piceHeight = cubeHeight / 3;

		for (int i = 0; i < 3; i++)
		{
			Imgproc.line(dst,new Point(MinCubeX+(piceWidth*i),MaxCubeY),new Point(MinCubeX+(piceWidth*i),MinCubeY),color,5);
		}
		for (int i = 0; i < 3; i++)
		{
			Imgproc.line(dst,new Point(MinCubeX,MinCubeY+(piceHeight*i)),new Point(MaxCubeX,MinCubeY+(piceHeight*i)),color,5);
		}

		Colors = new List<double[]>();
		for (int x = 0; x < 3; x++)
		for (int y = 0; y < 3; y++)
		{
			var col = mat.get(MinCubeY+(piceHeight*y + (piceHeight/2)), MinCubeX+(piceWidth*x+ (piceWidth/2)));
			Colors.Add(col);
		}
		
		
		ColorMap.Colors = Colors;
		ColorMap.Redraw();

		_texture = new Texture2D(mat.width(), mat.height());
		

		Utils.matToTexture2D(dst,_texture);
		
		GetComponent<Renderer>().material.mainTexture = _texture;
		GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Transparent");
	}
}
