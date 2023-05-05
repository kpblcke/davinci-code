using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class PlayField {
    [SerializeField] private List<int> startedValues = new List<int>();
    [SerializeField] private List<NumberPos> numbersPos = new List<NumberPos>();
    [SerializeField] private Stack<List<int>> previousValues = new Stack<List<int>>();
    [SerializeField] private List<int> targetValues = new List<int>();
    [SerializeField] private int maxNum = 0;

    public int MAXNum {
        get => maxNum;
        set => maxNum = value;
    }

    public void setTargetValues(List<int> target) {
        targetValues = target;
    }

    public void addNumberPos(NumberPos newNumberPos) {
        numbersPos.Add(newNumberPos);
        startedValues.Add(newNumberPos.getValue());
    }

    public void revertStep() {
        var hasPrevStep = previousValues.TryPop(out var prevStep);
        if (!hasPrevStep) return;
        foreach (var number in numbersPos) {
            number.init(number.getPos(), prevStep[number.getPos()], targetValues[number.getPos()], maxNum);
        }
    }

    public void resetNumberPos() {
        previousValues.Clear();
        foreach (var number in numbersPos) {
            number.init(number.getPos(), startedValues[number.getPos()], targetValues[number.getPos()], maxNum);
        }
        previousValues.Push(getValues());
    }

    public int getTargetValueAtPos(int pos) {
        return targetValues[pos];
    }

    public NumberPos getNumberAtPos(int pos) {
        if (pos < 0) return null;
        if (numbersPos.Count < pos) return null;
        if (numbersPos[pos] == null) return null;
        return numbersPos[pos];
    }

    public String getTargetText() {
        StringBuilder stringBuilder = new StringBuilder(); 
        stringBuilder.AppendJoin(null, targetValues);
        return stringBuilder.ToString();
    }

    public List<int> getValues() {
        List<int> values = new List<int>();
        foreach (NumberPos number in numbersPos) {
            values.Add(number.getValue());
        }

        return values;
    }

    public void addStep(List<int> prevStep) {
        previousValues.Push(prevStep);
    }
}
