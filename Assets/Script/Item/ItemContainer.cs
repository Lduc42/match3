using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public List<ItemElement> item_container;


    public static ItemContainer Instance { get; set; }
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
        if(this.transform.childCount != item_container.Count)
        {
            UpdateItemList();
        }
    }

    private void UpdateItemList()
    {
        item_container.Clear();
        item_container.AddRange(GetComponentsInChildren<ItemElement>());
    }

    public ItemElement FindObjectByInstanceID(int instanceID)
    {
        foreach (ItemElement obj in item_container)
        {
            if (obj.ID == instanceID)
            {
                return obj; // Trả về GameObject nếu tìm thấy
            }
        }
        return null; // Trả về null nếu không tìm thấyxx
    }

    public bool IsContainerEmpty()
    {
        return item_container.Count <= 0;
    }
}
