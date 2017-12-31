using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {

    public GameObject enemyPrefab;
    public float speed = 5f;
    public float flySpeed = 10f;
    public float width = 10f;
    public float height = 5f;
    public float xPadding = 0.5f;
    public float yPadding = 0.5f;

    // Private Variables
    private bool isMovingRight = true;
    private bool isMovingDown = false;
    private float xMin = -5.0f;
    private float xMax = 5.0f;
    private float yMin = -4.5f;
    private float yMax = -2.0f;

    // Use this for initialization
    void Start () {

        // Spawn Enemies in formation
        SpawnEnemies();

        // Distance between formation and camera
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));    // Left Corner
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));   // Right Corner
        xMin = leftBoundary.x + xPadding;
        xMax = rightBoundary.x - xPadding;
        yMin = leftBoundary.y + yPadding;
        yMax = rightBoundary.y - yPadding;

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width,height,0));
    }

    // Update is called once per frame
    void Update () {
        // Formation moving right
        if (isMovingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }      
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if(isMovingDown)
        {
            transform.position += Vector3.down * flySpeed * Time.deltaTime;
            isMovingDown = false;
        }


        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        if (leftEdgeOfFormation < xMin)
        {
            isMovingRight = true;
            isMovingDown = true;
        }
        else if(rightEdgeOfFormation > xMax)
        {
            isMovingRight = false;
            isMovingDown = true;
        }
        
        // Restrict the formation to the game space
        float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Check if formation is empty
        if(AllMembersDead())
        {
            Debug.Log("Emtpy Formation");
            SpawnEnemies();
        }
    }

    private bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }

        return true;
    }

    private void SpawnEnemies()
    {
        // Spawn each enemy
        foreach (Transform child in transform)
        {
            // Quaternion is rotation
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(child.transform.position.x,4,0), Quaternion.identity) as GameObject;
            // Set Enemy Spawner as the parent
            enemy.transform.parent = child;
        }
    }
}
