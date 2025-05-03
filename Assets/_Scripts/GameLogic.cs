using Articy.Unity;
using Articy.Articybrothel;
using Articy.Unity.Interfaces;
using UnityEngine;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{
    [Header("Dialogue presets")]
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private List<FlowScriptableObject> _flowsOrder = new List<FlowScriptableObject>();
    //[SerializeField]private List<ArticyObject> _dialogueEvents;
    private int _currentFlow = 0;
 

    [SerializeField]Entity Beata;
    [SerializeField] Entity Frida;
    [SerializeField] Entity Hildet;
    [SerializeField] Entity Kristof;
    void Start()
    {
        Beata = ArticyDatabase.GetObject<Entity>("Beata");
        Frida = ArticyDatabase.GetObject<Entity>("Frida");
        Hildet = ArticyDatabase.GetObject<Entity>("Hildet");
        Kristof = ArticyDatabase.GetObject<Entity>("Kristof");

        _dialogueManager.SetUpDialogue(_flowsOrder[_currentFlow].TechnicalName);
    }

    public void Initialize() 
    {
        Beata = ArticyDatabase.GetObject<Entity>("Beata");
        Frida = ArticyDatabase.GetObject<Entity>("Frida");
        Hildet = ArticyDatabase.GetObject<Entity>("Hildet");
        Kristof = ArticyDatabase.GetObject<Entity>("Kristof");

        _dialogueManager.SetUpDialogue(_flowsOrder[_currentFlow].TechnicalName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
