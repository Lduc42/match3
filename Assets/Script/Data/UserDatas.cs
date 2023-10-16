using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDatas : Singleton<UserDatas>
{
    private const string DataKey = "UserData";

    [Serializable]
    private class UserData
    {
        public Level LevelUserData = new();
    }

    public class Level
    {
        public int currentLevel;
        public Level()
        {
            currentLevel = 0;
        }
    }

    #region Process File

    [SerializeField] private UserData dataLocal;
    private void Awake()
    {
        Initialize();
    }

    public int UserLevel
    {
        get
        {
            return dataLocal.LevelUserData.currentLevel;
        }
        set
        {
            dataLocal.LevelUserData.currentLevel = value;
            SaveLocal();
        }
    }

    public Level GetLevelData()
    {
        return dataLocal.LevelUserData;
    }

    public void SetLevelUser(int levelID)
    {
        dataLocal.LevelUserData.currentLevel = levelID;
        SaveLocal();
    }

    public void Initialize()
    {
        try
        {
            LoadLocal();
        }
        catch (Exception e)
        {
            Debug.LogError("Init userdata error: " + e.ToString());
        }

    }

    private void LoadLocal()
    {
        try
        {
            if (PlayerPrefs.HasKey(DataKey))
            {
                var jsonString = PlayerPrefs.GetString(DataKey);
                dataLocal = JsonUtility.FromJson<UserData>(jsonString);
                Debug.Log("Data Load : " + jsonString);
            }
            else
            {
                dataLocal = new UserData();
                RequestSave();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("=================> Loadlocal error: " + e.ToString());
        }
    }

    private void RequestSave()
    {
        Debug.Log("RequestSave");
        SaveLocal();
    }

    private void SaveLocal()
    {
        try
        {
            var jsonString = JsonUtility.ToJson(dataLocal);
            PlayerPrefs.SetString(DataKey, jsonString);
            Debug.Log("data save : " + jsonString);
            PlayerPrefs.Save();

        }
        catch (Exception e)
        {
            Debug.LogError("======> Save file fail, error: " + e.ToString());
        }
    }

    #endregion
}
