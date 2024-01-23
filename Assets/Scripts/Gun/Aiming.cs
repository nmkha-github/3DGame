using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public GameObject gun;
    public GameObject crosshair;
    public Camera cam;
    public Vector3 normalPosition, aimPosition;
    public Vector3 aimRotation;
    public float rotationSmooth;
    public float aimSpeed;

    private WeaponSway sway;
    void Start()
    {
        sway = gun.GetComponent<WeaponSway>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            gun.transform.localRotation = Quaternion.identity;
            crosshair.SetActive(false);
            sway.enabled = false;
            transform.localPosition = Vector3.Slerp(transform.localPosition, aimPosition, aimSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(aimRotation), rotationSmooth * Time.deltaTime);
            cam.fieldOfView -= 40 * Time.deltaTime;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 30, 60);
        }
        else
        {
            crosshair.SetActive(true);
            sway.enabled = true;
            transform.localPosition = Vector3.Slerp(transform.localPosition, normalPosition, aimSpeed * Time.deltaTime);
            cam.fieldOfView += 40 * Time.deltaTime;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 30, 60);
        }
    }
}
