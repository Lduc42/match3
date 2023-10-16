using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupUIBase : MonoBehaviour
{
    protected void HiddenPopup()
    {
        this.transform.DOScale(new Vector3(.1f, .1f,.1f), .5f).SetEase(Ease.OutSine).OnComplete(()=> {
            this.gameObject.SetActive(false);
        });
    }

    protected void ShowPopup()
    {
        Debug.Log("show popup");
        this.gameObject.SetActive(true);
        this.transform.DOScale(new Vector3(1f, 1f,1f), .5f).SetEase(Ease.OutSine).OnComplete(() => {
        });
    }
}
