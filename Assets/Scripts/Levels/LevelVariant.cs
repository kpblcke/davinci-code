using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.Saving;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelVariant : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISaveable {
    
    [SerializeField] private Level level;
    [SerializeField] private TextMeshProUGUI startedText;
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finishedText;
    [SerializeField] private Image backgroundImage;
    
    [SerializeField] private Color selectColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Boolean levelFinished;
    [SerializeField] private int score;
    [SerializeField] private List<LevelModificators> modificators;

    private void Start() {
        textUpdate();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        backgroundImage.color = selectColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        backgroundImage.color = defaultColor;
    }

    public void OnPointerClick(PointerEventData eventData) {
        backgroundImage.color = defaultColor;
        GameStates gameStates = FindObjectOfType<GameStates>();
        gameStates.startGame(level, this);
    }

    public object CaptureState() {
        Dictionary<String, object> state = new Dictionary<String, object>();
        state.Add("score", score);
        state.Add("levelFinished", levelFinished);
        return state;
    }

    public void RestoreState(object state) { 
        Dictionary<String, object> dictionaryState = (Dictionary<String, object>) state;
        score = (int) dictionaryState.GetValueOrDefault("score");
        levelFinished = (Boolean) dictionaryState.GetValueOrDefault("levelFinished");
        textUpdate();
    }

    private void textUpdate() {
        StringBuilder startedStringBuilder = new StringBuilder(); 
        startedStringBuilder.AppendJoin(null, level.StartedValues);
        startedText.text = startedStringBuilder.ToString();
        
        StringBuilder targetStringBuilder = new StringBuilder(); 
        targetStringBuilder.AppendJoin(null, level.TargetValues);
        targetText.text = targetStringBuilder.ToString();

        scoreText.text = score.ToString();
        finishedText.text = levelFinished ? "finished" : "";
    }

    public void Win(int steps) {
        levelFinished = true;
        score = steps;
        textUpdate();
    }

    public Level getLevel() {
        return level;
    }
}