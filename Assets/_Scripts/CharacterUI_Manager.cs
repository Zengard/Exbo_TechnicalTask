using Articy.Articybrothel;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI_Manager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _charactersUI = new List<GameObject>();
    //[SerializeField] private List<GameObject> _supportersUI = new List<GameObject>();

    private CharacterTracker _characterTracker;
    //private SupportTracker _supporterTracker;
    private DialogueEventManager _dialogueEventManager;

    private List<string> _activeHeroineList = new List<string>();
    //private List<string> _activeSupportersList = new List<string>();

    public void Initialize(CharacterTracker characterTracker, SupportTracker supportTracker, DialogueEventManager dialogueEventManager) 
    {
        _characterTracker = characterTracker;
        //_supporterTracker = supportTracker;
        _dialogueEventManager = dialogueEventManager;

        foreach(var heroine in _characterTracker.allHeroines) 
        {
            if(heroine.GetFeatureCharacter().IsActive == true) 
            {
                _activeHeroineList.Add(heroine.TechnicalName);
            }
        }

        //foreach (var supporter in _supporterTracker.allSupporters)
        //{
        //    if (supporter.GetFeatureCharacter().IsActive == true)
        //    {
        //        _activeHeroineList.Add(supporter.TechnicalName);
        //    }
        //}

        for (int i = 0; i < _activeHeroineList.Count; i++)
        {
            _charactersUI[i].GetComponent<CharacterUI>().Initialize(_dialogueEventManager, _characterTracker, _activeHeroineList[i]);
            _charactersUI[i].gameObject.SetActive(true);
        }

        //получить количество активных персов и через евент отправить это значение в FoodUI

    }


    public void UpdateCharacterData(string heroinTechnicalName) 
    {
        foreach(var character in _charactersUI) 
        {
            if (character.GetComponent<CharacterUI>()._characterTechnicalName == heroinTechnicalName) 
            {
                character.GetComponent<CharacterUI>().UpdateUIResources();
            }
        }
    }
}
