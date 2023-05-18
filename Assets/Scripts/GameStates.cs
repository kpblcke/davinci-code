using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStates : MonoBehaviour {

    public TextMeshProUGUI _textMeshPro;
    public TextMeshProUGUI targetText;
    
    public GameObject startOverlay;
    public GameObject overlay;
    public GameObject levelsContainer;

    private ChangeNumberManager _numberManager;

    private FieldGenerator _fieldGenerator;

    [SerializeField]
    private LevelVariant currentLevel;

    [SerializeField]
    private List<LevelVariant> levels;

    // Start is called before the first frame update
    void Start() {
        _numberManager = FindObjectOfType<ChangeNumberManager>();
        _fieldGenerator = FindObjectOfType<FieldGenerator>();

        levels = levelsContainer.GetComponentsInChildren<LevelVariant>().ToList();
    }

    // Update is called once per frame
    void Update() {

    }
    
    public void clearLvl() {
        _fieldGenerator.clearLvl();
        targetText.SetText("");
        _numberManager.setNewPlayField(null);
    }

    public void startGame(int digits) {
        startOverlay.SetActive(false);
        List<int> startedValues = new List<int>();
        List<int> targetValues = new List<int>();
        for (int i = 0; i < digits; i++) {
            startedValues.Add(Random.Range(0, digits));
            targetValues.Add(Random.Range(0, digits));
        }
        Level customLevel = new Level(startedValues, targetValues);
        startGame(customLevel, null);
    }
    
    public void startGame(Level customLevel, LevelVariant levelVariant) {
        startOverlay.SetActive(false);
        if (levelVariant != null) currentLevel = levelVariant;
        PlayField playField = _fieldGenerator.generateSettedLvl(customLevel.StartedValues, customLevel.TargetValues);
        targetText.SetText(playField.getTargetText());
        _numberManager.setNewPlayField(playField);
    }

    public void WinState(int steps) {
        overlay.SetActive(true);
        currentLevel.Win(steps);
        _textMeshPro.SetText("YOU WIN!");
    }

    public void LoseState() {
        overlay.SetActive(true);
        _textMeshPro.SetText("YOU LOSE!");
    }

    public void Replay() {
        _textMeshPro.SetText("");
        overlay.SetActive(false);
        _numberManager.setNewPlayField(_fieldGenerator.resetLvl(_numberManager.getPlayField()));
        _numberManager.Replay();
    }

    public void PlayNextLevel() {
        int currLevel = levels.IndexOf(currentLevel);
        if (levels.Count < currLevel) {
            return;
        }
        clearLvl();
        currLevel++;
        startGame(levels[currLevel].getLevel(), levels[currLevel]);
    }
}
