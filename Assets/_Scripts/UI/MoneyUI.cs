using UnityEngine;
using UnityEngine.UI;
using Articy.Unity;
using Articy.Articybrothel;
using Articy.Articybrothel.GlobalVariables;

public class MoneyUI : ResourceUI
{
    //private ResoursesTracker _resourcesTracker;

    public void Initialize(ResoursesTracker resourcesTracker)
    {
        _slider = GetComponent<Slider>();
        _resourceData = ArticyDatabase.GetObject<ResourceMeta>(_resourceTechnicalName);
        Debug.Log(_resourceData.DisplayName);

        _globalvariableValue = ArticyGlobalVariables.Default.Resources.Money;

        _resourceName.text = _resourceData.DisplayName + ": " + _globalvariableValue;

        //_resourcesTracker = resourcesTracker;
    }

    public void UpdateUIResources() // receive new data from ResourceTracker
    {
        _globalvariableValue = ArticyGlobalVariables.Default.Resources.Money;
        _resourceName.text = _resourceData.DisplayName + ": " + _globalvariableValue;
    }
}
