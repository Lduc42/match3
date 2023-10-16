using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    [SerializeField] private WinPopup winPopup;
    [SerializeField] private LosePopup losePopup;
    [SerializeField] private LevelHub levelHub;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveWinPopup(bool isActive)
    {
        if(isActive)
        winPopup.gameObject.SetActive(isActive);
        winPopup.ActivePopUp(isActive);
    }

    public void ActiveLosePopup(bool isActive)
    {
        if (isActive)
            losePopup.gameObject.SetActive(true);
        losePopup.ActivePopUp(isActive);
    }

    public void UpdateLevelHub()
    {
        levelHub.UpdateLevelText();
    }
}
