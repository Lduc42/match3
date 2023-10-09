using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemElement : MonoBehaviour
{
    public int ID;

    public Vector3 originalRotation;
    public Vector3 originalScale;

    private void OnEnable()
    {
        originalRotation = this.transform.rotation.eulerAngles; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
