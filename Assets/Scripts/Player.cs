using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public static Player _inst;
    //Components 
    Rigidbody2D rb;
    Camera mainCamera;

    //Status
    bool isDied = false;

    //Jump Related
    public float jumpPower;
    int jumpCount = 0;
    float lastHInputBeforeJump;

    //Scale
    Vector3 baseScale;
  

    //Audio 
    public AudioSource jumpSFX, dieSFX;

    //VFX 
    public GameObject dieParticleSystemObject;
    public GameObject playerSpriteObject;

    private void Awake()
    {
        if (_inst != null)
        {
            Destroy(gameObject);
            return;
        }
        _inst = this;
        mainCamera = Camera.main;
    }

    void Start()
    {
        baseScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        VelocityUpdate();
        JumpUpdate();
       
    }


    float zRotValue = 0f;
    void VelocityUpdate()
    {
        zRotValue = mainCamera.transform.rotation.eulerAngles.z;

        if (zRotValue > 180f)
            zRotValue = zRotValue - 360;


        //Round to 0
        if (zRotValue < 10f && zRotValue > 0f)
            zRotValue = 0;
        else if (zRotValue > -10 && zRotValue < 0f)
            zRotValue = 0f;
        else if (zRotValue < 25f && zRotValue > 0)
            zRotValue = 25f;
        else if (zRotValue > -25f && zRotValue < 0)
            zRotValue = -25f;





        rb.velocity += new Vector2(zRotValue, 0f) * Time.deltaTime;
    }
    void JumpUpdate()
    {
        if (isDied) return;
        if (jumpCount < 2 && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    void Jump()
    {
        rb.velocity = Vector2.zero;
        lastHInputBeforeJump = Input.GetAxisRaw("Horizontal");
        jumpCount++;
        jumpSFX.Play();

        //CameraManager._inst.ShakeRotation(0.1f, 0.2f);
        CameraManager._inst.ShakePosition(0.1f, 0.2f);
        var addPower = 1 + Mathf.Abs( zRotValue * 0.008f);
        if (jumpCount == 1)
        {
            rb.AddForce(new Vector2(lastHInputBeforeJump * addPower, 1 * 1.25f) * jumpPower, ForceMode2D.Impulse);
            rb.AddTorque(jumpPower / 10 * -lastHInputBeforeJump, ForceMode2D.Impulse);
        }
            
        else if (jumpCount == 2)
        {
            //1.4f
            rb.AddForce(new Vector2(lastHInputBeforeJump * addPower, 0.2f) * jumpPower * 1.5f, ForceMode2D.Impulse);
            rb.AddTorque(jumpPower / 10 * -lastHInputBeforeJump * 2f, ForceMode2D.Impulse);
        }
            
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDied) return;
        if (jumpCount > 0)
        {
            transform.DOKill();
            transform.localScale = baseScale;
            transform.DOShakeScale(.1f, .2f, 1, 30).OnComplete(() => {
                transform.localScale = baseScale;
            });
            jumpCount = 0;
        }


 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Die();
        }
    }


    void Die()
    {
        if (isDied) return;
        isDied = true;
        dieSFX.Play();
        dieParticleSystemObject.SetActive(true);
        playerSpriteObject.SetActive(false);
        //CameraManager._inst.ShakeRotation(0.25f, 5f);
        CameraManager._inst.ShakePosition(0.3f, 0.6f);
        Invoke(nameof(ReloadScene), 2f);
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
