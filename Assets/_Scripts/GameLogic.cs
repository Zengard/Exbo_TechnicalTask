using Articy.Unity;
using Articy.Articybrothel;
using Articy.Unity.Interfaces;
using UnityEngine;
using System.Collections.Generic;
using Articy.Articybrothel.Features;
using Articy.Articybrothel.GlobalVariables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    private DialogueEventManager _dialogueEventManager;
    private bool _isGameOver;

    [Header("Resources")]
    [SerializeField] private float _food;
    [SerializeField] private int _money;
    [SerializeField] private float _sanitary;
    [SerializeField] private bool _isKilledByGuz;

    [Header("Dialogue flow presets")]
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private List<FlowScriptableObject> _flowsOrder = new List<FlowScriptableObject>();
    [SerializeField]private int _currentFlow = 0;

    [Header("Dialogue settings")]
    [SerializeField] private float _dialogueInterval;
    private bool _isallDialoguesHasEnded;
    private float _timer;

    [Header("UI")]
    [SerializeField] private Slider _foodSlider;
    [SerializeField] private Slider _moneySlider;
    [SerializeField] private Slider _sanitarySlider;
    [Space]
    [SerializeField] private GameObject _gameOverScreen;

    private Entity _beata;
    private Entity _frida;
    private Entity _hildet;
    private Entity _kristof;

    public void Initialize(DialogueEventManager eventManager) 
    {
        _beata = ArticyDatabase.GetObject<Entity>("Beata");
        _frida = ArticyDatabase.GetObject<Entity>("Frida");
        _hildet = ArticyDatabase.GetObject<Entity>("Hildet");
        _kristof = ArticyDatabase.GetObject<Entity>("Kristof");

        _food = ArticyGlobalVariables.Default.Resources.Food;
        _money = ArticyGlobalVariables.Default.Resources.Money;
        _sanitary = ArticyGlobalVariables.Default.Resources.Sanitary;
        _isKilledByGuz = ArticyGlobalVariables.Default.LoseConditions.KilledByGuz;

        _dialogueManager.SetUpDialogue(_flowsOrder[_currentFlow].TechnicalName);

        _dialogueEventManager = eventManager;
        _dialogueEventManager.OnDialogueEnded.AddListener(SetNextFlow);
        _dialogueEventManager.OnUpdateResources.AddListener(UpdateResources);

    }
    void Update()
    {
        if (_dialogueManager.IsDialogueActive == true || _isGameOver == true) return;

        //_food -= Time.deltaTime;
        if (_food <= 0) 
        {
            _gameOverScreen.SetActive(true);
            _isGameOver = true;
        }


        _timer += Time.deltaTime;
        if (_timer >= _dialogueInterval && _isallDialoguesHasEnded == false) 
        {
            _dialogueManager.SetUpDialogue(_flowsOrder[_currentFlow].TechnicalName);
            _timer = 0;
        }
    }

    private void SetNextFlow() 
    {
        _currentFlow++;

        if(_currentFlow > _flowsOrder.Count - 1)
        {
            _currentFlow = _flowsOrder.Count -1;
            _isallDialoguesHasEnded = true;
        }
    }

    private void UpdateResources() 
    {
        if(_isKilledByGuz != ArticyGlobalVariables.Default.LoseConditions.KilledByGuz) 
        {
            _isGameOver = true;
            _gameOverScreen.SetActive(true);
        }
    }

    public void RestartScene() 
    {
        ArticyGlobalVariables.Default.ResetVariables();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
