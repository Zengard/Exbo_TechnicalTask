using Articy.Articybrothel;
using Articy.Unity;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SupportTracker : MonoBehaviour
{
    DialogueEventManager _dialogueEventManager;

    private List<Supporter> _allSupporters = new List<Supporter>();
    private Dictionary<string, bool> _supportersCache = new Dictionary<string, bool>();


    public void Initialize(DialogueEventManager eventManager)
    {
        InitializeSupportersCache();

        _dialogueEventManager = eventManager;
        _dialogueEventManager.OnUpdateDialogueEntities.AddListener(CheckSupportersChanges);
    }

    private void InitializeSupportersCache()
    {
        _allSupporters = ArticyDatabase.GetAllOfType<Supporter>();

        foreach (var supporter in _allSupporters)
            _supportersCache.Add(supporter.TechnicalName, supporter.GetFeatureCharacter().IsActive);
    }

    private void CheckSupportersChanges(TextMeshProUGUI dialogue)
    {
        foreach (var supporter in _allSupporters)
        {
            var oldValue = _supportersCache[supporter.TechnicalName];
            var newValue = supporter.GetFeatureCharacter().IsActive;

            if (oldValue != newValue)
            {
                if (newValue == true)
                {
                    dialogue.text += "\n" + "Добавлен персонаж: " + supporter.DisplayName;
                    _supportersCache[(supporter.TechnicalName)] = newValue;
                }
                else if (newValue == false) 
                {
                    dialogue.text += "\n" + "Персонаж удален: " + supporter.DisplayName;
                    _supportersCache[(supporter.TechnicalName)] = newValue;
                }
            }
        }
    }
}
