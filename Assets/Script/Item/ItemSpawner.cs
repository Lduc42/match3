using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header ("Item Data:")]
    [SerializeField] private SOLevelData itemList;
    [SerializeField] private GameObject itemContainer;
    [SerializeField] private List<SpawnData> itemsType;

    [Header("Spawn Position:")]
    [SerializeField] protected float spawnXLimitPosition;
    [SerializeField] protected float spawnZLimitPosition;

    private int indexSpawn;
    
    private void OnEnable()
    {
        //itemsType = itemList.itemTypes;
    }

    // Start is called before the first frame update
    void Start()
    {
        //SpawnListItem(itemsType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnListItem(List<SpawnData> items)
    {
        for(int i = 0; i < items.Count; i++)
        {
            SpawnItemType(items[i].itemType, items[i].numberItem);
        }
    }

    protected void SpawnItemType(GameObject itemType, int numberItem)
    {
        for(int i = 0; i < numberItem; i++)
        {
            GameObject obj = Instantiate(itemType, itemContainer.transform);
            obj.GetComponent<ItemElement>().SpawnedID = indexSpawn;
            SetRandomItemPosition(obj);
            indexSpawn++;

            ItemSelector.Instance.originalItemScale = obj.transform.localScale;
        }
    }

    protected void SetRandomItemPosition(GameObject item)
    {
        float random_x = Random.Range(-spawnXLimitPosition, spawnXLimitPosition);
        float random_y = Random.Range(-spawnZLimitPosition, spawnZLimitPosition);

        item.transform.position = new Vector3(random_x, item.transform.position.y, random_y);
    }
}
