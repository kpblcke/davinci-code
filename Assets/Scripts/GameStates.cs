using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStates : MonoBehaviour {

    public TextMeshProUGUI _textMeshPro;
    public TextMeshProUGUI targetText;
    
    public GameObject startOverlay;
    public GameObject overlay;

    private ChangeNumberManager _numberManager;

    [SerializeField]
    private Slider _slider;
    
    private FieldGenerator _fieldGenerator;

    // Start is called before the first frame update
    void Start() {
        _numberManager = FindObjectOfType<ChangeNumberManager>();
        _fieldGenerator = FindObjectOfType<FieldGenerator>();
    }

    // Update is called once per frame
    void Update() {

    }
    
    public void clearLvl() {
        _fieldGenerator.clearLvl();
        //PlayField playField = _fieldGenerator.generateLvl((int) _slider.value);
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
        startGame(customLevel);
    }
    
    public void startGame(Level customLevel) {
        startOverlay.SetActive(false);
        PlayField playField = _fieldGenerator.generateSettedLvl(customLevel.StartedValues, customLevel.TargetValues);
        targetText.SetText(playField.getTargetText());
        _numberManager.setNewPlayField(playField);
    }

    public void WinState() {
        overlay.SetActive(true);
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
}
