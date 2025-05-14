using Articy.Articybrothel;
using Articy.Articybrothel.GlobalVariables;
using Articy.Unity;
using Articy.Unity.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using TMPro;
using UnityEngine;

public class InventoryTracker : MonoBehaviour
{
    DialogueEventManager _dialogueEventManager;

    [ArticyTypeConstraint(typeof(IEntity))]
    private List<Item> _allItems = new List<Item>();
    private Dictionary<string, float> _itemCache = new Dictionary<string, float>();

    public void Initialize(DialogueEventManager eventManager) 
    {
        InitializeItemCache();

        _dialogueEventManager = eventManager;
        _dialogueEventManager.OnUpdateDialogueEntities.AddListener(CheckItemsChanges);
    }
    private void InitializeItemCache()
    { 
        _allItems = ArticyDatabase
        .GetAllOfType<Item>()
        .Where(obj => obj.TechnicalName.Contains("Item_"))
        .ToList();

        foreach (var item in _allItems)
        {
            _itemCache.Add(item.TechnicalName, item.GetFeatureItem().Count);
        }
    }

    private void CheckItemsChanges(TextMeshProUGUI dialogue) 
    {
        foreach (var item in _allItems)
        {
            var oldValue = _itemCache[item.TechnicalName];
            var newValue = item.GetFeatureItem().Count;

            if(newValue > oldValue) 
            {
               dialogue.text += "\n" + item.GetFeatureItem().StrObtained;
                Debug.Log($"{item.GetFeatureItem().StrObtained}");
                _itemCache[item.TechnicalName] = newValue;
            }
            else if( oldValue > newValue) 
            {
                dialogue.text += "\n" + item.GetFeatureItem().StrLost;
                Debug.Log($"{item.GetFeatureItem().StrLost}");
                _itemCache[item.TechnicalName] = newValue;
            }
        }
    }
}
