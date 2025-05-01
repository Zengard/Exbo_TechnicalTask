using UnityEngine;
using TMPro;
using Articy.Unity;
using Articy.Unity.Interfaces;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor.Rendering;
using Articy.Articybrothel;


public class DialogueManager : MonoBehaviour, IArticyFlowPlayerCallbacks
{
    [SerializeField] private GameObject _dialogueWidget;
    [SerializeField] private TextMeshProUGUI _dialgueText;
    //[SerializeField] private Text _simpleText;
    private bool _isDialogueActive;

  [SerializeField] private ArticyFlowPlayer _flowPlayer;

    [Space]
    [Space]

    public ArticyRef myRef;

    private void Start()
    {
       // _flowPlayer = GetComponent<ArticyFlowPlayer>();
        _dialogueWidget.SetActive(true);
        _isDialogueActive = true;

        if (myRef.HasReference)
        {
            var obj = myRef.GetObject();
        }
    }

    private void Initialize() 
    {
        _isDialogueActive = true;
    }

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        _dialgueText.text = string.Empty;
        Debug.Log(aObject);
        var objectWithText = aObject as DialogueFragment;
        Debug.Log(objectWithText);
        if (objectWithText != null)     
        {
            _dialgueText.text = objectWithText.Text;
        }
    }

    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
       
    }
}
