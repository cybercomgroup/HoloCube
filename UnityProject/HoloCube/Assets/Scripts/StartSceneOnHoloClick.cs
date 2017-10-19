using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneOnHoloClick : MonoBehaviour,IInputClickHandler
{
	public SceneField MySceneAssets;

	public void OnInputClicked(InputClickedEventData eventData)
	{
		SceneManager.LoadScene(MySceneAssets.SceneName);
	}
}
