using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool takeDamagePlayer = true;
    public bool takeDamageEnemy = true;
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;
        if (hitTransform.CompareTag("Player"))
        {
            if (takeDamagePlayer)
            {
                PlayerHealth playerHealth = hitTransform.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                    playerHealth.TakeDamage(10);
            }
        }

        else if (hitTransform.CompareTag("Enemy"))
        {
            if (takeDamageEnemy)
            {
                Enemy enemy = hitTransform.GetComponent<Enemy>();
                if (enemy != null)
                    enemy.TakeDamage(10);
            }
        }

        else
        {
            CreateBulletImpactEffect(collision);
        }

        Destroy(gameObject);
    }

    private void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(Resources.Load("Prefabs/BulletImpact") as GameObject, contact.point, Quaternion.LookRotation(contact.normal));

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
