using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOLevelData", menuName = "GameConfiguration/SOLevelData", order = 1)]
public class SOLevelData : ScriptableObject
{
    [SerializeField]
    public List<LevelData> levelsData;
}

[System.Serializable]
public class LevelData
{
    public int LevelID;
    public List<SpawnData> itemTypes;
}

[System.Serializable]
public class SpawnData
{
    [SerializeField]
    public GameObject itemType;
    [SerializeField]
    public int numberItem;
}
