using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ItemSpawner itemSpawner;
    public ItemContainer itemContainer;

    public static GameController Instance { get; set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckWin()
    {
        Debug.Log("count "+itemContainer.item_container.Count);
        if (itemContainer.IsContainerEmpty())
        {
            Debug.Log("win");
            UIController.Instance.ActiveWinPopup(true);
        }
    }

    public void CheckLose()
    {
        if (MatchBoxController.Instance.CanNotFill())
        {
            Debug.Log("lose");
            UIController.Instance.ActiveLosePopup(true);
        }
    }

    public void CheckStatusGame()
    {
        CheckWin();
        CheckLose();
    }
}
