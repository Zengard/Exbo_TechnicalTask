using UnityEngine;
using TMPro;
using Articy.Unity;
using Articy.Unity.Interfaces;
using System.Collections.Generic;
using Articy.Articybrothel;
using UnityEngine.UI;


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

    private void Start()
    {
        _flowPlayer = GetComponent<ArticyFlowPlayer>();
        _dialogueWidget.SetActive(true);
        _isDialogueActive = true;
        //SetUpButtons(_buttons.Count);

    }

    private void Initialize() 
    {
        _isDialogueActive = true;
    }

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        if (aObject as DialogueFragment == null) 
        { 
            EndDialogue(); 
            return; 
        }

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
        }

        if ((aObject as IObjectWithMenuText).MenuText.Value != "")
        {
            _flowPlayer.Play();
            Debug.Log("TEXT NULL");
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
        if((branch.Target as IObjectWithMenuText).MenuText != "")
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
            if(!(branch.Target is IDialogueFragment)) 
            {
                DialogueReachedEnd();
                return;
            }    
        }

        if (aBranches.Count > 0)
        {
            for (int i = 0; i < aBranches.Count; i++)
            {
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
        _buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "->->->";
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
}
