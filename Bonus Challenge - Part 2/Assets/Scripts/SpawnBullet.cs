using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float magazineSize = 13;

   private float lastTime;
    
    void Update()
    {
        if (Time.time - lastTime > fireRate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (magazineSize != 0)
                {
                    // Spawn Bullet with Object Pool Pattern
                    ObjectPoolSpawn();
                    magazineSize--;
                    Debug.Log("Bullets left: " + magazineSize);
                }
                else
                {
                    Debug.Log("Magazine Empty");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && magazineSize == 0)
        {
            magazineSize = 13;
            Debug.Log("Gun Reloaded");
            Debug.Log("Bullets left: 13");
        }
    }

    private void ObjectPoolSpawn()
    {
        lastTime = Time.time;

        Vector3 position = transform.position;

        var bullet = BasicPool.Instance.GetFromPool();
        bullet.transform.position = position;

        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        rigidbody.velocity = bullet.transform.up * bulletSpeed;
    }
}
