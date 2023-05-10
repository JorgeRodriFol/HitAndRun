using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 startPos;
    public Vector3 endPos;
    public bool moveRight = true;

    void Start()
    {
        startPos = transform.position;
        endPos.x = transform.position.x + 2.0f;
        endPos.y = transform.position.y;
        endPos.z = transform.position.z;
    }

    void Update()
    {
        if (moveRight) // Si se mueve hacia la derecha
        {
            transform.position += Vector3.right * speed * Time.deltaTime; // Mover el objeto hacia la derecha
            if (transform.position.x >= endPos.x) // Si ha alcanzado la posición final
            {
                moveRight = false; // Cambiar la dirección de movimiento a la izquierda
            }
        }
        else // Si se mueve hacia la izquierda
        {
            transform.position += Vector3.left * speed * Time.deltaTime; // Mover el objeto hacia la izquierda
            if (transform.position.x <= startPos.x) // Si ha alcanzado la posición inicial
            {
                moveRight = true; // Cambiar la direcci
            }
        }
    }
}