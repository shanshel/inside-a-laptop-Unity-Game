using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
    public GameObject hitGroundParticleObject;
    public float minX, maxX;

    Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(baseScale, .2f).OnComplete(()=>
        {
            transform.DOLocalMoveX(transform.position.x + 0.1f, 0.2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        });

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && transform.position.y < -1f)
        {
            hitGroundParticleObject.SetActive(true);
            transform.DOKill();
            Destroy(gameObject, 3f);
            CameraManager._inst.ShakePosition(0.1f, 0.1f);
        }
    
    }


}
