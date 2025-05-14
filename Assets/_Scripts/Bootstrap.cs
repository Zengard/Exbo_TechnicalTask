using Articy.Articybrothel;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Main dialogue settings")]
    [SerializeField] private GameLogic _gameLogic;
    [SerializeField] private DialogueManager _dialogueManager;

    [Header("Item settings")]
    [SerializeField] private InventoryTracker _inventoryTracker;

    [Header("Character settings")]
    [SerializeField] private SupportTracker _supportTracker;
    [SerializeField] private CharacterTracker _characterTracker;

    private DialogueEventManager _dialogueEventManager;
    
    void Start()
    {
        _dialogueEventManager = new DialogueEventManager();

        _dialogueManager.Initialize(_dialogueEventManager);
        _gameLogic.Initialize(_dialogueEventManager);
        _inventoryTracker.Initialize(_dialogueEventManager);
        _supportTracker.Initialize(_dialogueEventManager);
        _characterTracker.Initialize(_dialogueEventManager);
    } 
}
