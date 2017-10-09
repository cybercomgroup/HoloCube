using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScanPhace : MonoBehaviour
{

	public TextMesh ProgressText;
	public ColorMap ColorMap;

	private int _progress;

	private List<GameObject> _scanFaces;
	
	// Use this for initialization
	void Start ()
	{
		_progress = 0;
		_scanFaces = new List<GameObject>();

		for (int i = 0; i < 6; i++)
		{
			var scanFace = GameObject.Find("ScanFace" + i);
			_scanFaces.Add(scanFace);
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

		for (int i = 0; i < _scanFaces.Count; i++)
		{
			var scannedFace = _scanFaces[i].GetComponent<ScanedFace>();
			var face = scannedFace.Face;
			var colors = face.Colors.Select(e => e.ToString()).ToList();
			var middle = face.MiddleColor.ToString();
			var top = face.TopColor;
		}
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
