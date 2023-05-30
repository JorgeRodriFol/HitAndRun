using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JohnMovement : MonoBehaviour
{
    
    
    public float JumpForce;
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public bool Grounded;
    private Animator Animator;
    public float GravityForce = 5.0f;
    public bool InMobilePlatform;
    public GameObject MobilePlatform;
    public int Health;
    private GameObject Heart;
    public AudioClip Sound;

    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Heart = GameObject.Find("Health");
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        if(Horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }else if(Horizontal > 0.0f) 
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        Animator.SetBool("Running", Horizontal != 0.0f);
        
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
        if(Health <= 0)
        {
            Animator.SetBool("Dead", Health == 0);
        }
        
    }

    

    private void Die()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        if (InMobilePlatform)
        {
            Rigidbody2D.velocity = new Vector2(transform.parent.GetComponent<MobilePlatform>().speed, Rigidbody2D.velocity.y);
        }
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject NormalBullet = GameObject.Find("EnemyBullet(Clone)");
        GameObject BossBullet = GameObject.Find("BossBullet(Clone)");
        GameObject obstaculos = GameObject.Find("Obstaculos");
        GameObject mapa = GameObject.Find("Tilemap");
        if(collision.gameObject == mapa)
        {
            Grounded = true;
        }
        if (collision.gameObject == NormalBullet || collision.gameObject == obstaculos || collision.gameObject == BossBullet)
        {
            if (collision.gameObject == NormalBullet)
            {
                Dañado(25);
            }
            if (collision.gameObject == BossBullet)
            {
                Dañado(50);
            }
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
        }
        if (collision.gameObject == MobilePlatform)
        {
            transform.parent = collision.transform;
            InMobilePlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Grounded = false;
        if (collision.gameObject == MobilePlatform)
        {
            transform.parent = null;
            InMobilePlatform = false;
        }
    }

    public void Dañado(int dañoRecivido)
    {
        Health -= dañoRecivido;
        Animator animator = Heart.GetComponent<Animator>();
        animator.SetInteger("Hit", Health);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }
}
