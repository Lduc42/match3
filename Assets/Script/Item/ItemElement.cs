using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemElement : MonoBehaviour
{
    [SerializeField] private int id;
    public int ID
    {
        get => id;
        set => id = value;
    }
    public int SpawnedID { get; set; }

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

    public void SetDefaultRotation()
    {
        this.transform.DOLocalRotate(originalRotation, .2f);
    }
}
