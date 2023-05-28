using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnShootScript : MonoBehaviour
{
    private float NormalLastShoot;
    private float MachineLastShoot;
    private float OmegaLastShoot;
    public GameObject NormalBulletPrefab;
    public GameObject OmegaBulletPrefab;
    public GameObject MachineBullet;
    public int WeaponMode;
    public AudioClip ChangeWeapon;

    void Start()
    {
        WeaponMode = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponMode == 1)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > NormalLastShoot + 0.5f)
            {
                ShootDamage();
                NormalLastShoot = Time.time;
            }
        }
        else if (WeaponMode == 2)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > OmegaLastShoot + 2.0f)
            {
                ShootDamage();
                OmegaLastShoot = Time.time;
            }
        }
        else if (WeaponMode == 0)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > MachineLastShoot + 0.15f)
            {
                ShootDamage();
                MachineLastShoot = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(ChangeWeapon);
            WeaponMode++;
            if (WeaponMode > 2)
            {
                WeaponMode = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(ChangeWeapon);
            WeaponMode--;
            if (WeaponMode < 0)
            {
                WeaponMode = 2;
            }
        }
    }
    private void ShootDamage()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;
        if (WeaponMode == 0)
        {
            GameObject bullet = Instantiate(MachineBullet, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<BulletScript>().SetDirection(direction);
        }
        else if (WeaponMode == 1)
        {
            GameObject bullet = Instantiate(NormalBulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<BulletScript>().SetDirection(direction);
        }
        else if (WeaponMode == 2)
        {
            GameObject bullet = Instantiate(OmegaBulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<OmegaBulletScript>().SetDirection(direction);
        }
    }
}
