using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePopup : PopupUIBase
{
    [SerializeField] private Button replayBtn;

    // Start is called before the first frame update
    void Start()
    {
        InitBtn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void InitBtn()
    {
        replayBtn.onClick.AddListener(()=> {
            LevelManager.Instance.LoadLevel(UserDatas.Instance.UserLevel);
            HiddenPopup();
        });
    }

    public void ActivePopUp(bool isActive)
    {
        if (isActive)
        {
            ShowPopup();
        }
        else
        {
            HiddenPopup();
        }
    }
}
