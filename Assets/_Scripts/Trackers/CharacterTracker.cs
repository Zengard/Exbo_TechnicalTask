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

    [ArticyTypeConstraint(typeof(IEntity))]
    private List<Heroine> _allHeroines = new List<Heroine>();
    private Dictionary<string, float> _heroinesCache = new Dictionary<string, float>();

    public void Initialize(DialogueEventManager eventManager)
    {
        InitializeItemCache();

        _dialogueEventManager = eventManager;
        _dialogueEventManager.OnUpdateDialogueEntities.AddListener(CheckHeroineChanges);
    }
    private void InitializeItemCache()
    {
        _allHeroines = ArticyDatabase.GetAllOfType<Heroine>();

        foreach (var heroine in _allHeroines)
        {
            _heroinesCache.Add(heroine.TechnicalName, heroine.GetFeatureCharacter().Health);
        }
    }

    private void CheckHeroineChanges(TextMeshProUGUI dialogue)
    {
        foreach (var heroine in _allHeroines)
        {
            var oldValue = _heroinesCache[heroine.TechnicalName];
            var newValue = heroine.GetFeatureCharacter().Health;

            if (newValue != oldValue)
            {
                dialogue.text += "\n" + heroine.GetFeatureCharacter().StrHealth + ": " + oldValue + " -> " + newValue;
                _heroinesCache[heroine.TechnicalName] = newValue;
            }
        }
    }
}
