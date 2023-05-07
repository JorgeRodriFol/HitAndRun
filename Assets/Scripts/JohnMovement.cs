using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    private float LastShoot;
    public GameObject BulletPrefab;
    public float JumpForce;
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public bool Grounded;
    private Animator Animator;
    private int WeaponMode;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
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
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot+0.25f)
        {
            ShootDamage();
            LastShoot = Time.time;
        }
    }

    private void ShootDamage()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }
}
