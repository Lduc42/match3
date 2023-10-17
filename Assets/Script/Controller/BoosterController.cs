using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterController : MonoBehaviour
{
    [SerializeField] protected ReplaceBooster replaceBooster;
    [SerializeField] protected Button ReplaceBoosterBtn;
    // Start is called before the first frame update
    void Start()
    {
        InitBooster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitBooster()
    {
        ReplaceBoosterBtn.onClick.AddListener(()=> {
            List<ItemElement> lstItem = ItemContainer.Instance.item_container;
            replaceBooster.ReplaceAllItem(lstItem);
        });
    }
}
