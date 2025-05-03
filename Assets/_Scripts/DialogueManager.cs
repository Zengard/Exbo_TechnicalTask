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

    public void Initialize() 
    {
        _flowPlayer = GetComponent<ArticyFlowPlayer>();
    }

    public void SetUpDialogue(string eventTechnicalName)
    {
        _nextFlow = ArticyDatabase.GetObject(eventTechnicalName);

        _flowPlayer.StartOn = _nextFlow;
        _dialogueWidget.SetActive(true);
        _buttonsWidget.SetActive(true);
        _isDialogueActive = true;
    }

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {

        _dialgueText.text = string.Empty;
        _npcName.text = string.Empty;

        var dialogueFragment = aObject as DialogueFragment;

        if (dialogueFragment != null)
        {
            _dialgueText.text = dialogueFragment.Text;

            //Debug.Log(dialogueFragment.TechnicalName);
            if (dialogueFragment.TechnicalName.Contains("Ins_")) 
            {
                Debug.Log("INSTRUCTION");
            }
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
            if ((aObject as IObjectWithMenuText).MenuText != "Изменить выбор")
            {
                //Debug.Log("TEXT NULL");
                _flowPlayer.Play();
            }
        }
    }
    private void ContinueDialogue(Branch branch)
    {
        _flowPlayer.Play(branch);
    }

    private void ContinueDialogue(int branchIndex)
    {
        _flowPlayer.Play(branchIndex);
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
            if ((branch.Target is ICondition) || (branch.Target is IInstruction) || (branch.Target is IConditionEvaluator))
                    Debug.Log("INSTRUCTION");

            //Debug.Log("Branch target " + branch + " is: " + branch.Target);
            //if (branch.Target is IOutputPin)
            //{
            //    DialogueReachedEnd();
            //}
        }

        if (aBranches.Count > 0)
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
        _buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "End flow";
        _buttons[0].gameObject.SetActive(true);
    }

    private void EndDialogue() 
    {
        foreach (var button in _buttons)
            button.onClick.RemoveAllListeners();

        _dialogueWidget.SetActive(false);
        _buttonsWidget.SetActive(false);
        _flowPlayer.FinishCurrentPausedObject();
    }



    //private void TestSwitchFlow() 
    //{
    //    IArticyObject articyObject = ArticyDatabase.GetObject("Event_Lif");
    //    _flowPlayer.StartOn = articyObject;
    //    _dialogueWidget.SetActive(true);
    //    _buttonsWidget.SetActive(true);
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E) && _testBool == false) 
    //    {
    //        _testBool = true;
    //        TestSwitchFlow();
    //    }
    //}
}
