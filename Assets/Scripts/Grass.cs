using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Grass : MonoBehaviour
{
    private void Start()
    {
        transform.DOShakeScale(3f, 0.02f, 1, 1f, true).SetLoops(-1, LoopType.Yoyo);
    }
}
