using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MatchBoxController : MonoBehaviour
{
    public Transform[] slotsPosition;

    public List<GameObject> itemsMatchBox;

    public static MatchBoxController Instance { get; set; }
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

    public void AddItemToBox(GameObject item)
    {
        item.GetComponent<MeshCollider>().enabled = false;
        for(int i = 0; i < itemsMatchBox.Count; i++)
        {
            if(itemsMatchBox[i].name == item.name)
            {
                itemsMatchBox.Insert(i, item);
                UpdateItemInBox();
                StartCoroutine(MoveObjectToBox(item, i ));
                return;
            }
        }
        item.GetComponent<Outline>().enabled = false;
        itemsMatchBox.Add(item);
        //MoveObjectToBox(item, itemsMatchBox.Count - 1);
        
        StartCoroutine(MoveObjectToBox(item, itemsMatchBox.Count - 1));

        //ItemSelector.Instance.preItemIndex = 999;
    }

    public IEnumerator MoveObjectToBox(GameObject item, int indexBox)
    {

        ItemElement itemElement = item.GetComponent<ItemElement>();
        item.transform.DOMove(slotsPosition[indexBox].position, 1f);
        yield return new WaitForSeconds(.3f);

        ScaleDown(itemElement);
        itemElement.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        itemElement.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        //item.transform.DORotate(itemElement.originalRotation, .1f);
    }

    private void ScaleDown(ItemElement item)
    {
        item.gameObject.transform.DOScale(item.originalScale, .2f);
    }

    private void UpdateItemInBox()
    {
        Debug.Log("update position box");
        for(int i = itemsMatchBox.Count - 1; i >= 0; i--)
        {
            ItemElement item = itemsMatchBox[i].GetComponent<ItemElement>();
            item.transform.DOMove(slotsPosition[i].position, .5f);
        }
    }

    private void CheckMatchTriple()
    {
        int matchCount;
        for(int i = 0; i < itemsMatchBox.Count; i++)
        {
            
        }
    }
}
