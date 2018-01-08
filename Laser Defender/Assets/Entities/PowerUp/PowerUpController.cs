using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

    public AudioClip powerUpSound;
    public GameObject powerUpLaser;

    public float powerUpFiringRate = 1.0f;
    public int   powerUpHealth     = 0;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            Impact();
            BuffPlayer(player);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuffPlayer(PlayerController player)
    {
        if (powerUpLaser)
        {
            player.laserType = powerUpLaser;
        }

        if (player.firingRate > 0.05f)
        {
            player.firingRate = player.firingRate / powerUpFiringRate;
        }
        else
        {
            player.firingRate = 0.05f;
        }
        if (player.currentHealth < 5)
        {
            player.currentHealth += powerUpHealth;
        }       
    }

    public void Impact()
    {
        AudioSource.PlayClipAtPoint(powerUpSound, transform.position);
        Destroy(gameObject);
    }
}
