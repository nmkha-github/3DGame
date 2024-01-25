using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Gun : MonoBehaviour
{
    public Transform gunBarrel;

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public Animator gunAnimator;
    public Camera fpscamera;
    public ParticleSystem muzzleflash;
    //public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    private void Start()
    {
        gunAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        //running
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            gunAnimator.enabled = true;
        } else
        {
            gunAnimator.enabled = false;
        }
    }

    void Shoot()
    {
        muzzleflash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpscamera.transform.position + fpscamera.transform.forward.normalized * 3, fpscamera.transform.forward, out hit, range))
        {
            UnityEngine.Debug.Log(hit.transform.name);


            //instantiate a new bullet
            GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/PlayerBullet") as GameObject, gunBarrel.position, fpscamera.transform.rotation);
            //add force rigidbody of the bullet

            Vector3 shootDirection = (hit.point - gunBarrel.position).normalized;
            //add force rigidbody of the bullet
            bullet.GetComponent<Rigidbody>().velocity =  shootDirection * 40;

            //Target target = hit.transform.GetComponent<Target>();
            //if (target != null)
            //{
            //    target.TakeDamage(damage);
            //}

            //if (hit.rigidbody != null)
            //{
            //    hit.rigidbody.AddForce(-hit.normal * impactForce);
            //}

            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impactGO, 2f);
        }

    }

}

