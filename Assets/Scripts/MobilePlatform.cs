using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 startPos;
    public Vector3 endPos;
    public bool moveRight;
    public bool moveUp;
    private bool stop;
    public float TimeWaiting;
    private GameObject PointStop;

    void Start()
    {
        startPos = transform.position;
        stop = false;
        moveRight=false;
        moveUp = true;
        PointStop = GameObject.Find("FirstFinishPoint");
    }

    void Update()
    {
        if (stop)
        {
            if(moveUp)
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
                if (transform.position.y >= PointStop.transform.position.y)
                {
                    moveUp = false;
                    moveRight = true;
                    PointStop = GameObject.Find("SecondFinishPoint");
                }

            }
            else if (moveRight) 
            {
                transform.position += Vector3.right * speed * Time.deltaTime; 
                if (transform.position.x >= PointStop.transform.position.x) 
                {
                    moveRight = false;
                    PointStop = GameObject.Find("FinalFinishPoint");
                }
            }
            else
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
                if (transform.position.y <= PointStop.transform.position.y)
                {
                    stop = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject john = GameObject.Find("john");

        if (collision.gameObject == john)
        {
            stop = true;
        }
    }
}