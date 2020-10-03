using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Apple : MonoBehaviour {
    // Below visible screen.
    public float bottomY = -20f;

    // This is the list of drop speeds for the apple.

    // Start is called before the first frame update.
    void Start() {
        
    }
    // Update is called once per frame
    void Update() {
        // If offscreen, delete apple.
        if (transform.position.y < bottomY) {
            Destroy(this.gameObject);
            // Get a reference to the Apple Picker component of the Main Camera.
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            // Call the public AppleDestroyed() method of apScript
            apScript.AppleDestroyed();

        }
    }

    public static void SetDifficulty(int score, List<float> dropSpeedList, Rigidbody rigidBody, Renderer renderer) {
        switch (score) {
            case var expression when score > 2500:
                rigidBody.AddForce(rigidBody.transform.up * dropSpeedList[2]);
                renderer.material.color = Color.green;
                break;
            case var expression when score > 1000:
                rigidBody.AddForce(rigidBody.transform.up * dropSpeedList[1]);
                renderer.material.color = Color.yellow;
                break;
        }
    }
}
