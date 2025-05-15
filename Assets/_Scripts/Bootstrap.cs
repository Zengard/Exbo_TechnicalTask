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

    [Header("Resource settings")]
    [SerializeField] private ResoursesTracker _resourcesTracker;

    [Header("UI settings")]
    [SerializeField] private FoodUI _foodUI;
    [SerializeField] private MoneyUI _moneyUI;
    [SerializeField] private SanitaryUI _sanitaryUI;

    [Header("UI characters")]
    [SerializeField] private CharacterUI_Manager _characterUI_Manager;

    private DialogueEventManager _dialogueEventManager;
    
    void Start()
    {
        _dialogueEventManager = new DialogueEventManager();

        _foodUI.Initialize(_dialogueEventManager, _resourcesTracker);
        _moneyUI.Initialize(_resourcesTracker);
        _sanitaryUI.Initialize(_dialogueEventManager, _resourcesTracker);


        _inventoryTracker.Initialize(_dialogueEventManager);
        _supportTracker.Initialize(_dialogueEventManager);
        _characterTracker.Initialize(_dialogueEventManager, _characterUI_Manager);
        _resourcesTracker.Initialize(_dialogueEventManager, _foodUI, _moneyUI, _sanitaryUI);

        _characterUI_Manager.Initialize(_characterTracker, _supportTracker, _dialogueEventManager);

        _dialogueManager.Initialize(_dialogueEventManager);
        _gameLogic.Initialize(_dialogueEventManager);
    } 
}
