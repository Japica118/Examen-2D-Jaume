using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rBody;
    private float horizontal;
    private Animator animator;
    private SFXManager sfxManager;
    private BGMManager bgmManager;

    [SerializeField] private float speed = 5;
    [SerializeField] private float jump = 10;
    [SerializeField] Transform groundSensor;
    [SerializeField] private float sensorRadius;
    [SerializeField] LayerMask sensorLayer;
    [SerializeField] private bool isGrounded;

    void Awake()
    {
       rBody = GetComponent<Rigidbody2D>();

       animator = GetComponentInChildren<Animator>();

       sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();

       bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();

    }

    void Update()
    {
        Movement();

        Jump();
    }

    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");

        if(horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("isRunning", true);
        }
        else if(horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("isRunning", true);
        }

        else if(horizontal == 0)
        {
            animator.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundSensor.position, sensorRadius, sensorLayer);

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            rBody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            sfxManager.JumpSound();
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.layer == 3)
        {
            animator.SetBool("isJumping", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Star")
        {
            GameManager.Instance.LoadLevel(1);

            sfxManager.StarSound();

            bgmManager.StopBGM();
        }

        else if(collider.gameObject.tag == "Coin")
        {
            GameManager.Instance.AddCoin(collider.gameObject);

            sfxManager.CoinSound();
        }
    }
    
    void FixedUpdate()
    {
        rBody.velocity = new Vector2(horizontal * speed, rBody.velocity.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(groundSensor.position, sensorRadius);
    }
        
    
}
