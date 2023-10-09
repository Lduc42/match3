using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemListConfig", menuName = "GameConfiguration/ItemListConfig", order = 1)]
public class ItemListConfig : ScriptableObject
{
    [SerializeField]
    public List<ItemSpawnConfig> itemTypes;
}

[System.Serializable]
public class ItemSpawnConfig
{
    [SerializeField]
    public GameObject itemType;
    [SerializeField]
    public int numberItem;
}
