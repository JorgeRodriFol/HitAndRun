using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private GameObject John;
    public GameObject BossBullet;
    private int Health;
    private float Recarga;
    // Start is called before the first frame update
    void Start()
    {
        John = GameObject.Find("john");
        Health = 200;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time > Recarga + 2.0f)
        {
            Shoot();
            Recarga = Time.time;
        }
        if(Health <= 0)
        {
            UnityEngine.Debug.Log("Muerto");
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject NormalBullet = GameObject.Find("BulletPrefab(Clone)");
        GameObject OmegaBullet = GameObject.Find("OmegaBullet(Clone)");
        GameObject MachinelBullet = GameObject.Find("MachinBullet(Clone)");

        if (collision.gameObject == NormalBullet)
        {
            UnityEngine.Debug.Log("Golpeado bala amarilla");
            Health = Health - 15;
        }
        else if (collision.gameObject == OmegaBullet)
        {
            UnityEngine.Debug.Log("Golpeado bala roja");
            Health = Health - 30;
        }
        else if (collision.gameObject == MachinelBullet)
        {
            UnityEngine.Debug.Log("Golpeado bala azul");
            Health = Health - 7;
        }
        UnityEngine.Debug.Log("Vida restante: "+ Health);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Shoot()
    {
        Vector3 direccion = John.transform.position - transform.position;
        GameObject bullet = Instantiate(BossBullet, transform.position + direccion * 0.1f, Quaternion.identity);
        bullet.GetComponent<BossBulletScript>().SetDirection(direccion);
    }
}
