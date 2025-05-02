using UnityEngine;
using TMPro;
using Articy.Unity;
using Articy.Unity.Interfaces;
using System.Collections.Generic;
using Articy.Articybrothel;
using UnityEngine.UI;
using Articy.Articybrothel.Features;
using Unity.VisualScripting;
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

    private bool _isDialogueActive;

   private ArticyFlowPlayer _flowPlayer;
    private bool _isReachedEnd;

    ////// below test variables
    [SerializeField] private IArticyObject _nextFlow;
    [SerializeField] private bool _testBool;

    private void Start()
    {
        _flowPlayer = GetComponent<ArticyFlowPlayer>();
        _dialogueWidget.SetActive(true);
        _isDialogueActive = true;
        //SetUpButtons(_buttons.Count);

        Entity character1 = ArticyDatabase.GetObject<Entity>("Beata");
        Entity character2 = ArticyDatabase.GetObject<Entity>("Frida");

        Debug.Log(character1.DisplayName);
        Debug.Log(character2.DisplayName);
        //ArticyGlobalVariables.Default.LoseConditions.KilledByGuz = true;

    }

    private void Initialize() 
    {
        _isDialogueActive = true;
    }

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        Debug.Log("Test Call");
        //if (aObject as DialogueFragment == null) 
        //{ 
        //    EndDialogue(); 
        //    return; 
        //}

        _dialgueText.text = string.Empty;
        _npcName.text = string.Empty;
        //foreach (var button in _buttons)
        //{
        //    button.onClick.RemoveAllListeners();
        //    button.gameObject.SetActive(false);
        //}

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

            //if(_npcName.text == "Герой") 
            //{
            //    Debug.Log("TEXT NULL");
            //    _flowPlayer.Play();
            //}
        }

        if ((aObject as IObjectWithSpeaker) != null && ((aObject as IObjectWithSpeaker).Speaker as Entity).DisplayName == "Герой")
        {
            if((aObject as IObjectWithMenuText).MenuText != "Изменить выбор") 
            {
                Debug.Log("TEXT NULL");
                _flowPlayer.Play();
            }
        }
    }

    public void ContinueDialogue(int branchIndex)
    {
        _flowPlayer.Play(branchIndex);
    }

    public void ContinueDialogue(Branch branch)
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

    private void SetUpButtons(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = i;
            _buttons[index].onClick.AddListener(() => ContinueDialogue(index + 1));
        }
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

        if (aBranches.Count > 0)
        {
            for (int i = 0; i < aBranches.Count; i++)
            {
                if (!aBranches[i].IsValid) continue;
                //Debug.Log("aBranches.Count " + aBranches.Count);
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

    private void TestSwitchFlow() 
    {
        IArticyObject articyObject = ArticyDatabase.GetObject("Event_Lif");
        _flowPlayer.StartOn = articyObject;
        _dialogueWidget.SetActive(true);
        _buttonsWidget.SetActive(true);
        //_flowPlayer.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _testBool == false) 
        {
            _testBool = true;
            TestSwitchFlow();
        }
    }

    private void OnDisable()
    {
        _isReachedEnd = false;
    }
}
