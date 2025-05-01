using UnityEngine;
using TMPro;
using Articy.Unity;
using Articy.Unity.Interfaces;
using System.Collections.Generic;
using Articy.Articybrothel;
using UnityEngine.UI;
using System.Linq;


public class DialogueManager : MonoBehaviour, IArticyFlowPlayerCallbacks
{
    [Header("Dialogue panel settings")]
    [SerializeField] private GameObject _dialogueWidget;
    [SerializeField] private TextMeshProUGUI _dialgueText;
    [SerializeField] private TextMeshProUGUI _npcName;

    [Space]
    [Header("Button panel settings")]
    [SerializeField] private List<Button> _buttons = new List<Button>();

    private bool _isDialogueActive;

   private ArticyFlowPlayer _flowPlayer;

    private void Start()
    {
        _flowPlayer = GetComponent<ArticyFlowPlayer>();
        _dialogueWidget.SetActive(true);
        _isDialogueActive = true;

    }

    private void Initialize() 
    {
        _isDialogueActive = true;
    }

    public void ContinueDialogue(int branchIndex) 
    {
        _flowPlayer.Play(branchIndex);
    }

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        _dialgueText.text = string.Empty;
        _npcName.text = string.Empty;

        var dialogueFragment = aObject as DialogueFragment;

        if (dialogueFragment != null)
        {
            _dialgueText.text = dialogueFragment.Text;
        }
       
        if(_dialgueText.text == "")
        {
            Debug.Log("IS NULL!");
            _flowPlayer.Play(0);
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
    }

    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
       if(aBranches.Count > 0) 
        {
            for (int i = 0; i < aBranches.Count; i++) 
            {
                _buttons[i].onClick.AddListener(() => ContinueDialogue(i));
                _buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = aBranches[i].DefaultDescription;
                _buttons[i].gameObject.SetActive(true);
            }
        }
    }
}
