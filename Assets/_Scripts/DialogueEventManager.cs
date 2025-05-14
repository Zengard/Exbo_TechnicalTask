using UnityEngine;
using UnityEngine.Events;

public class DialogueEventManager
{
    public UnityEvent OnDialogueEnded = new UnityEvent();
    public UnityEvent OnUpdateResources = new UnityEvent();
    public UnityEvent OnUpdateItems = new UnityEvent();

    public void DialogueEnded() 
    {
        OnDialogueEnded.Invoke();
    }

    public void UpdateResources() 
    {
        OnUpdateResources.Invoke();
    }

    public void UpdateItems() 
    {
        OnUpdateItems.Invoke();
    }
}
