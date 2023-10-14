using UnityEngine;
using DG.Tweening;

public class ItemSelector : MonoBehaviour
{
    public int currentItemIndex;
    public int preItemIndex = 999;
    private bool isDragging;

    public ItemElement currentItem;
    public ItemElement preItem;

    [HideInInspector] public Vector3 originalItemScale;

    public static ItemSelector Instance { get; set; }
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("nha chuot");
            isDragging = false;
            GameObject obj = CheckCollisionItem();
            if(obj != null)
            {
                MatchBoxController.Instance.AddItemToBox(obj);
            }
        }

        if (isDragging)
        {
            CheckCollisionItem();
        }
        
    }

    private GameObject CheckCollisionItem()
    {
        // Create a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast and check for collisions
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the ray hit the collider of your 3D model
            if (hit.collider.gameObject.tag == "Item")
            {
                GameObject objectHit = OnSelectItem(hit);
                Debug.Log(objectHit.name);
                return objectHit;
                // Collision detected between the mouse and the 3D model
                Debug.Log("Collision with " + objectHit.name);
            }
            else
            {
                ItemElement currentSelectedItem = ItemContainer.Instance.item_container[currentItemIndex].gameObject.GetComponent<ItemElement>();
                foreach(GameObject obj in MatchBoxController.Instance.itemsMatchBox)
                {
                    ItemElement item_element = obj.GetComponent<ItemElement>();
                    if(item_element.SpawnedID == currentSelectedItem.SpawnedID)
                    {
                        return null;
                    }
                }
                if (preItem) DropItem(preItem);
                //DropItem(currentSelectedItem);
                /*Debug.Log("scale this");*/
                preItemIndex = 999;
            }
        }

        return null;
    }

    private GameObject OnSelectItem(RaycastHit hit)
    {
        GameObject objectHit = hit.collider.gameObject;
        Vector3 objectHitPosition = objectHit.transform.position;
        ItemElement item = objectHit.GetComponent<ItemElement>();
        currentItemIndex = item.SpawnedID;


        preItem = currentItem;
        currentItem = item;
        if(currentItem && preItem && currentItem.SpawnedID != preItem.SpawnedID || !preItem)
        {
            PickUpItem(objectHit, objectHitPosition);

            foreach(GameObject obj in MatchBoxController.Instance.itemsMatchBox)
            {
                if(obj.GetComponent<ItemElement>().SpawnedID == preItemIndex)
                {
                    return objectHit;
                }
            }
            //if(preItemIndex != 999)
            //{
            //    ItemElement itemDrop = GameController.Instance.itemContainer.FindObjectByInstanceID(preItemIndex);
            //    if(itemDrop)
            //    DropItem(itemDrop);
            //}
            if(preItem)
            DropItem(preItem);
            
            preItemIndex = currentItemIndex;
        }
        else
        {
            Debug.Log("preItemIndex = currentItemIndex");
        }

        return objectHit;
    }

    private void PickUpItem(GameObject objectHit, Vector3 objectHitPosition)
    {
        if(objectHit.transform.position.y < 1) objectHit.transform.DOMoveY(objectHitPosition.y + 3f, .2f);

        Rigidbody rb = objectHit.GetComponent<Rigidbody>();
        objectHit.GetComponent<Rigidbody>().useGravity = false;
        objectHit.GetComponent<Rigidbody>().freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        objectHit.transform.DOScale(new Vector3(5, 5, 5), .1f);
        Debug.Log("scale");
    }

    private void DropItem(ItemElement item)
    {
        if (!item) Debug.Log("item null");
        item.transform.DOScale(originalItemScale, .1f);
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
