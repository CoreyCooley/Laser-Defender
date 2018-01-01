using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

    public Sprite[] HeartSprites;   // Store Heart Sprites

    public Image HeartUI;   // Image on the UI

    private PlayerController player;   // Used to access health of the player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); //Get player from game
    }

    void Update()
    {
        Debug.Log("Current Health: " + player.currentHealth);

        if (player.currentHealth > player.maxHealth)
            HeartUI.sprite = HeartSprites[player.maxHealth];
        else if (player.currentHealth < 0)
            HeartUI.sprite = HeartSprites[0];
        else
            HeartUI.sprite = HeartSprites[player.currentHealth];
    }
}
