using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    private Text scoreText;

    // Use this for initialization
    void Start () {
        scoreText = GetComponent<Text>();
        Reset();
	}

    public void Score(int points)
    {
        score += points;
        scoreText.text = score.ToString("D6");
    }

    public static void Reset()
    {
        score = 0;        
    }
}
