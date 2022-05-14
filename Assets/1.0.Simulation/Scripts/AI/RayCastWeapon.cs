using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/*
***************************************************************************************
*	Title: Shooting with Raycasts - Unity Tutorial
*	Author: Brackeys
*   Date: 19. 4. 2017
*	Code version: 1.0
*	Availability: https://www.youtube.com/watch?v=THnivyG0Mvo&ab_channel=Brackeys
*	
*	Implementation is based on this tutorial
*
***************************************************************************************/

public class RayCastWeapon : MonoBehaviour
{ 
    [SerializeField] float range = 30f;
    public float damage;
    [SerializeField] float fireRate;
    //[SerializeField] float impactForce;
    [SerializeField] int magazineSize;
    [SerializeField] float projectileSpeed;
    public float reloadTime;
    public int bulletsPerTap;
    int bulletsLeft;

    bool readyToShoot, reloading;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Transform playerTransform;
    public GameObject projectile;
    public Transform bulletHole;
    Transform transformAi;



    public int iterations = 10;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        
    }

   

    private void Start()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        transformAi = GameObject.FindWithTag("Enemy").transform;
    }


    

    public void PrepareToShoot()
    {
        
        if (bulletsLeft < 1 && !reloading) Reload();
        //Shoot
        if (readyToShoot && !reloading && bulletsLeft > 0)
        {

            bulletsLeft -= bulletsPerTap;
            Shoot();
        }
    }



    void Shoot()
    {
        readyToShoot = false;
        muzzleFlash.Play();

        // projectiles option
        /*
        GameObject bullet = Instantiate(projectile, bulletHole.transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        bullet.transform.Rotate(0, 90, 0);
        rb.AddForce(bulletHole.transform.forward * projectileSpeed, ForceMode.Impulse);
        Destroy(bullet, 20);
        */
        if (Physics.Raycast(bulletHole.transform.position, bulletHole.transform.forward, out RaycastHit hit, range))
        {
            Debug.Log(hit.collider);
            var hitBox = hit.collider.GetComponent<PlayerHitbox>();
            Debug.Log(hitBox);

            if (hitBox)
            {
                Debug.Log("hit");
                hitBox.OnRaycasthit(this);
            }

            //projectiles option
            /*
            Debug.Log(range);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 1f);
            */

        }
        
        //Debug.Log("shot");
        Invoke(nameof(ResetShot), fireRate);
        if ( bulletsLeft > 1)
        {
            Invoke(nameof(PrepareToShoot), 0f);
        }
            
    }


    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        Debug.Log("reloading");
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
