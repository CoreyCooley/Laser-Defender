using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelector : MonoBehaviour {


    public Sprite[] ShipSprites;   // Store Ship Sprites

    public Image ShipImage;   // Image on the Ship
    public static Sprite ShipSprite;

    private int selection; // Current Ship Sprite

    // Use this for initialization
    void Start () {
        selection = 0;
        ShipImage.sprite = ShipSprites[selection];
    }
	
	// Update is called once per frame
	void Update () {
        ShipSprite = ShipSprites[selection];

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            PreviousShip();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            NextShip();
    }

    public void NextShip()
    {
        if (selection < ShipSprites.Length - 1)
            selection++;
        else
            selection = 0;

        ShipImage.sprite = ShipSprites[selection];
    }

    public void PreviousShip()
    {
        if (selection > 0)
            selection--;
        else
            selection = ShipSprites.Length - 1;

        ShipImage.sprite = ShipSprites[selection];
    }
}
