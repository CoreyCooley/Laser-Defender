using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Public Variables
    public GameObject laserPrefab;
    public float speed;
    public float laserSpeed = 5f;
    public float firingRate = 0.2f;
    public float xPadding = 0.5f;
    public float yPadding = 0.5f;
    public int currentHealth;
    public int maxHealth = 5;
    public AudioClip fireSound;
    public AudioClip deathSound;
    public AudioClip hitSound;

    // Private Variables
    private float xMin = -5.0f;
    private float xMax = 5.0f;
    private float yMin = -4.5f;
    private float yMax = -2.0f;
    private const string FIRE_METHOD = "Fire";
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        speed = 10.0f;
        currentHealth = maxHealth;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ShipSelector.ShipSprite;

        // Distance between player and camera
        float distance = transform.position.z - Camera.main.transform.position.z;
        // Vector 3 has values between 0 and 1 relative to screen
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));    // Left Corner
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, .4f, distance)); // Right Corner
        xMin = leftMost.x + xPadding;
        xMax = rightMost.x - xPadding;
        yMin = leftMost.y + yPadding;
        yMax = rightMost.y - yPadding;
    }
	
	// Update is called once per frame
	void Update () {
        // Move Player
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //transform.position += new Vector3(-speed * Time.deltaTime, 0,0); // Old Way
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating(FIRE_METHOD, 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke(FIRE_METHOD);
        }

        // Restrict the player to the game space
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser laser = collision.gameObject.GetComponent<Laser>();
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if (laser)
        {
            currentHealth -= laser.GetDamage();
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
            laser.Hit();
        }
        else if (enemy)
        {
            currentHealth -= enemy.GetImpactDamage();
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
            enemy.Impact();
        }
        else
        {
            Debug.Log("Hit by a unknown");
        }        

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Fire()
    {        
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);

        AudioSource.PlayClipAtPoint(fireSound, transform.position);        
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);

        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        levelManager.LoadLevel("Game Over");
    }
}
