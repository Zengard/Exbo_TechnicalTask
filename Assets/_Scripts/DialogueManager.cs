using UnityEngine;
using TMPro;
using Articy.Unity;
using Articy.Unity.Interfaces;
using System.Collections.Generic;
using Articy.Articybrothel;


public class DialogueManager : MonoBehaviour, IArticyFlowPlayerCallbacks
{
    [Header("Dialogue panel settings")]
    [SerializeField] private GameObject _dialogueWidget;
    [SerializeField] private TextMeshProUGUI _dialgueText;
    [SerializeField] private TextMeshProUGUI _npcName;

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

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        _dialgueText.text = string.Empty;
        _npcName.text = string.Empty;

        var dialogueFragment = aObject as DialogueFragment;

        if (dialogueFragment != null)     
        {
            _dialgueText.text  = dialogueFragment.Text;
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
       
    }
}
