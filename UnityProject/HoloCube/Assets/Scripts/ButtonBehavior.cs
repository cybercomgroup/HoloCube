using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour, IInputClickHandler
{
	private bool _hasBeenTriggerd;

	
	public void OnInputClicked(InputClickedEventData eventData)
	{
		_hasBeenTriggerd= true;
	}
	
	
	public bool HasBeenTriggered()
	{
		var toReturn = _hasBeenTriggerd;
		_hasBeenTriggerd = false;
		return toReturn;
	}
	
	
	
}
