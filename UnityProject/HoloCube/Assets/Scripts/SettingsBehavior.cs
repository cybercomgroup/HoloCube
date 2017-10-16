using System.Collections;
using System.Collections.Generic;
using Backend;
using UnityEngine;

public class SettingsBehavior : MonoBehaviour
{
    public ButtonBehavior VoiceComs;
    public ButtonBehavior Timer;
    public ButtonBehavior Tutorial;

    public TextMesh VoiceStatus;
    public TextMesh TimerStatus;
    public TextMesh TutorialStatus;


    private void Start()
    {
        UpdateTimerStatus();
        UpdateTutorialStatus();
        UpdateVoiceStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (VoiceComs.HasBeenTriggered())
        {
            Settings.UseVoice = !Settings.UseVoice;
            UpdateVoiceStatus();
        }

        if (Timer.HasBeenTriggered())
        {
            Settings.UseTimer = !Settings.UseTimer;
            UpdateTimerStatus();
        }

        if (Tutorial.HasBeenTriggered())
        {
            Settings.UseTutorial = !Settings.UseTutorial;
            UpdateTutorialStatus();
        }
    }

    private void UpdateVoiceStatus()
    {
        VoiceStatus.text = Settings.UseVoice ? "On" : "Off";
        print("Voice was toggled");
    }

    private void UpdateTimerStatus()
    {
        TimerStatus.text = Settings.UseTimer ? "On" : "Off";
        print("Timer was toggled");

    }

    private void UpdateTutorialStatus()
    {
        TutorialStatus.text = Settings.UseTutorial ? "On" : "Off";
        print("Tutorial was toggled");
    }
}