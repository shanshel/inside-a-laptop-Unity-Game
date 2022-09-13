using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Eye : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScaleY(0f, Random.Range(0.5f, 1f)).SetEase(Ease.InBack).SetLoops(-1, LoopType.Yoyo);
    }


}
