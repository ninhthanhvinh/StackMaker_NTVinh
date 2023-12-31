using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    public int levelNumber;
    public GameObject levelPrefab;
    public Vector3 startPoint;
    public int highestPoint;
}
