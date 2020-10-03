using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEditor.UIElements;
using UnityEngine;
public class AppleTree : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject applePrefab;
    // Speed at which the AppleTree moves, distance where AppleTree turns around, chance that the AppleTree will change directions,
    // rate at which Apples will be instantiated.
    public float speed, leftAndRightEdge = 10f, chanceToChangeDirections = 0.02f, secondsBetweenAppleDrops = 1f;
    // This is the list of drop speeds for the apple.
    public List<float> dropSpeedList = new List<float>{ -1f, -1000f, -10000f };

    void Start() {
        Invoke(nameof(DropApple), 1f);  // Dropping apples every second.
    }
    void DropApple() {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        Rigidbody rb = apple.GetComponent<Rigidbody>();
        apple.transform.position = transform.position;

        // Three difficulties depending your score [ 0 <= 1000 <= 10000 ].
        if (int.Parse(Basket.ScoreGT.text) >= 1000)
            rb.AddForce(transform.up * ((int.Parse(Basket.ScoreGT.text) < 10000) ? dropSpeedList[1] : dropSpeedList[2]));

        Invoke(nameof(DropApple), secondsBetweenAppleDrops);
    }
    void Update() {
        // Basic movement.
        Vector3 pos = transform.position; 
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        // Changing direction.
        if (pos.x < -leftAndRightEdge) // Hit left edge.
            speed = Mathf.Abs(speed); // Move right.
        else if (pos.x > leftAndRightEdge) // Hit right edge.
            speed = -Mathf.Abs(speed); // Move left.
    }
    private void FixedUpdate() {
        // Roll 0 - 1.00. Hit [0 - 0.10). Reverse direction.
        if (Random.value < chanceToChangeDirections)  // Average 1 time per second at 0.02 chance.
            speed *= -1;
    }
}
