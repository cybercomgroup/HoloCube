using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ColorThings;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour, ISpeechHandler
{
    public TextToSpeech TextToSpeech;

    public GameObject InstructionsPrefab;
    public TextMesh TextHelperForEditor;


    private List<Instruction> _instructions;
    private int _currentVoiceIndex;

    private const string MoveForwardKeyword = "Continue";
    private const string MoveBackKeyword = "Go back";
    private const string RepeatKeyword = "Repeat";

    // Use this for initialization
    void Start()
    {
        InstructionsPrefab.GetComponent<InstructionBehavior>().IsTutorial = true;

        TextToSpeech.Voice = TextToSpeechVoice.Zira;

        InputManager.Instance.AddGlobalListener(gameObject);

        _instructions = GetInstructions();
        Speek(_currentVoiceIndex++);

#if UNITY_EDITOR
        TextHelperForEditor.gameObject.SetActive(true);
#else
        TextHelperForEditor.gameObject.SetActive(false);
#endif
    }

    #region instructions

    private List<Instruction> GetInstructions()
    {
        var mvk = MoveForwardKeyword;
        var mbk = MoveBackKeyword;
        var rk = RepeatKeyword;

        return new List<Instruction>
        {
            new Instruction("Hello user, my name is Zira and I will teach you how to use HoloCube.", false),
            new Instruction(string.Format("To move forwards in this program you need to say {0}.", mvk), false),
            new Instruction(string.Format("Go ahead, try it out and say {0}", mvk)),
            new Instruction("Good, try saying it again."),
            new Instruction("There you go. Great work.", false),
            new Instruction(string.Format("If you need to hear the instruction again, say {0}", rk), false),
            new Instruction(string.Format("You could also say, {0}, to get the previously instruction", mbk), false),
            new Instruction(string.Format("If you are ready to continue, say {0}", mvk)),
            new Instruction("This is the instrcution view, this is how you will be instructed to move the cube.", false,
                () => { InstructionsPrefab.SetActive(true); }),
            new Instruction("The red color that is the middle", false),
            new Instruction("The one that now is green", false,
                () => { InstructionsPrefab.GetComponent<InstructionBehavior>().SetNewInstruction(RubicColors.Green); },
                true),
            new Instruction("That is the facing color. In this case, the middle pice of the cube should be green",
                false, () =>
                {
                 SceneManager.LoadScene("ScanCube");   
                }),
        };
    }

    #endregion

    private class Instruction
    {
        public string Text { get; private set; }
        public Action ActionToInvoke { get; private set; }
        public bool NeedsUserConfirmation { get; private set; }
        public bool InvokeActionInBegining { get; private set; }

        public Instruction(
            string text,
            bool needsUserConfirmation = true,
            Action actionToInvoke = null,
            bool invokeActionInBegining = false
        )
        {
            InvokeActionInBegining = invokeActionInBegining;
            Text = text;
            ActionToInvoke = actionToInvoke;
            NeedsUserConfirmation = needsUserConfirmation;
        }
    }

    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        switch (eventData.RecognizedText)
        {
            case MoveForwardKeyword:
                Speek(_currentVoiceIndex++);
                break;
            case MoveBackKeyword:
                Speek(_currentVoiceIndex--);
                break;
            case RepeatKeyword:
                Speek(_currentVoiceIndex);
                break;
        }
    }

    private void Speek(int index)
    {
        var instruction = GetCurrentInscrucuton(index);
        TextHelperForEditor.text = instruction.Text.Replace(".", "\n");

        if (instruction.ActionToInvoke != null && instruction.InvokeActionInBegining)
        {
            if(instruction.InvokeActionInBegining)
            {
                instruction.ActionToInvoke.Invoke();
                TextToSpeech.StartSpeaking(instruction.Text);

                if (!instruction.NeedsUserConfirmation) StartCoroutine(WaitUntilSoundIsDone());
                return;    
            }
            
            if(!instruction.InvokeActionInBegining)
            {
                
            }
            
        }

        TextToSpeech.StartSpeaking(instruction.Text);

        if (instruction.ActionToInvoke == null)
        {
            if (instruction.NeedsUserConfirmation) return;
            StartCoroutine(WaitUntilSoundIsDone());
            return;
        }
        instruction.ActionToInvoke.Invoke();
    }

    private Instruction GetCurrentInscrucuton(int index)
    {
        Instruction inscrucuton;
        try
        {
            inscrucuton = _instructions[index];
        }
        catch (Exception)
        {
            inscrucuton = _instructions.Last();
        }
        return inscrucuton;
    }

    private IEnumerator WaitUntilSoundIsDone()
    {
#if UNITY_EDITOR
        int index = 0;
        while (index < 5)
        {
            index++;
            yield return new WaitForSeconds(0.5f);
        }
#else
            yield return new WaitWhile(() => TextToSpeech.SpeechTextInQueue() || TextToSpeech.IsSpeaking());
#endif

        Speek(_currentVoiceIndex++);
    }
}