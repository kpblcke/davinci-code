using System;
using TMPro;
using UnityEngine;

public class MovesText : MonoBehaviour {
    
    [SerializeField]
    private ChangeNumberManager changeNumberManager;
    private TextMeshProUGUI _text;
    private void Awake() {
        
    }

    private void Start() {
        changeNumberManager = FindObjectOfType<ChangeNumberManager>();
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        _text.SetText(changeNumberManager.getSteps().ToString());
    }
}