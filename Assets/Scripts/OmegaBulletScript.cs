using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmegaBulletScript : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;
    private bool Collision;
    private Animator Animator;
    public AudioClip Sound;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
        Animator = GetComponent<Animator>();
        Speed = 1;
        Rigidbody2D = GetComponent<Rigidbody2D>();
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
        Animator.SetBool("Collision", collision.gameObject != null);
    }
}
