using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneOnHoloClick : MonoBehaviour,IInputClickHandler
{
	public SceneField mySceneAssets;

	public void OnInputClicked(InputClickedEventData eventData)
	{
		SceneManager.LoadScene(mySceneAssets.SceneName);
	}
}
