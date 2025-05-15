 using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Articy.Articybrothel.Templates;
using Articy.Unity;
using Articy.Articybrothel;

public abstract class ResourceUI : MonoBehaviour
{
    [SerializeField] protected string _resourceTechnicalName;
    [SerializeField] protected Slider _slider;
    [SerializeField] protected TextMeshProUGUI _resourceName;

    protected ResourceMeta _resourceData;

    protected float _globalvariableValue;

    
}
