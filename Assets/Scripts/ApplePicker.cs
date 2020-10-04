using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ApplePicker : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f, basketSpacingY = 2f;
    public List<GameObject> basketList;

    private static List<Color> colourList = new List<Color> { Color.red, Color.yellow, Color.green };
    enum BasketColors { Red, Yellow, Green }
    List<BasketColors> basketColors;


    void Start() {        
        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++) {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY*i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }

        InitializeColours(basketList);
    }
    void Update() {
        
    }
    public void AppleDestroyed() {
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tGO in tAppleArray) // Destroy all of the falling apples.
            Destroy(tGO);
        int basketIndex = basketList.Count - 1; // Get the index of the last Basket in basketList, destroy.
        GameObject tBasketGO = basketList[basketIndex]; // Get a reference to that Basket GameObject
        basketList.RemoveAt(basketIndex); // Remove the Basket from the list.
        Destroy(tBasketGO); // Destroy GameObject.
        if (basketList.Count == 0) // If there are no Baskets left, restart the game.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void InitializeColours(List<GameObject> basketList) {
        foreach (GameObject basket in basketList)
            foreach (Color colour in colourList)
                if (colour != basket.GetComponent<Renderer>().material.color)
                    if (basketList.IndexOf(basket) == 2)
                        foreach (GameObject _basket in basketList)
                            _basket.GetComponent<Renderer>().material.color = colourList[basketList.IndexOf(_basket)];
                    else
                        continue;
                else
                    continue;
    }
}
