using Articy.Articybrothel;
using Articy.Unity;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SupportTracker : MonoBehaviour
{
    DialogueEventManager _dialogueEventManager;

    public List<Supporter> allSupporters = new List<Supporter>();
    private Dictionary<string, bool> _supportersCache = new Dictionary<string, bool>();


    public void Initialize(DialogueEventManager eventManager)
    {
        InitializeSupportersCache();

        _dialogueEventManager = eventManager;
        _dialogueEventManager.OnUpdateDialogueEntities.AddListener(CheckSupportersChanges);
    }

    private void InitializeSupportersCache()
    {
        allSupporters = ArticyDatabase.GetAllOfType<Supporter>();

        foreach (var supporter in allSupporters)
            _supportersCache.Add(supporter.TechnicalName, supporter.GetFeatureCharacter().IsActive);
    }

    private void CheckSupportersChanges(TextMeshProUGUI dialogue)
    {
        foreach (var supporter in allSupporters)
        {
            var oldValue = _supportersCache[supporter.TechnicalName];
            var newValue = supporter.GetFeatureCharacter().IsActive;

            if (oldValue != newValue)
            {
                if (newValue == true)
                {
                    dialogue.text += "\n" + "�������� ��������: " + supporter.DisplayName;
                    _supportersCache[(supporter.TechnicalName)] = newValue;
                }
                else if (newValue == false) 
                {
                    dialogue.text += "\n" + "�������� ������: " + supporter.DisplayName;
                    _supportersCache[(supporter.TechnicalName)] = newValue;
                }
            }
        }
    }
}
