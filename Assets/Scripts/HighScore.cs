using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScore : MonoBehaviour {
    static public int score = 1000;
    private void Awake() {
        if (PlayerPrefs.HasKey("HighScore")) // If the PlayerPrefs HighScore already exists, read it.
            score = PlayerPrefs.GetInt("HighScore");
        PlayerPrefs.SetInt("HighScore", score); // Assign the high score to HighScore.

    }
    void Update() {
        Text gt = this.GetComponent<Text>(); // Get text component from HighScore component on the canvas.
        gt.text = "High Score: " + score; // Set the above to the current high score found in memory.

        if (score > PlayerPrefs.GetInt("HighScore")) // Check if the current score is higher than the HighScore, update it.
            PlayerPrefs.SetInt("HighScore", score);
    }
}
