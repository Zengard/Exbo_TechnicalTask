using UnityEngine;
using UnityEngine.UI;
using Articy.Unity;
using Articy.Articybrothel;
using Articy.Articybrothel.GlobalVariables;

public class FoodUI : ResourceUI
{
    private DialogueEventManager _dialogueEventManager;
    private ResoursesTracker _resourcesTracker;

    [SerializeField] private float _baseChangePerDay;

    private bool _isDialogueActive;


    public void Initialize(DialogueEventManager eventManager, ResoursesTracker resourcesTracker)
    {
        _slider = GetComponent<Slider>();
        _resourceData = ArticyDatabase.GetObject<ResourceMeta>(_resourceTechnicalName);

        _globalvariableValue = ArticyGlobalVariables.Default.Resources.Food;
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
            _slider.value += _baseChangePerDay * Time.deltaTime * 3;

        if (_slider.value <= 0)
        {
            _slider.value = _slider.maxValue;
            ArticyGlobalVariables.Default.Resources.Food--;
            _resourcesTracker.UpdateFoodData(ArticyGlobalVariables.Default.Resources.Food);
            _globalvariableValue = ArticyGlobalVariables.Default.Resources.Food;
            _resourceName.text = _resourceData.DisplayName + ": " + _globalvariableValue;
        }

        if(ArticyGlobalVariables.Default.Resources.Food < 0) 
        {
            _dialogueEventManager.ResourceEnded();
        }
    }

    public void UpdateUIResources() // receive new data from ResourceTracker
    {
        _globalvariableValue = ArticyGlobalVariables.Default.Resources.Food;
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
