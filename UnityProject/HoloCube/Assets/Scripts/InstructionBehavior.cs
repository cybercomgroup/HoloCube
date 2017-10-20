using System.Collections;
using System.Collections.Generic;
using ColorThings;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionBehavior : MonoBehaviour
{

	public TextMesh TextMesh;
	public MeshRenderer FacingColor;
	public SpriteRenderer Arrow;
	public SceneField NextScene;

	[HideInInspector]
	public bool IsTutorial;

	private int _index;
	public List<object> ListOfMoves{ get; set; }
	
	//for debug;
	public RubicColors Color;
	public bool Inverse;
	public bool DoublMove;

	private void Start()
	{
		ListOfMoves = new List<object>();
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(0,0,100,20), "SetNewInstruction"))
		{
			SetNewInstruction(Color,Inverse,DoublMove);
		}
	}


	private void Update()
	{
		if(IsTutorial)return;
		
		if(Input.GetKeyUp("space"))
		{
			NextMove();
		}
	}


	public void NextMove()
	{

		if (_index == ListOfMoves.Count)
		{
			SceneManager.LoadScene(NextScene.SceneName);
			return;
		}
		
		var move = ListOfMoves[_index++];
//		SetNewInstruction();
	}

	public void SetNewInstruction(RubicColors facingColor, bool inverse = false, bool doubleMove = false)
	{
		FacingColor.material.color = ColorDetection.UnityColorFromEnum(facingColor);
		Arrow.flipX = !inverse;
		TextMesh.text = doubleMove ? "180°" : "90°";
	}
}
