using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This line enables use of uGUI features.
public class Basket : MonoBehaviour {
    private static List<Color> colourList = new List<Color> { Color.red, Color.yellow, Color.green };
    [Header("Set Dynamically")]
    public static Text scoreGT;

    public static Text ScoreGT {
        get { return scoreGT; }
        set { scoreGT = value; }
    }

    void Start() {
        // Find a reference to the ScoreCounter GameObject.
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        // Get the Text Component of that GameObject.
        scoreGT = scoreGO.GetComponent<Text>();
        // Set the starting number of points to 0.
        scoreGT.text = "0";
    }

    void SwitchColours(List<Color> colourList) {

    }

    void Update() {
        // Get the current screen position of the mouse from Input.
        Vector3 mousePos2D = Input.mousePosition;
        // The Camera's z position sets how far to push the mouse into 3D.
        mousePos2D.z = -Camera.main.transform.position.z;
        // Convert the point from 2D screen space into 3D game world space.
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        // Move the x position of this Basket to the x position of the Mouse.
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }
    private void OnCollisionEnter(Collision collision) {
        // Find out what hit this basket.
        GameObject collidedWith = collision.gameObject;

        if (collidedWith.tag == "Apple") {
            Destroy(collidedWith);
            int score = int.Parse(scoreGT.text); // Parse the text of the scoreGT into an int.
            Color color = collidedWith.GetComponent<Renderer>().material.color; // Color of the colliding apple.
            score += (color == Color.green) ? 300 : (color == Color.yellow) ? 200 : 100; // 300 points for red, 200 points for yellow, 100 points for red (default).
            // Convert the score back to a string and display it.
            scoreGT.text = score.ToString();
            // Track the high score.
            if (score > HighScore.score)
                HighScore.score = score;
        }
    }
}
