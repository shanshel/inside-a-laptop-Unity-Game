using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraManager : MonoBehaviour
{
    public static CameraManager _inst;
    Camera cameraCache;
    Vector3 basePos;
    Quaternion baseRotation;
    Tweener positionTweener, rotationTweener;

    float rotPower = 2f;
    float targetRotation = 0f;
    private void Awake()
    {
        if (_inst != null)
        {
            Destroy(gameObject);
            return;
        }
        _inst = this;
        cameraCache = Camera.main;
        basePos = cameraCache.transform.position;
        baseRotation = cameraCache.transform.rotation;
        


    }

    private void Start()
    {
        InvokeRepeating(nameof(ChangeCameraRotation), 2f, 10f);
    }

    private void Update()
    {

        /*
        var playerXPos = Player._inst.transform.position.x;

      
        cameraCache.transform.rotation = Quaternion.Euler(0, 0, cameraCache.transform.rotation.eulerAngles.z + (playerXPos * rotPower * Time.deltaTime));
        baseRotation = cameraCache.transform.rotation;
        
        */
        cameraCache.transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(cameraCache.transform.rotation.eulerAngles.z, targetRotation, Time.deltaTime));
        baseRotation = cameraCache.transform.rotation;
        var angle = cameraCache.transform.rotation.eulerAngles.z;
        if (angle > 180f)
            angle = Mathf.Abs(360 - angle);
        cameraCache.orthographicSize = 5.4f + (angle / 9f);

    }

    public void ShakePosition(float duration = 0.1f, float power = 0.1f)
    {

        if (positionTweener != null) positionTweener.Kill();
        cameraCache.transform.position = basePos;
        positionTweener = cameraCache.transform.DOShakePosition(duration, power, 2, 40f).SetAutoKill(false).SetEase(Ease.InBounce).OnComplete(() => {
            cameraCache.transform.position = basePos;
        });
      
       
    }

    /*
    public void ShakeRotation(float duration = 0.1f, float power = 0.1f)
    {
        if (rotationTweener != null) rotationTweener.Kill();
        cameraCache.transform.rotation = baseRotation;
        rotationTweener = cameraCache.transform.DOShakeRotation(duration, power, 0, 40f).SetAutoKill(false).SetEase(Ease.InBounce).OnComplete(() => {
            cameraCache.transform.rotation = baseRotation;
        });
    }
    */

    void ChangeCameraRotation()
    {
        targetRotation = Random.Range(-80f, 80f);
    }
}
