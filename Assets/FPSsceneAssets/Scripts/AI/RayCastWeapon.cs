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
    public float reloadTime;
    public int bulletsPerTap;
    int bulletsLeft;

    bool readyToShoot, reloading;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Transform player;
    public GameObject projectile;
    public Transform bulletHole;

    public int iterations = 10;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }


    public void AimAtTarget(Vector3 targetPosition)
    {
        Vector3 aimDirection = bulletHole.forward;
        Vector3 targetDirection = targetPosition - bulletHole.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);

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

        Vector3 targetPosition = player.position;
        for (int i = 0; i < iterations; i++)
        {
            AimAtTarget(targetPosition);
        }
        readyToShoot = false;
        muzzleFlash.Play();
        GameObject bullet = Instantiate(projectile, bulletHole.transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        bullet.transform.Rotate(0, 90, 0);
        rb.AddForce(bulletHole.transform.forward * projectileSpeed, ForceMode.Impulse);
        Destroy(bullet, 20);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range))
        {
            Debug.Log(hit);
            var hitBox = hit.collider.GetComponent<PlayerHitbox>();

            if (hitBox)
            {
                hitBox.OnRaycasthit(this);
            }

            Debug.Log(range);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 1f);


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
