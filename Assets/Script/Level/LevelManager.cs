using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private SOLevelData SOLevelDataStore;
    [SerializeField]
    public ItemSpawner ItemSpawner;

    public int currentLevelIndex;
    public LevelData currentLevelData;
    public List<SpawnData> LstItemInLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(UserDatas.Instance.UserLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        Debug.Log("level quit save " + UserDatas.Instance.UserLevel);
    }

    public void InitLevelData(int level)
    {
        currentLevelData = SOLevelDataStore.levelsData[level];
        LstItemInLevel = currentLevelData.itemTypes;
        currentLevelIndex = currentLevelData.LevelID;
    }

    public void LoadLevel(int level)
    {
        ClearCurrentLevel();
        InitLevelData(level);
        this.ItemSpawner.SpawnListItem(LstItemInLevel);
        MatchBoxController.Instance.CheckWarning();
    }

    public void LoadNextLevel(){
        UserDatas.Instance.UserLevel += 1;
        
        LoadLevel(UserDatas.Instance.UserLevel);
        UIController.Instance.UpdateLevelHub();
    }

    public void ClearCurrentLevel()
    {
        foreach(GameObject obj in MatchBoxController.Instance.itemsMatchBox)
        {
            Destroy(obj);
        }
        foreach (ItemElement obj in ItemContainer.Instance.item_container)
        {
            Destroy(obj.gameObject);
        }
        MatchBoxController.Instance.itemsMatchBox.Clear();
        ItemContainer.Instance.item_container.Clear();
    }
}
