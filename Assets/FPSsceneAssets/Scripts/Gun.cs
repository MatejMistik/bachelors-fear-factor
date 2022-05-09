using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Gun : MonoBehaviour
{
    [SerializeField] float range = 100f;
    public float damage = 60f;
    [SerializeField] float fireRate = 0.1f;
    [SerializeField] float impactForce = 60f;
    [SerializeField] int magazineSize = 5;
    public float spread,reloadTime;
    public int bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public TextMeshProUGUI text;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

        //SetText
       
        if(reloading == true)
        {
            text.SetText("Reloading");
        }
        else
        {
            text.SetText(bulletsLeft + " / " + magazineSize);
        }
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        if(bulletsLeft == 0 && !reloading) Reload();
        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
           
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }


    void Shoot()
    {
        readyToShoot = false;
        muzzleFlash.Play();
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, range))
        {
            var hitBox = hit.collider.GetComponent<Hitbox>();

            if (hitBox)
            {
                hitBox.OnRaycasthit(this);
            }


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
