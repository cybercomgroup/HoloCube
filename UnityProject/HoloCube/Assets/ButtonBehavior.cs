using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour, IInputClickHandler
{
	private bool hasBeenTriggerd;

	
	public void OnInputClicked(InputClickedEventData eventData)
	{
		hasBeenTriggerd= true;
	}
	
	
	public bool HasBeenTriggered()
	{
		var toReturn = hasBeenTriggerd;
		hasBeenTriggerd = false;
		return toReturn;
	}
	
	
	
}
