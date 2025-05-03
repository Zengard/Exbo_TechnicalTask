using Articy.Articybrothel;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameLogic _gameLogic;
    [SerializeField] private DialogueManager _dialogueManager;
    
    void Start()
    {
        _dialogueManager.Initialize();
        _gameLogic.Initialize();
    }

    
}
