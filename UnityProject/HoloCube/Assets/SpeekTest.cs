
using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.UI;

public class SpeekTest : MonoBehaviour
{

	public ButtonBehavior David;
	public ButtonBehavior Default;
	public ButtonBehavior Zira;
	public ButtonBehavior Mark;
	
	public InputField InputField;

	public TextToSpeech TextToSpeech;
	
	// Update is called once per frame
	void Update ()
	{
		var text = InputField.text;
		
		if (David.HasBeenTriggered())
		{
			SpeekText(TextToSpeechVoice.David,text);
			
		}
		if (Default.HasBeenTriggered())
		{
			SpeekText(TextToSpeechVoice.Default,text);
			
		}
		if (Zira.HasBeenTriggered())
		{
			SpeekText(TextToSpeechVoice.Zira,text);
			
		}
		if (Mark.HasBeenTriggered())
		{
			SpeekText(TextToSpeechVoice.Mark,text);
		}
	}

	private void SpeekText(TextToSpeechVoice voice, string text)
	{
		TextToSpeech.Voice = voice;	
		TextToSpeech.StartSpeaking(text);
	}
}
