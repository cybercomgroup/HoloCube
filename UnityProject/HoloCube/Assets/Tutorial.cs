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

    public TextMesh TextHelperForEditor;

    //Instructions view
    public GameObject InstructionsPrefab;

    private InstructionBehavior _instructionBehavior;

    private List<Instruction> _instructions;
    private int _currentVoiceIndex;

    private const string MoveForwardKeyword = "Continue";
    private const string MoveBackKeyword = "Go back";
    private const string RepeatKeyword = "Repeat";

    // Use this for initialization
    void Start()
    {
        _instructionBehavior = InstructionsPrefab.GetComponent<InstructionBehavior>();
        _instructionBehavior.IsTutorial = true;

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
        
        var introInstructions = new List<Instruction>
        {
            new Instruction("Hello user, my name is Zira and I will teach you how to use HoloCube."),
            new Instruction(string.Format("To move forwards in this program you need to say {0}.", mvk)),
            new Instruction(string.Format("Go ahead, try it out and say {0}", mvk),true),
            new Instruction("Good, try saying it again.",true),
            new Instruction("There you go. Great work."),
            new Instruction(string.Format("If you need to hear the instruction again, say {0}", rk)),
            new Instruction(string.Format("You could also say, {0}, to get the previously instruction", mbk)),
            new Instruction(string.Format("If you are ready to continue, say {0}", mvk),true)
        };

        var instructionsView = new List<Instruction>
        {
            new Instruction("This is the instrcution view, this is how you will be instructed to move the cube.",
                false,() => InstructionsPrefab.SetActive(true)),
            new Instruction("You see the red color in the middle."),
            new Instruction("The one that now is green", false,
                () => _instructionBehavior.SetNewInstruction(RubicColors.Green)),
            new Instruction("And now oranage", false,
                () => _instructionBehavior.SetNewInstruction(RubicColors.Orange)),
            new Instruction("That is a 2D representation of the cube, now it shows you to hold to cube so that the orange side is facing towards you"),
            new Instruction("The arrow that is now facing clockwise, show in what diraction you should turn the front face.",false, () => _instructionBehavior.SetNewInstruction(RubicColors.Orange)),
            new Instruction("In this case, you should turn it 90 degres clockwise"),
            new Instruction("And in this case, you should turn it 90 degres anti-clockwise", false,() => _instructionBehavior.SetNewInstruction(RubicColors.Orange,true)),
            new Instruction("All turns is either 90 or 180, so look at what is says before executing you move"),
            new Instruction(string.Format("Did you get that? If so, say {0}",mvk),true)
        };

        var scanningPhaseInstructionsViews = new List<Instruction>
        {
            new Instruction("This is the view where you will scan the cube with HoloLens",false,() => SceneManager.LoadScene("ScanCube")),
            new Instruction("We have not yet implimentet anything more here at the moment",true)
        };
        
        
        var allInstructions = new List<Instruction>();
        allInstructions.AddRange(introInstructions);
        allInstructions.AddRange(instructionsView);
        allInstructions.AddRange(scanningPhaseInstructionsViews);
    
        return allInstructions;
    }

    #endregion

    private class Instruction
    {
        public string Text { get; private set; }
        public Action ActionToInvoke { get; private set; }
        public bool NeedsUserConfirmation { get; private set; }

        public Instruction(string text, bool needsUserConfirmation = false, Action actionToInvoke = null)
        {
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
        TextHelperForEditor.text = instruction.Text.Replace(".", "\n").Replace(",", "\n");

        if (instruction.ActionToInvoke == null)
        {
            TextToSpeech.StartSpeaking(instruction.Text);
            if(instruction.NeedsUserConfirmation) return;
            
            StartCoroutine(WaitUntilSoundIsDone());
            return;
        }

        instruction.ActionToInvoke.Invoke();
        TextToSpeech.StartSpeaking(instruction.Text);

        if (instruction.NeedsUserConfirmation) return;

        StartCoroutine(WaitUntilSoundIsDone());
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