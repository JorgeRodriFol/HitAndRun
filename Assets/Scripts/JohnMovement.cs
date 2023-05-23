using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JohnMovement : MonoBehaviour
{
    private float NormalLastShoot;
    private float MachineLastShoot;
    private float OmegaLastShoot;
    public GameObject NormalBulletPrefab;
    public GameObject OmegaBulletPrefab;
    public GameObject MachineBullet;
    public float JumpForce;
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public bool Grounded;
    private Animator Animator;
    public int WeaponMode;
    public float GravityForce = 5.0f;
    public bool InMobilePlatform;
    public GameObject MobilePlatform;
    private int Health;
    private GameObject Heart;
    public AudioClip Sound;

    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        WeaponMode = 1;
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
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
        if(WeaponMode == 1)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > NormalLastShoot + 0.5f)
            {
                ShootDamage();
                NormalLastShoot = Time.time;
            }
        }else if(WeaponMode == 2)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > OmegaLastShoot + 2.0f)
            {
                ShootDamage();
                OmegaLastShoot = Time.time;
            }
        }else if (WeaponMode == 0)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > MachineLastShoot + 0.15f)
            {
                ShootDamage();
                MachineLastShoot = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            WeaponMode++;
            if(WeaponMode > 2)
            {
                WeaponMode = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            WeaponMode--;
            if (WeaponMode < 0)
            {
                WeaponMode = 2;
            }
        }
        if(Health <= 0)
        {
            Animator.SetBool("Dead", Health == 0);
        }
    }

    private void ShootDamage()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;
        if(WeaponMode == 0)
        {
            GameObject bullet = Instantiate(MachineBullet, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<BulletScript>().SetDirection(direction);
        }
        else if(WeaponMode == 1)
        {
            GameObject bullet = Instantiate(NormalBulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<BulletScript>().SetDirection(direction);
        }else if(WeaponMode == 2)
        {
            GameObject bullet = Instantiate(OmegaBulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<OmegaBulletScript>().SetDirection(direction);
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
        if (collision.gameObject == NormalBullet || collision.gameObject == obstaculos || collision.gameObject == MobilePlatform || collision.gameObject == BossBullet)
        {
            if (collision.gameObject == NormalBullet || collision.gameObject == obstaculos)
            {
                Health -= 25;
                Animator animator = Heart.GetComponent<Animator>();
                animator.SetInteger("Hit", Health);
            }
            if (collision.gameObject == MobilePlatform)
            {
                transform.parent = collision.transform;
                InMobilePlatform = true;
            }
            if (collision.gameObject == BossBullet)
            {
                Health -= 50;
                Animator animator = Heart.GetComponent<Animator>();
                animator.SetInteger("Hit", Health);
            }
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == MobilePlatform)
        {
            transform.parent = null;
            InMobilePlatform = false;
        }
    }
}
