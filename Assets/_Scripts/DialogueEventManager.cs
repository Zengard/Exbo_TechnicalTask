using UnityEngine;
using UnityEngine.Events;

public class DialogueEventManager
{
    public UnityEvent OnDialogueEnded = new UnityEvent();
    public UnityEvent OnUpdateResources = new UnityEvent();

    public void DialogueEnded() 
    {
        OnDialogueEnded.Invoke();
    }

    public void UpdateResources() 
    {
        OnUpdateResources.Invoke();
    }
}
