using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;
    [SerializeField] private float moveSwayMultiplier; // New variable for movement-based sway

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Input for mouse movement
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        // Input for player movement
        float moveX = Input.GetAxisRaw("Horizontal") * moveSwayMultiplier;
        float moveY = Input.GetAxisRaw("Vertical") * moveSwayMultiplier;

        // Calculate rotations for mouse and movement sway
        Quaternion rotationX = Quaternion.AngleAxis(-mouseX + moveY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseY - moveX, Vector3.up);

        // Combine rotations
        Quaternion targetRotation = rotationX * rotationY;

        // Apply smooth rotation to the weapon
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
