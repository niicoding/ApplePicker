using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Apple : MonoBehaviour {
    private static float bottomY = -20f; // Below visible screen.
    private static List<float> dropSpeedList = new List<float> { -1f, -1f, -1f };
    private GameObject apple;
    private Color appleColor;
    private ApplePicker apScript;

    private void Start() {
        apple = this.gameObject;
        appleColor = GetComponent<Renderer>().material.color;   
        apScript = Camera.main.GetComponent<ApplePicker>();
        SetDifficulty(GetComponent<Rigidbody>(), GetComponent<Renderer>(), int.Parse(Basket.ScoreGT.text));
    }
    private void Update() {
        if (apScript.basketList.Count != 0) {                     
            Color basketColor = apScript.basketList[apScript.basketList.Count - 1].GetComponent<Renderer>().material.color;
            if (transform.position.y < bottomY && !apScript.ExtraHard) {  // If offscreen, delete apple.
                Destroy(apple);
                apScript.AppleDestroyed(); // Get a reference to the Apple Picker component of the Main Camera. Call the public AppleDestroyed() method of apScript
            } else if (transform.position.y < bottomY && apScript.ExtraHard) {
                Destroy(apple);
                if (appleColor == basketColor) apScript.AppleDestroyed(); // Get a reference to the Apple Picker component of the Main Camera. Call the public AppleDestroyed() method of apScript
            }
        }
    }
    private void SetDifficulty(Rigidbody rigidBody, Renderer renderer, int scoreNumeric) {
        switch (scoreNumeric) { // Easy difficulty is default. No changes are necessary -- apples are red and travel drop at -1f speed.
            case var expression when (Random.Range(0, 2)) * scoreNumeric >= 1000: // 50% chance of executing when score >= 1000. Medium difficulty.
                rigidBody.AddForce(rigidBody.transform.up * dropSpeedList[1]); // Change apple drop speed to -100f.
                renderer.material.color = Color.yellow; // Change apple color to yellow.
                break;
            case var expression when (Random.Range(0, 2)) * scoreNumeric > 2500:  // 25% chance of executing when score >= 2000. Hard difficulty
                rigidBody.AddForce(rigidBody.transform.up * dropSpeedList[2]); // Change apple drop speed to -200f.
                renderer.material.color = Color.green; // Change apple color to green.
                break;
        }
    }
}
