using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberPos : MonoBehaviour {

    public TextMeshProUGUI previousValueText;
    public TextMeshProUGUI currentValueText;
    public TextMeshProUGUI nextValueText;
    private Animator _animator;
    [SerializeField]
    private Image numBackImage;

    [SerializeField] private Color correctColor;
    [SerializeField] private Color defaultColor;

    [SerializeField] private int value;
    [SerializeField] private int pos;
    [SerializeField] private int target;
    [SerializeField] private int maxValue;

    // Start is called before the first frame update
    void Start() {
        _animator = GetComponent<Animator>();
        updateText();
    }

    public void init(int pos, int value, int target, int maxValue) {
        this.pos = pos;
        this.value = value;
        this.target = target;
        this.maxValue = maxValue;
        updateText();
    }

    public void updateText() {
        previousValueText.SetText(getPrevValue().ToString());
        currentValueText.SetText(value.ToString());
        nextValueText.SetText(getNextValue().ToString());
        numBackImage.color = value == target ? correctColor : defaultColor;
    }

    public int getValue() {
        return value;
    }

    public int getPos() {
        return pos;
    }

    public void plusOne() {
        value = getNextValue();
        _animator.SetTrigger("setNext");
    }

    public void minusOne() {
        value = getPrevValue();
        _animator.SetTrigger("setPrevious");
    }
    
    public void mousePlusEnter() {
        _animator.SetBool("showNext", true);
    }
    
    public void mouseMinusEnter() {
        _animator.SetBool("showPrevious", true);
    }

    public void mousePlusOver() {
        _animator.SetBool("showNext", false);
    }
    
    public void mouseMinusOver() {
        _animator.SetBool("showPrevious", false);
    }

    private int getPrevValue() {
        if (value == 0) {
            return maxValue;
        } else {
            return value - 1;
        }
    }
    
    private int getNextValue() {
        if (value == maxValue) {
            return 0;
        } else {
            return value + 1;
        }
    }
    
}
