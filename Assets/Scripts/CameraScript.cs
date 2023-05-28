using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject John;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = John.transform.position.x;
        position.y = John.transform.position.y + 0.2f;
        transform.position = position;
    }
}
