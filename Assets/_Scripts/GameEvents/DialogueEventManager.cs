using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueEventManager
{
    public UnityEvent OnDialogueEnded = new UnityEvent();
    public UnityEvent OnDialogueStarted = new UnityEvent();
    public UnityEvent OnUpdateResources = new UnityEvent();
    public UnityEvent OnResourceEnded = new UnityEvent();

    public UnityEvent<TextMeshProUGUI> OnUpdateDialogueEntities = new UnityEvent<TextMeshProUGUI>();

    public void DialogueEnded() 
    {
        OnDialogueEnded.Invoke();
    }

    public void DialogueStarted() 
    {
        OnDialogueStarted.Invoke();
    }

    public void UpdateResources() 
    {
        OnUpdateResources.Invoke();
    }


    public void UpdateEntities(TextMeshProUGUI dialogue) 
    {
        OnUpdateDialogueEntities.Invoke(dialogue);
    }

    public void ResourceEnded() 
    {
        OnResourceEnded.Invoke();
    }
}
