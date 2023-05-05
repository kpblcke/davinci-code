using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeNumberManager : MonoBehaviour {
    [SerializeField] private PlayField playField;

    private GameStates gState;
    private long steps;
    public TextMeshProUGUI _stepsText;

    // Start is called before the first frame update
    void Start() {
        steps = 0;
        _stepsText.SetText(steps.ToString());
        gState = FindObjectOfType<GameStates>();
    }

    public void setNewPlayField(PlayField playField) {
        this.playField = playField;
        if (playField != null) { checkState(); }
    }

    public PlayField getPlayField() {
        return playField;
    }

    public void Replay() {
        steps = 0;
        _stepsText.SetText(steps.ToString());
        checkState();
    }

    public void stepRevert() {
        if (steps <= 0) return;
        playField.revertStep();
        steps -= 2;
        stepsUpdate();
    }

    private void stepsUpdate() {
        steps++;
        _stepsText.SetText(steps.ToString());
    }

    public void mouseOn(int pos, bool isPlus) {
        NumberPos number = playField.getNumberAtPos(pos);
        if (number == null || number.getPos() == number.getValue() - 1) return;
        if (number.getValue() == 0) {
            if (isPlus) {
                number.mousePlusEnter();
            } else {
                number.mouseMinusEnter();
            }
            return;
        }

        NumberPos crossNumber = playField.getNumberAtPos(number.getValue() - 1);
        if (crossNumber == null) return;
        if (isPlus) {
            number.mousePlusEnter();
            crossNumber.mouseMinusEnter();
        } else {
            number.mouseMinusEnter();
            crossNumber.mousePlusEnter();
        }
    }

    public void mouseOver(int pos, bool isPlus) {
        NumberPos number = playField.getNumberAtPos(pos);
        if (number == null || number.getPos() == number.getValue() - 1) return;
        if (number.getValue() == 0) {
            if (isPlus) {
                number.mousePlusOver();
            } else {
                number.mouseMinusOver();
            }
            return;
        }

        NumberPos crossNumber = playField.getNumberAtPos(number.getValue() - 1);
        if (crossNumber == null) return;
        if (isPlus) {
            number.mousePlusOver();
            crossNumber.mouseMinusOver();
        } else {
            number.mouseMinusOver();
            crossNumber.mousePlusOver();
        }
    }
    

    public void plusOne(int pos) {
        NumberPos number = playField.getNumberAtPos(pos);
        if (number == null) return;
        if (number.getValue() - 1 == pos) return;
        if (number.getValue() == 0) {
            playField.addStep(playField.getValues());
            number.plusOne();
            stepsUpdate();
            checkState();
            return;
        }

        NumberPos crossNumber = playField.getNumberAtPos(number.getValue() - 1);
        if (crossNumber == null) return;
        playField.addStep(playField.getValues());
        crossNumber.mouseMinusOver();
        number.plusOne();
        crossNumber.minusOne();
        stepsUpdate();
        checkState();
    }

    public void minusOne(int pos) {
        NumberPos number = playField.getNumberAtPos(pos);
        if (number == null) return;
        if (number.getValue() - 1 == pos) return;
        if (number.getValue() == 0) {
            playField.addStep(playField.getValues());
            number.minusOne();
            stepsUpdate();
            checkState();
            return;
        }

        NumberPos crossNumber = playField.getNumberAtPos(number.getValue() - 1);
        if (crossNumber == null) return;
        playField.addStep(playField.getValues());
        crossNumber.mousePlusOver();
        number.minusOne();
        crossNumber.plusOne();
        stepsUpdate();
        checkState();
    }

    private void checkState() {
        bool lose = true;
        bool win = true;
        for (int p = 0; p < playField.MAXNum; p++) {
            NumberPos number = playField.getNumberAtPos(p);
            if (number == null) break;
            if (number.getValue() - 1 != number.getPos()) {
                lose = false;
            }

            if (number.getValue() != playField.getTargetValueAtPos(number.getPos())) { win = false; }
        }

        if (lose) { gState.LoseState(); }
        if (win) { gState.WinState(); }
    }
}
