using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpFormationController : MonoBehaviour {

    public GameObject[] powerupPrefabs;
    public float speed = 5f;
    public float flySpeed = 10f;
    public float width = 10f;
    public float height = 5f;
    public float xPadding = 0.5f;
    public float yPadding = 0.5f;
    public float spawnDelay = 30.0000001f;

    // Use this for initialization
    void Start()
    {
        // Spawn Power Up in formation
        InvokeRepeating("SpawnPowerUp", 5.00001f, spawnDelay);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnPowerUp()
    {
        int randPostition = Random.Range(0, transform.childCount);
        int randPowerUp = Random.Range(0, powerupPrefabs.Length);

        Transform freePosition = transform.GetChild(randPostition);
        GameObject powerUp = powerupPrefabs[randPowerUp];

        if (freePosition)
        {
            GameObject enemy = Instantiate(powerUp, new Vector3(freePosition.position.x, 10, 0), Quaternion.identity) as GameObject;
            // Set Enemy Spawner as the parent
            enemy.transform.parent = freePosition;
        }
    }
}
