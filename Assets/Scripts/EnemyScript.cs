using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject John;
    public GameObject Bullet;
    public GameObject XFinal;
    public GameObject XInitial;
    private bool movingToEndPosition = true;
    private float LastShoot;
    private bool Shooting;
    public float speed;
    private Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distancia = Math.Abs(John.transform.position.x - transform.position.x);
        Animator.SetBool("Running", distancia > 1.0f);
        if (distancia <= 1.0f && Time.time > LastShoot + 0.5f)
        {
            Vector3 direccion = John.transform.position - transform.position;
            if (direccion.x >= 0) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            Shoot();
            LastShoot = Time.time;
        }
        if (distancia > 1.0f)
        {
            if (movingToEndPosition)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                transform.position = Vector2.MoveTowards(transform.position, XFinal.transform.position, speed * Time.fixedDeltaTime);
                if (transform.position.x >= XFinal.transform.position.x)
                {
                    movingToEndPosition = false;
                }
            }
            else
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                transform.position = Vector2.MoveTowards(transform.position, XInitial.transform.position, speed * Time.fixedDeltaTime);
                if (transform.position.x <= XInitial.transform.position.x)
                {
                    movingToEndPosition = true;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObject = GameObject.Find("BulletPrefab(Clone)");

        if (collision.gameObject == gameObject)
        {
            Animator.SetBool("Dead", collision.gameObject == gameObject);
            Muerte();
        }
    }
    private void Muerte()
    {
        Destroy(gameObject);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;
        GameObject bullet = Instantiate(Bullet, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

}
