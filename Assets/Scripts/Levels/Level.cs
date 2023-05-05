using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "LevelVariant", order = 0)]
public class Level : ScriptableObject {
    [SerializeField] private List<int> startedValues;
    [SerializeField] private List<int> targetValues;

    public Level(List<int> startedValues, List<int> targetValues) {
        this.startedValues = startedValues;
        this.targetValues = targetValues;
    }
    
    public List<int> StartedValues => startedValues;
    
    public List<int> TargetValues => targetValues;
}
