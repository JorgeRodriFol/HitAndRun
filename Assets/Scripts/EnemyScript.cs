using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameObject John;
    public GameObject Bullet;
    private bool movingToEndPosition = true;
    private float LastShoot;
    private bool Shooting;
    public float speed;
    private Animator Animator;
    private int Health;
    public Vector3 startPos;
    public Vector3 endPos;
    public bool moveRight = true;
    // Start is called before the first frame update
    void Start()
    {
        John = GameObject.Find("john");
        Health = 100;
        Animator = GetComponent<Animator>();
        startPos = transform.position;
        endPos.x = transform.position.x + 1.0f;
        endPos.y = transform.position.y;
        endPos.z = transform.position.z;
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
            if (moveRight) // Si se mueve hacia la derecha
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                transform.position += Vector3.right * speed * Time.deltaTime; // Mover el objeto hacia la derecha
                if (transform.position.x >= endPos.x) // Si ha alcanzado la posición final
                {
                    moveRight = false; // Cambiar la dirección de movimiento a la izquierda
                }
            }
            else // Si se mueve hacia la izquierda
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                transform.position += Vector3.left * speed * Time.deltaTime; // Mover el objeto hacia la izquierda
                if (transform.position.x <= startPos.x) // Si ha alcanzado la posición inicial
                {
                    moveRight = true; // Cambiar la direcci
                }
            }
        }
        Animator.SetBool("Dead", Health == 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject NormalBullet = GameObject.Find("BulletPrefab(Clone)");
        GameObject OmegaBullet = GameObject.Find("OmegaBullet(Clone)");
        GameObject MachinelBullet = GameObject.Find("MachinBullet(Clone)");

        if (collision.gameObject == NormalBullet)
        {
            Health -= 25;
        }else if (collision.gameObject == OmegaBullet)
        {
            Health -= 100;
        }else if(collision.gameObject == MachinelBullet)
        {
            Health -= 10;
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
