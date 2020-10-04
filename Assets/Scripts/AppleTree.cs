using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
public class AppleTree : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject applePrefab;
    // Apple from ApplePrefab, Speed at which the AppleTree moves, distance where AppleTree turns around, chance that the AppleTree will change directions,
    // rate at which Apples will be instantiated, list of apple drop speeds.
    public float speed, leftAndRightEdge = 10f, chanceToChangeDirections = 0.02f, secondsBetweenAppleDrops = 1f;
    public List<float> dropSpeedList = new List<float> { -1f, -1000f, -2500f };

    public GameObject ApplePrefab {
        get { return applePrefab; }
        set { applePrefab = value; }
    }

    void Start() {
        Invoke(nameof(DropApple), 2f);  // Dropping apples every second.
    }
    void DropApple() {
        GameObject ApplePrefab = Instantiate<GameObject>(applePrefab);
        ApplePrefab.transform.position = transform.position;
        Apple.SetDifficulty(int.Parse(Basket.ScoreGT.text), dropSpeedList, ApplePrefab.GetComponent<Rigidbody>(), ApplePrefab.GetComponent<Renderer>());
        Invoke(nameof(DropApple), secondsBetweenAppleDrops);
    }

    void Update() {
        // Basic movement.
        Vector3 pos = transform.position; 
        pos.x += speed * Time.deltaTime;
        transform.position = pos; // Changing direction.
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
