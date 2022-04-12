using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RayCastWeapon : MonoBehaviour
{ 
    [SerializeField] float range = 100f;
        public float damage = 620f;
    [SerializeField] float fireRate = 0.1f;
    [SerializeField] float impactForce = 60f;
    [SerializeField] int magazineSize = 30;
    [SerializeField] float projectileSpeed;
    public float reloadTime, timeBetweenShots = 0.1f;
    public int bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Transform player;
    public GameObject projectile;



    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }



    public void PrepareToShoot()
    {

        if (bulletsLeft < magazineSize && !reloading) Reload();
        if (bulletsLeft == 0 && !reloading) Reload();
        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {

            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }


    void Shoot()
    {


        transform.LookAt(player);
        readyToShoot = false;
        muzzleFlash.Play();
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
        Destroy(bullet, 1);
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range))
        {
       

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 1f);


        }

        bulletsLeft--;
        bulletsShot--;

        Invoke(nameof(ResetShot), fireRate);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke(nameof(Shoot), 0f);
    }


    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
