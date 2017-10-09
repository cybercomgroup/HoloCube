using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenCVForUnityExample.ColorDetection;
using UnityEngine;

public class ScanPhace : MonoBehaviour
{

	public TextMesh ProgressText;
	public ColorMap ColorMap;

	private int _progress;

	private List<GameObject> _scanFaces;

	private Dictionary<RubicColors, RubicColors> _scanningInstructions;
	
	// Use this for initialization
	void Start ()
	{
		//it's face color, and top color
		_scanningInstructions = new Dictionary<RubicColors, RubicColors>()
		{
			{RubicColors.Orange, RubicColors.White},
			{RubicColors.Blue, RubicColors.White},
			{RubicColors.Red, RubicColors.White},
			{RubicColors.Green, RubicColors.White},
			{RubicColors.White, RubicColors.Green},
			{RubicColors.Yellow, RubicColors.Orange},
		};
		_progress = 0;
		_scanFaces = new List<GameObject>();

		var keys = _scanningInstructions.Keys.ToList();
		for (int i = 0; i < 6; i++)
		{
			var obj = GameObject.Find("ScanFace" + i);
			var scanface = obj.GetComponent<ScanedFace>();
			scanface.Face.MiddleColor = keys[i];
			scanface.Face.TopColor = _scanningInstructions[keys[i]];
			scanface.Redraw();
			
			_scanFaces.Add(obj);
		}
		
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyUp("space"))
		{
			if(ScanPhaseIsDone())
			{
				FinishPhase();
				return;
			}

			var scanFace = _scanFaces[_progress].GetComponent<ScanedFace>();
			scanFace.Face.Colors = ColorMap.Face.Colors;
			scanFace.Face.MiddleColor = ColorMap.Face.MiddleColor;
			scanFace.Redraw();
			
			SetProgressText();
		}
		
		
	}

	private void FinishPhase()
	{
		print("We are done!");
	}

	private bool ScanPhaseIsDone()
	{
		return _progress == 6;
	}
	
	private void SetProgressText()
	{
		ProgressText.text = string.Format("{0}/6",++_progress);
	}
}
