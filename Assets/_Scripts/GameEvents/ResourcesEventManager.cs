using UnityEngine;
using UnityEngine.Events;

public class ResourcesEventManager 
{
   
   public UnityEvent OnUpdateUIResources = new UnityEvent();

    public void UpdateUIResources() 
    {
        OnUpdateUIResources.Invoke();
    }
}
