using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float health = 250f;

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
        else
        {
            Debug.Log("Hit by a unknown");
        }
    }
}