using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;
    public AudioClip Sound;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Direction == Vector2.left)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (Direction == Vector2.right)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
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
    }
}
