using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ReColor : MonoBehaviour, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("You clicked me") ;
        Debug.Log("" + GetComponent<GameObject>().name);
    }
}
