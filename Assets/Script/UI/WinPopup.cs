using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : PopupUIBase
{
    [SerializeField] private Button continueButton;
    
    // Start is called before the first frame update
    void Start()
    {
        InitButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void InitButton()
    {
        continueButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.LoadNextLevel();
            base.HiddenPopup();
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
