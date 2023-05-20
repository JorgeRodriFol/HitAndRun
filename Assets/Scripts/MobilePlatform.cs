using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 startPos;
    public Vector3 endPos;
    public bool moveRight = true;
    private bool JohnOn;
    public float TimeWaiting;

    void Start()
    {
        startPos = transform.position;
        JohnOn = false;
    }

    void Update()
    {
        if (JohnOn)
        {
            if (moveRight && Time.time > TimeWaiting + 1.0f) // Si se mueve hacia la derecha
            {
                transform.position += Vector3.right * speed * Time.deltaTime; // Mover el objeto hacia la derecha
                if (transform.position.x >= endPos.x) // Si ha alcanzado la posición final
                {
                    moveRight = false; // Cambiar la dirección de movimiento a la izquierda
                    TimeWaiting = Time.time;
                }
            }
            else if (!moveRight && Time.time > TimeWaiting + 1.0f) // Si se mueve hacia la izquierda
            {
                transform.position += Vector3.left * speed * Time.deltaTime; // Mover el objeto hacia la izquierda
                if (transform.position.x <= startPos.x) // Si ha alcanzado la posición inicial
                {
                    moveRight = true; // Cambiar la direcci
                    TimeWaiting = Time.time;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject john = GameObject.Find("john");

        if (collision.gameObject == john)
        {
            JohnOn = true;
        }
    }
}