using UnityEngine;
using UnityEngine.UI;
using Articy.Unity;
using Articy.Articybrothel;
using Articy.Articybrothel.GlobalVariables;

public class CharacterUI : ResourceUI
{
    private DialogueEventManager _dialogueEventManager;
    private CharacterTracker _characterTracker;

    [SerializeField] private float _baseChangePerDay;

    private bool _isDialogueActive;

    private Heroine _heroineData;
    public string _characterTechnicalName;

    public void Initialize(DialogueEventManager eventManager, CharacterTracker characterTracker, string characterTechnicalName)
    {
        _characterTechnicalName = characterTechnicalName;
;
        _heroineData = ArticyDatabase.GetObject<Heroine>(_characterTechnicalName);

        _globalvariableValue = _heroineData.GetFeatureCharacter().Health;
        _baseChangePerDay = _heroineData.GetFeatureCharacter().HealthRegen;

        _resourceName.text = _heroineData.DisplayName + ": " + _globalvariableValue;

        _dialogueEventManager = eventManager;
        _dialogueEventManager.OnDialogueStarted.AddListener(DialogueStarted);
        _dialogueEventManager.OnDialogueEnded.AddListener(DialogueEnded);

        _characterTracker = characterTracker;

    }

    private void Update()
    {
        if (_isDialogueActive == true) return;

        if (_slider.value < 1)
            _slider.value += _baseChangePerDay * Time.deltaTime;

        if(_slider.value >= 1) 
        {
            if (_heroineData.GetFeatureCharacter().Health == _heroineData.GetFeatureCharacter().MaxHealth)
                return;

            _slider.value = _slider.minValue;
            _heroineData.GetFeatureCharacter().Health++;
            _globalvariableValue = _heroineData.GetFeatureCharacter().Health;
            _resourceName.text = _heroineData.DisplayName + ": " + _globalvariableValue;
        }
    }

    public void UpdateUIResources() // receive new data from CharacterTracker
    {
         _globalvariableValue = _heroineData.GetFeatureCharacter().Health;
        _resourceName.text = _heroineData.DisplayName + ": " + _globalvariableValue;
    }

    private void DialogueStarted()
    {
        _isDialogueActive = true;
    }

    private void DialogueEnded()
    {
        _isDialogueActive = false;
    }
}
