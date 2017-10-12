using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Color;
using OpenCVForUnity;
using UnityEngine;

public class ScanPhace : MonoBehaviour
{

	public TextMesh ProgressText;
	public ColorMap ColorMap;
	#if UNITY_EDITOR
	public bool EnforceSecureity = true;
	#endif

	private int _progress;

	private List<GameObject> _scanFaces;

	private List<ScanningInstruction> _scanningInstructions;
	private bool _redoScan;
	
	// Use this for initialization
	void Start ()
	{
		_scanningInstructions = new List<ScanningInstruction>
		{
			new ScanningInstruction(RubicColors.Orange, RubicColors.White),
			new ScanningInstruction(RubicColors.Blue, RubicColors.White),
			new ScanningInstruction(RubicColors.Red, RubicColors.White),
			new ScanningInstruction(RubicColors.Green, RubicColors.White),
			new ScanningInstruction(RubicColors.White, RubicColors.Green),
			new ScanningInstruction(RubicColors.Yellow, RubicColors.Orange),
		};
		
		_progress = 0;
		_scanFaces = new List<GameObject>();

		for (int i = 0; i < 6; i++)
		{
			var obj = GameObject.Find("ScanFace" + i);
			var scanface = obj.GetComponent<ScanedFace>();
			
			scanface.Face.MiddleColor = _scanningInstructions[i].FaceColor;
			scanface.Face.TopColor = _scanningInstructions[i].TopColor;
			scanface.Redraw();
			
			_scanFaces.Add(obj);
		}
		
		
	}

	private void Reset()
	{
		_progress = 0;
		
		for (int i = 0; i < 6; i++)
		{
			var obj = GameObject.Find("ScanFace" + i);
			var scanface = obj.GetComponent<ScanedFace>();
			scanface.Face.MiddleColor = _scanningInstructions[i].FaceColor;
			scanface.Face.TopColor = _scanningInstructions[i].TopColor;
			scanface.Reset();
			
			SetProgressText();
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

#if UNITY_EDITOR
			if(EnforceSecureity)
#endif
			if(ColorMap.Face.MiddleColor != _scanningInstructions[_progress].FaceColor)
			{
				print("That is not the exptected face color");
				return;
			}
			
			scanFace.Face.Colors = ColorMap.Face.Colors;
			scanFace.Face.MiddleColor = ColorMap.Face.MiddleColor;
			scanFace.Redraw();
			
			_progress++;
			SetProgressText();
		}
		
		
	}

	private void FinishPhase()
	{
		var scannedFaces = _scanFaces.Select(gObj => gObj.GetComponent<ScanedFace>());
		var colors = scannedFaces.SelectMany(face => face.Face.Colors);
		var colorsGroup = colors.GroupBy(color => color).ToList();
		
		foreach (var kvp in colorsGroup)
		{
			if (kvp.ToList().Count == 8) continue;
			
			Debug.Log(string.Format("Key:{0}, found count:{1} Expected 8 (+ face)", kvp.Key, kvp.ToList().Count));
			_redoScan = true;
		}
		if (_redoScan)
		{
			Reset();
			return;
		}

		print("We are done!");
	}

	private bool ScanPhaseIsDone()
	{
		return _progress == 6;
	}
	
	private void SetProgressText()
	{
		ProgressText.text = string.Format("{0}/6",_progress);
	}

	class ScanningInstruction
	{
		public RubicColors FaceColor;
		public RubicColors TopColor;

		public ScanningInstruction(RubicColors faceColor, RubicColors topColor)
		{
			FaceColor = faceColor;
			TopColor = topColor;
		}
	}
}
