using Articy.Articybrothel;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameLogic _gameLogic;
    [SerializeField] private DialogueManager _dialogueManager;
    private DialogueEventManager _dialogueEventManager;
    
    void Start()
    {
        _dialogueEventManager = new DialogueEventManager();

        _dialogueManager.Initialize(_dialogueEventManager);
        _gameLogic.Initialize(_dialogueEventManager);
    } 
}
