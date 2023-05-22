using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;
    private Animator Animator;
    private GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Boss = GameObject.Find("BossHead");
        Collider2D colliderA = GetComponent<Collider2D>();
        Collider2D colliderB = Boss.GetComponent<Collider2D>();

        if (colliderA != null && colliderB != null)
        {
            Physics2D.IgnoreCollision(colliderA, colliderB);
        }
        else
        {
            UnityEngine.Debug.LogError("Los colliders no están asignados correctamente.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Speed = 0;
        Animator.SetBool("Hit", true);
    }
}
