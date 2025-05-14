using Articy.Articybrothel;
using Articy.Articybrothel.GlobalVariables;
using Articy.Unity;
using Articy.Unity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        _dialogueEventManager.OnUpdateItems.AddListener(CheckItemsChanges);
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

    private void CheckItemsChanges() 
    {
        foreach (var item in _allItems)
        {
            var oldValue = _itemCache[item.TechnicalName];
            var newValue = item.GetFeatureItem().Count;

            if(newValue > oldValue) 
            {
                Debug.Log($"{item.GetFeatureItem().StrObtained}");
            }
            else if( oldValue > newValue) 
            {
                Debug.Log($"{item.GetFeatureItem().StrLost}");
            }
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.F)) 
    //    {
    //        foreach (var item in _allItems)
    //        {
    //            Debug.Log($"ID: {item.Id}");
    //            Debug.Log($"Ќазвание: {item.TechnicalName}");
    //            Debug.Log($"количество: {item.GetFeatureItem().Count}");
    //            Debug.Log($"текст получени€: {item.GetFeatureItem().StrObtained}");
    //            Debug.Log($"текст потери:{item.GetFeatureItem().StrLost}");
    //            Debug.Log("------------------");
    //        }
    //    }
    //}
}
