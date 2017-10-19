using Backend;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartProgramButton : MonoBehaviour,IInputClickHandler {

	public SceneField AdjustHeadsetScene;
	public SceneField CubeScanScene;
	
	public void OnInputClicked(InputClickedEventData eventData)
	{
		SceneManager.LoadScene(Settings.UseTutorial ? AdjustHeadsetScene : CubeScanScene);
	}
}
