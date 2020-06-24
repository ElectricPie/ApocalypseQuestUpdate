using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {
    public delegate void UpdateAmmo(int ammo);
    public static event UpdateAmmo OnUpdateAmmo;

    public GameObject bulletPrefab;
    public Transform bulletSpawn1;
    public Transform bulletSpawn2;
    public float fireTime = 0.5f;

    public int ammo = 20;
    private int ammoCapacity;
    private float reloadCount;

    private bool isFiring = false;
    public bool isReloaded = true;
    private bool leftGun = false;

    private void Start()
    {
        SendAmmoCount();
        ammoCapacity = ammo;
    }

    void SetFiring()
    {
        isFiring = false;

    }
	
	void Fire()
    {
        isFiring = true;

        if (leftGun)
        {
            Instantiate(bulletPrefab, bulletSpawn1.position, bulletSpawn1.rotation);
            leftGun = false;
            ammo -= 1;
        }
        else
        {
            Instantiate(bulletPrefab, bulletSpawn2.position, bulletSpawn2.rotation);
            leftGun = true;
            ammo -= 1;
        }

        if(GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play();
        }

        Invoke("SetFiring", fireTime);
	}

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if (ammo > 0 && isReloaded)
            {
                if (!isFiring)
                {
                    Fire();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(ammo <= 0)
        {
            isReloaded = false; 
            reloadCount += 0.05f;
            if (reloadCount >= 5)
            {
                ammo = ammoCapacity;
                reloadCount = 0;
                isReloaded = true;
            }
        }
        SendAmmoCount();
    }

    void SendAmmoCount()
    {
        if (OnUpdateAmmo != null)
        {
            OnUpdateAmmo(ammo);
        }
    }
}
