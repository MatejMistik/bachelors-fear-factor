using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RayCastWeapon : MonoBehaviour
{ 
    [SerializeField] float range = 30f;
        public float damage = 60f;
    [SerializeField] float fireRate = 0.1f;
    [SerializeField] float impactForce = 60f;
    [SerializeField] int magazineSize = 30;
    [SerializeField] float projectileSpeed;
    public float reloadTime, timeBetweenShots = 0.1f;
    public int bulletsPerTap;
    int bulletsLeft;

    bool readyToShoot, reloading;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Transform player;
    public GameObject projectile;
    public Transform bulletHole;



    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }


    public void ShootTest()
    {
        
        transform.LookAt(player);
        muzzleFlash.Play();
        GameObject bullet = Instantiate(projectile, bulletHole.transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletHole.transform.forward * projectileSpeed, ForceMode.Impulse);
        Destroy(bullet, 2);
       /* if (Physics.Raycast(bulletHole.transform.position, bulletHole.transform.forward, out RaycastHit hit, range))
        {

            Debug.Log(range);
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 1f);


        }
       */
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


        transform.LookAt(player);
        readyToShoot = false;
        muzzleFlash.Play();
        GameObject bullet = Instantiate(projectile, bulletHole.transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Debug.Log(bullet.transform.rotation);
        bullet.transform.Rotate(0, 90, 0);
        Debug.Log(bullet.transform.rotation);
        rb.AddForce(bulletHole.transform.forward * projectileSpeed, ForceMode.Impulse);
        Destroy(bullet, 20);
        /*if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range))
        {
       

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 1f);


        }
        */

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
