using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float health = 250f;
    public GameObject laserPrefab;
    public float laserSpeed = 5f;
    public float shotsPerSecond = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();
        if (laser)
        {
            health -= laser.GetDamage();
            laser.Hit();
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if(Random.value < probability)
        {
            Fire();
        }        
    }

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject laser = Instantiate(laserPrefab, startPosition, Quaternion.identity) as GameObject;

        laser.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -laserSpeed, 0);
    }
}