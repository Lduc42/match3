using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;

public class MatchBoxController : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer warning;
    [SerializeField] private int NumberMatchBarSlot;
    public float fadeDuration = .5f;
    public float minAlpha = 0.2f;
    public float maxAlpha = 1.0f;
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
        //if (itemsMatchBox.Count >= 7) return;
        item.GetComponent<MeshCollider>().enabled = false;
        item.GetComponent<Rigidbody>().useGravity = false;
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
        if(item.GetComponent<Outline>())
        item.GetComponent<Outline>().enabled = false;
        itemsMatchBox.Add(item);
        //MoveObjectToBox(item, itemsMatchBox.Count - 1);
        
        StartCoroutine(MoveObjectToBox(item, itemsMatchBox.Count - 1));

        //ItemSelector.Instance.preItemIndex = 999;
    }

    public IEnumerator MoveObjectToBox(GameObject item, int indexBox)
    {

        ItemElement itemElement = item.GetComponent<ItemElement>();
        item.transform.DOMove(slotsPosition[indexBox].position, .6f).OnComplete(()=> {
            ScaleDown(itemElement);
        });
        yield return new WaitForSeconds(.3f);
        
        
        item.GetComponent<Outline>().OutlineWidth = 0;
        itemElement.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        itemElement.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

        itemElement.SetDefaultRotation();
        //item.transform.DORotate(itemElement.originalRotation, .1f);
    }

    private void ScaleDown(ItemElement item)
    {
        item.gameObject.transform.DOScale(item.originalScale, .2f).OnComplete(()=> {
            CheckMatchItem(item.ID);
        });
    }

    private void UpdateItemInBox()
    {
        Debug.Log("update position box");
        for(int i = itemsMatchBox.Count - 1; i >= 0; i--)
        {
            ItemElement item = itemsMatchBox[i].GetComponent<ItemElement>();
            item.transform.DOMove(slotsPosition[i].position, .5f);
        }
        CheckWarning();
    }


    public void CheckMatchItem(int id)
    {
        int match = 0;
        int firstSpawnedID = 0;
        int centerSpawnedID = 0;
        int lastSpawnedID = 0;
        Vector3 targetMove = Vector3.zero;
        foreach(GameObject obj in itemsMatchBox)
        {
            ItemElement item = obj.GetComponent<ItemElement>();
            if(item.ID == id)
            {
                match++;
            }

            if (match == 1) firstSpawnedID = item.SpawnedID;
            if (match == 2) {
                centerSpawnedID = item.SpawnedID;
                targetMove = item.transform.position;
            }
            if (match == 3) lastSpawnedID = item.SpawnedID;
            
        }
        if(match >= 3)
        {
            int preLength = itemsMatchBox.Count;
            Debug.Log("length " + preLength);
            for(int i = itemsMatchBox.Count - 1; i >= 0; i--)
            {
                ItemElement itemObj = itemsMatchBox[i].GetComponent<ItemElement>();
                if(itemObj.ID == id)
                {
                    itemObj.gameObject.transform.DOMove(targetMove, .2f).OnComplete(() =>
                    {
                        if (itemObj.SpawnedID == centerSpawnedID)
                            itemObj.ShowMatchVfx(new Vector3(targetMove.x, targetMove.y,targetMove.z));
                        itemObj.gameObject.transform.DOScale(.2f, .5f).OnComplete(() =>
                        {
                            
                            itemsMatchBox.Remove(itemObj.gameObject);
                            ItemContainer.Instance.item_container.Remove(itemObj);
                            Debug.Log("remove item");
                            Destroy(itemObj.gameObject);
                            UpdateItemInBox();
                            GameController.Instance.CheckStatusGame();
                        });
                    });

                }
            }
        }
        else
        {
            GameController.Instance.CheckStatusGame();
        }
        
        CheckWarning();
    }

    public void ActiveWarning(bool isActive)
    {
        if (isActive)
        {
            warning.enabled = true;
            DOTween.Sequence()
    .Append(warning.DOFade(maxAlpha, fadeDuration))
    .AppendInterval(0.5f) // Dừng 0.5 giây
    .Append(warning.DOFade(minAlpha, fadeDuration))
    .SetLoops(-1);
        }
        else
        {
            warning.enabled = false;
        }
    }

    public void CheckWarning()
    {
        if (itemsMatchBox.Count >= 6)
        {
            Debug.Log("warning");
            ActiveWarning(true);
        }
        else
        {
            ActiveWarning(false);
        }
    }

    public bool CanNotFill()
    {
        return itemsMatchBox.Count >= NumberMatchBarSlot;
    }
}
