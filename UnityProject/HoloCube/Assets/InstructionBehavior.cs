using System.Collections;
using System.Collections.Generic;
using ColorThings;
using UnityEngine;

public class InstructionBehavior : MonoBehaviour
{

	public TextMesh TextMesh;
	public MeshRenderer FacingColor;
	public SpriteRenderer Arrow;

	private int _index;
	public List<object> ListOfMoves{ get; set; }
	
	//for debug;
	public RubicColors Color;
	public bool Inverse;
	public bool DoublMove;
	
	private void OnGUI()
	{
		if (GUI.Button(new Rect(0,0,100,20), "SetNewInstruction"))
		{
			SetNewInstruction(Color,Inverse,DoublMove);
		}
	}

	private void Start()
	{
		FacingColor.material.shader = Shader.Find("Unlit/Color");
	}

	private void Update()
	{
		if(Input.GetKeyUp("space"))
		{
			NextMove();
		}
	}


	public void NextMove()
	{
		var move = ListOfMoves[_index++];
//		SetNewInstruction();
	}

	public void SetNewInstruction(RubicColors facingColor, bool inverse = false, bool doubleMove = false)
	{
		FacingColor.material.color = ColorDetection.UnityColorFromEnum(facingColor);
		Arrow.flipX = inverse;
		TextMesh.text = doubleMove ? "180°" : "90°";
	}
}
