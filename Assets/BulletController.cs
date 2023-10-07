using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Create a new instance of the projectile
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Get the mouse position in screen space
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // Set the z-coordinate to a value that works for your scene

            // Convert the mouse position to world space
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Calculate the direction from the current position to the mouse position
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Access the rigidbody of the new projectile and add force to shoot it
            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
            rb.AddForce(direction * projectileSpeed, ForceMode.VelocityChange);

            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                string hitTag = hit.transform.tag;

                if(hitTag == "Sign")
                {
                    Debug.Log("Hit");
                }
            }

            Debug.DrawRay(transform.position, direction * 1000f, Color.red);
        }
    }
}