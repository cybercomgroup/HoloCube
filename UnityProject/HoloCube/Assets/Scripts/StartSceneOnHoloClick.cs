using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneOnHoloClick : MonoBehaviour,IInputClickHandler
{
	public List<SceneField> MySceneAssets;

	public void OnInputClicked(InputClickedEventData eventData)
	{
		SceneManager.LoadScene(MySceneAssets[0].SceneName);
	}
}
