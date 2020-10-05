using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Apple : MonoBehaviour {
    public float bottomY = -20f; // Below visible screen.
    public List<float> dropSpeedList = new List<float> { -1f, -1f, -1f };
    public void Start() {
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        SetDifficulty(this.gameObject.GetComponent<Rigidbody>(), this.gameObject.GetComponent<Renderer>(), int.Parse(Basket.ScoreGT.text));
    }
    void Update() {
        ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
        if (apScript.basketList.Count != 0) {
            Color appleColor = this.gameObject.GetComponent<Renderer>().material.color;
            Color basketColor = apScript.basketList[apScript.basketList.Count - 1].GetComponent<Renderer>().material.color;
            // If offscreen, delete apple.
            if (transform.position.y < bottomY && !ApplePicker.extraHard) {
                Destroy(this.gameObject);
                // Get a reference to the Apple Picker component of the Main Camera.
                // Call the public AppleDestroyed() method of apScript
                apScript.AppleDestroyed();
            } else if (transform.position.y < bottomY && ApplePicker.extraHard) {
                Destroy(this.gameObject);
                // Get a reference to the Apple Picker component of the Main Camera.
                // Call the public AppleDestroyed() method of apScript
                if (appleColor == basketColor)
                    apScript.AppleDestroyed();
            }
        }
    }
    public void SetDifficulty(Rigidbody rigidBody, Renderer renderer, int scoreNumeric) {
        // Easy difficulty is default. No changes are necessary -- apples are red and travel drop at -1f speed.
        switch (scoreNumeric) {
            // 50% chance of executing when score >= 1000. Medium difficulty.
            case var expression when (Random.Range(0, 2)) * scoreNumeric >= 1000:
                // Change apple drop speed to -100f.
                rigidBody.AddForce(rigidBody.transform.up * dropSpeedList[1]);
                // Change apple color to yellow.
                renderer.material.color = Color.yellow;
                break;
            // 25% chance of executing when score >= 2000. Hard difficulty.
            case var expression when (Random.Range(0, 2)) * scoreNumeric > 2500:
                // Change apple drop speed to -200f.
                rigidBody.AddForce(rigidBody.transform.up * dropSpeedList[2]);
                // Change apple color to green.
                renderer.material.color = Color.green;
                break;
        }
    }
}
