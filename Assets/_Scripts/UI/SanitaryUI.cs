using UnityEngine;
using UnityEngine.UI;
using Articy.Unity;
using Articy.Articybrothel;
using Articy.Articybrothel.GlobalVariables;

public class SanitaryUI : ResourceUI
{
    private DialogueEventManager _dialogueEventManager;
    private ResoursesTracker _resourcesTracker;

    [SerializeField] private float _baseChangePerDay;

    private bool _isDialogueActive;


    public void Initialize(DialogueEventManager eventManager, ResoursesTracker resourcesTracker)
    {
        _slider = GetComponent<Slider>();
        _resourceData = ArticyDatabase.GetObject<ResourceMeta>(_resourceTechnicalName);

        _globalvariableValue = ArticyGlobalVariables.Default.Resources.Sanitary;
        _baseChangePerDay = _resourceData.GetFeatureResourceMeta().BaseChangePerDay;

        _resourceName.text = _resourceData.DisplayName + ": " + _globalvariableValue;

        _dialogueEventManager = eventManager;
        _dialogueEventManager.OnDialogueStarted.AddListener(DialogueStarted);
        _dialogueEventManager.OnDialogueEnded.AddListener(DialogueEnded);

        _resourcesTracker = resourcesTracker;

    }

    private void Update()
    {
        if (_isDialogueActive == true) return;

        if (_slider.value > 0)
            _slider.value += _baseChangePerDay * Time.deltaTime;

        if (_slider.value <= 0)
        {
            _slider.value = _slider.maxValue;
            ArticyGlobalVariables.Default.Resources.Sanitary--;
            _resourcesTracker.UpdateSanitaryData(ArticyGlobalVariables.Default.Resources.Sanitary);
            _globalvariableValue = ArticyGlobalVariables.Default.Resources.Sanitary;
            _resourceName.text = _resourceData.DisplayName + ": " + _globalvariableValue;
        }
    }

    public void UpdateUIResources() // receive new data from ResourceTracker
    {
        _globalvariableValue = ArticyGlobalVariables.Default.Resources.Sanitary;
        _resourceName.text = _resourceData.DisplayName + ": " + _globalvariableValue;
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
