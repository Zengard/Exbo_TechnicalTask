using UnityEngine;
using TMPro;
using Articy.Unity;
using Articy.Unity.Interfaces;
using System.Collections.Generic;
using Articy.Articybrothel;
using UnityEngine.UI;
using Articy.Articybrothel.Features;
using Articy.Articybrothel.GlobalVariables;


public class DialogueManager : MonoBehaviour, IArticyFlowPlayerCallbacks
{
 
    DialogueEventManager _dialogueEventManager;

    [Header("Dialogue panel settings")]
    [SerializeField] private GameObject _dialogueWidget;
    [SerializeField] private TextMeshProUGUI _dialgueText;
    [SerializeField] private TextMeshProUGUI _npcName;

    [Space]
    [Header("Button panel settings")]
    [SerializeField] private GameObject _buttonsWidget;
    [SerializeField] private List<Button> _buttons = new List<Button>();

    private ArticyFlowPlayer _flowPlayer;
    private IArticyObject _nextFlow;
    private bool _isDialogueActive;

    //Getters
    public bool IsDialogueActive { get { return _isDialogueActive; }}

    ////// below test variables
    [SerializeField] private bool _testBool;

    List<IOutputPin> outputs;

    public void Initialize(DialogueEventManager eventManager) 
    {
        _flowPlayer = GetComponent<ArticyFlowPlayer>();
        _dialogueEventManager = eventManager;
    }

    public void SetUpDialogue(string eventTechnicalName)
    {
        _isDialogueActive = true;
        _nextFlow = ArticyDatabase.GetObject(eventTechnicalName);

        _buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "";
        _flowPlayer.StartOn = _nextFlow;
        _dialogueWidget.SetActive(true);
        _buttonsWidget.SetActive(true);
        
    }

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        if (aObject as Instruction != null) 
        {
            //Debug.Log("InputPins: " + (aObject as Instruction).InputPins);
            Debug.Log("OutPutPins[0]: " + (aObject as Instruction).OutputPins[0]);
            outputs =  (aObject as Instruction).GetOutputPins();

            foreach (var pin in outputs)
            {
                {
                    Debug.Log(outputs);
                }
            }
            }

            _dialgueText.text = string.Empty;
        _npcName.text = string.Empty;

        var dialogueFragment = aObject as DialogueFragment;

        if (dialogueFragment != null)
        {
            _dialgueText.text = dialogueFragment.Text;
        }
       
        var objectWithSpeaker = aObject as IObjectWithSpeaker;
       
        if (objectWithSpeaker != null) 
        {
            var speakerEntity = objectWithSpeaker.Speaker as Entity;
            
            if (speakerEntity != null) 
            {
                _npcName.text = speakerEntity.DisplayName;
            }
        }

        if ((aObject as IObjectWithSpeaker) != null && ((aObject as IObjectWithSpeaker).Speaker as Entity).DisplayName == "Герой")
        {
            if ((aObject as IObjectWithMenuText).MenuText != "Изменить выбор")// it would be better to use universal TechnicalName
            {
                _flowPlayer.Play();
            }
        }
    }
    private void ContinueDialogue(Branch branch)
    {
        _flowPlayer.Play(branch);
    }

    private void SetUpTheButton(Button button, Branch branch)
    { 

        if((branch.Target as IObjectWithMenuText) != null && (branch.Target as IObjectWithMenuText).MenuText != "")
             button.GetComponentInChildren<TextMeshProUGUI>().text = (branch.Target as IObjectWithMenuText).MenuText;
        else
            _buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "->->->";


        button.onClick.AddListener(() => ContinueDialogue(branch));
        button.gameObject.SetActive(true);
    }

    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
        
        foreach (var button in _buttons)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }

        foreach (var branch in aBranches)
        {
            Debug.Log("Branch target " + branch + " is: " + branch.Target);
            if (branch.Target is IOutputPin)
            {
                DialogueReachedEnd();
            }
        }

        if (aBranches.Count > 0 && _buttons[0].GetComponentInChildren<TextMeshProUGUI>().text != "Закончить диалог")
        {
            for (int i = 0; i < aBranches.Count; i++)
            {
                if (!aBranches[i].IsValid) continue;
                SetUpTheButton(_buttons[i], aBranches[i]);
            }
        }
    }

    private void DialogueReachedEnd() 
    {
        foreach(var button in _buttons)
            button.onClick.RemoveAllListeners();

        _buttons[0].onClick.AddListener(EndDialogue);
        _buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Закончить диалог";
        _buttons[0].gameObject.SetActive(true);
    }

    private void EndDialogue() 
    {
        foreach (var button in _buttons)
            button.onClick.RemoveAllListeners();

        _dialogueWidget.SetActive(false);
        _buttonsWidget.SetActive(false);
        _isDialogueActive = false;
        _flowPlayer.FinishCurrentPausedObject();

        _dialogueEventManager.DialogueEnded();
    }
}
