using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Apple : MonoBehaviour {
    // Below visible screen.
    public float bottomY = -20f;
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
}
