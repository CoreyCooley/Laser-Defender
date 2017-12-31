using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

    private void OnDrawGizmos()
    {
        // Gizmos are attached to objects in the edit view of the game
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
