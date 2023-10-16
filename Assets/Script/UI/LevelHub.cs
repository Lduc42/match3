using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelHub : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLevelText()
    {
        int currentLevel = UserDatas.Instance.UserLevel + 1;
        levelText.text = "Lv." + currentLevel.ToString();
    }
}
