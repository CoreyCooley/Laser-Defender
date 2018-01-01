using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public int damage = 1;

    public void Hit()
    {
        Destroy(gameObject);
    }

    public int GetDamage()
    {
        return damage;
    }
}
