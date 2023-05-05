using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelVariant : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    
    [SerializeField] private Level level;
    [SerializeField] private TextMeshProUGUI startedText;
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private Image backgroundImage;
    
    [SerializeField] private Color selectColor;
    [SerializeField] private Color defaultColor;

    private void Start() {
        StringBuilder startedStringBuilder = new StringBuilder(); 
        startedStringBuilder.AppendJoin(null, level.StartedValues);
        startedText.text = startedStringBuilder.ToString();
        
        StringBuilder targetStringBuilder = new StringBuilder(); 
        targetStringBuilder.AppendJoin(null, level.TargetValues);
        targetText.text = targetStringBuilder.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        backgroundImage.color = selectColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        backgroundImage.color = defaultColor;
    }

    public void OnPointerClick(PointerEventData eventData) {
        GameStates gameStates = FindObjectOfType<GameStates>();
        gameStates.startGame(level);
    }
}