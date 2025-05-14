using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueEventManager
{
    public UnityEvent OnDialogueEnded = new UnityEvent();
    public UnityEvent OnUpdateResources = new UnityEvent();

    //public UnityEvent<TextMeshProUGUI> OnUpdateItems = new UnityEvent<TextMeshProUGUI>();
    //public UnityEvent<TextMeshProUGUI> OnUpdateSupport = new UnityEvent<TextMeshProUGUI>();
    //public UnityEvent<TextMeshProUGUI> OnUpdateHeroine = new UnityEvent<TextMeshProUGUI>();

    public UnityEvent<TextMeshProUGUI> OnUpdateDialogueEntities = new UnityEvent<TextMeshProUGUI>();

    public void DialogueEnded() 
    {
        OnDialogueEnded.Invoke();
    }

    public void UpdateResources() 
    {
        OnUpdateResources.Invoke();
    }

    //public void UpdateItems(TextMeshProUGUI dialogue) 
    //{
    //    OnUpdateItems.Invoke(dialogue);
    //}

    //public void UpdateSupport(TextMeshProUGUI dialogue)
    //{
    //    OnUpdateSupport.Invoke(dialogue);
    //}

    //public void UpdateHeroine(TextMeshProUGUI dialogue) 
    //{
    //    OnUpdateHeroine.Invoke(dialogue);
    //}

    public void UpdateEntities(TextMeshProUGUI dialogue) 
    {
        OnUpdateDialogueEntities.Invoke(dialogue);
    }
}
