using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour {
    [SerializeField] private GameObject numberVariant;

    [SerializeField] private GameObject tabloParent;
    void Start() {
        clearLvl();
    }

    public PlayField generateSettedLvl(List<int> startedValues, List<int> targetValues) {
        if (startedValues.Count != targetValues.Count) {
            Debug.LogError("started and target values have different size");
            return null;
        }
        
        PlayField playField = new PlayField();
        playField.MAXNum = startedValues.Count;
        for (int i = 0; i < playField.MAXNum; i++) {
            GameObject newField = Instantiate(numberVariant, tabloParent.transform);
            NumberPos newNumberPos = newField.GetComponent<NumberPos>();
            newNumberPos.init(i, startedValues[i], targetValues[i], playField.MAXNum);
            playField.addNumberPos(newNumberPos);
        }
        playField.setTargetValues(targetValues);
        return playField;
    }

    public PlayField resetLvl(PlayField playField) {
        playField.resetNumberPos();
        return playField;
    }
    
    public void clearLvl() {
        for (int i = 0; i < tabloParent.transform.childCount; i++) {
            Destroy(tabloParent.transform.GetChild(i).gameObject);
        }
    }

}
