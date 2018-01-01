using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float health = 250f;
    public GameObject laserPrefab;
    public float laserSpeed = 5f;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 25;
    public int impactDamage = 150;
    public AudioClip fireSound;
    public AudioClip deathSound;

    private ScoreKeeper scoreKeeper;


    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();
        if (laser)
        {
            health -= laser.GetDamage();
            laser.Hit();
            if(health <= 0)
            {
                Die();
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
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -laserSpeed, 0);

        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    public void Impact()
    {
        Destroy(gameObject);
    }

    public int GetImpactDamage()
    {
        return impactDamage;
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
        scoreKeeper.Score(scoreValue);
    }
}