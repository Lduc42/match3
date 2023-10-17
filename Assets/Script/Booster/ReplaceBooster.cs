using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceBooster : MonoBehaviour
{
    public float forceMin = 5.0f; // Độ lớn tối thiểu của lực
    public float forceMax = 10.0f; // Độ lớn tối đa của lực

    public void ReplaceAllItem(List<ItemElement> lstItem)
    {
        foreach(ItemElement item in lstItem)
        {
            float forceX = Random.Range(-forceMin, forceMax);
            float forceY = Random.Range(forceMin, forceMax);
            float forceZ = Random.Range(-forceMin, forceMax);
            Vector3 tornadoForce = new Vector3(forceX, forceY, forceZ);
            item.gameObject.GetComponent<Rigidbody>().AddForce(tornadoForce, ForceMode.Impulse);
        }
    }
}
