using Articy.Articybrothel;
using Articy.Articybrothel.GlobalVariables;
using Articy.Unity;
using Articy.Unity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class CharacterTracker : MonoBehaviour
{
    DialogueEventManager _dialogueEventManager;
    CharacterUI_Manager _characterUI_Manager;

    [ArticyTypeConstraint(typeof(IEntity))]
    public List<Heroine> allHeroines = new List<Heroine>();
    private Dictionary<string, float> _heroinesHealthCache = new Dictionary<string, float>();

    public void Initialize(DialogueEventManager eventManager, CharacterUI_Manager characterUI_Manager)
    {
        InitializeItemCache();

        _dialogueEventManager = eventManager;
        _dialogueEventManager.OnUpdateDialogueEntities.AddListener(CheckHeroineChanges);

        _characterUI_Manager = characterUI_Manager;
    }
    private void InitializeItemCache()
    {
        allHeroines = ArticyDatabase.GetAllOfType<Heroine>();

        foreach (var heroine in allHeroines)
        {
            _heroinesHealthCache.Add(heroine.TechnicalName, heroine.GetFeatureCharacter().Health);
        }
    }

    private void CheckHeroineChanges(TextMeshProUGUI dialogue)
    {
        foreach (var heroine in allHeroines)
        {
            var oldValue = _heroinesHealthCache[heroine.TechnicalName];
            var newValue = heroine.GetFeatureCharacter().Health;

            if (newValue != oldValue)
            {
                dialogue.text += "\n" + heroine.GetFeatureCharacter().StrHealth + ": " + oldValue + " -> " + newValue;
                _heroinesHealthCache[heroine.TechnicalName] = newValue;
                _characterUI_Manager.UpdateCharacterData(heroine.TechnicalName);
            }
        }
    }

}
