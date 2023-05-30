using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculosScript : MonoBehaviour
{
    private JohnMovement johnScript;

    // Start is called before the first frame update
    void Start()
    {
        johnScript = GameObject.Find("john").GetComponent<JohnMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UnityEngine.Debug.Log("John detectado");
            johnScript.Dañado(25);
        }
    }
}
