using Articy.Articybrothel;
using Articy.Articybrothel.GlobalVariables;
using Articy.Unity;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResoursesTracker : MonoBehaviour
{
    DialogueEventManager _dialogueEventManager;
    string _resourceUpdateText;

    private int _food;
    private int _money;
    private int _sanitary;
  
    public void Initialize(DialogueEventManager eventManager)
    {
        _food = ArticyGlobalVariables.Default.Resources.Food;
        _money = ArticyGlobalVariables.Default.Resources.Money;
        _sanitary = ArticyGlobalVariables.Default.Resources.Sanitary;

        _dialogueEventManager = eventManager;
        ArticyDatabase.DefaultGlobalVariables.Notifications.AddListener("Resources.*", OnFoodChanged);
        _dialogueEventManager.OnUpdateDialogueEntities.AddListener(SendToDialogue);

    }

    private void OnFoodChanged(string aVariable, object aValue)
    {     
        switch (aVariable) 
        {
            case "Resources.Food":
                if((int)aValue > _food) 
                {
                    _resourceUpdateText += "\n" + "+" +((int)aValue - _food) + " Еда";                   
                }
                else
                {
                    _resourceUpdateText += "\n" + "-" + (_food - (int)aValue) + " Еда";
                }
                _food = (int)aValue;
                break;

            case "Resources.Money":
                if ((int)aValue > _money)
                {
                    _resourceUpdateText += "\n" + "+" + ((int)aValue - _money ) +" Деньги";
                }
                else
                {
                    _resourceUpdateText += "\n" + "-" + (_money - (int)aValue) + " Деньги";
                }
                _money = (int)aValue;
                break;

            case "Resources.Sanitary":
                if ((int)aValue > _sanitary)
                {
                    _resourceUpdateText += "\n" + "+" + ((int)aValue - _sanitary) + " Санитария";
                }
                else
                {
                    _resourceUpdateText += "\n" + "-" + (_sanitary - (int)aValue) + " Санитария";
                }
                _sanitary = (int)aValue;
                break;
        }

       
       
    }

    private void SendToDialogue(TextMeshProUGUI dialogue) 
    {
         dialogue.text += _resourceUpdateText;
        _resourceUpdateText = null;
    }

}
