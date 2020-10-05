using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ApplePicker : MonoBehaviour {
    private static bool extraHard = false;
    public bool ExtraHard {
        get { return extraHard; }
        set { extraHard = value; }
    }

    [Header("Set in Inspector")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f, basketSpacingY = 2f;
    public List<GameObject> basketList;
    private static List<Color> colourList = new List<Color> { Color.red, Color.yellow, Color.green };
    void Start() {        
        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++) {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY*i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }
    void Update() {
        if (extraHard && basketList.Count != 0)
                if (!colourList.Contains(basketList[0].GetComponent<Renderer>().material.color))
                    StartExtraHardMode();
    }
    void StartExtraHardMode() {
        InitializeColours(basketList);
        Invoke(nameof(RotateColours), 5f);
    }
    public void AppleDestroyed() {
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tGO in tAppleArray) // Destroy all of the falling apples.
            Destroy(tGO);
        BasketDestroyed();
    }

    public void DestroyApples() {
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tGO in tAppleArray) // Destroy all of the falling apples.
            Destroy(tGO);
    }

    public void BasketDestroyed() {
        int basketIndex = basketList.Count - 1; // Get the index of the last Basket in basketList, destroy.
        GameObject tBasketGO = basketList[basketIndex]; // Get a reference to that Basket GameObject
        basketList.RemoveAt(basketIndex); // Remove the Basket from the list.

        Destroy(tBasketGO); // Destroy GameObject.
        if (basketList.Count == 0)
        { // If there are no Baskets left, restart the game.
            Basket.scoreGT.text = "0";
            extraHard = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void InitializeColours(List<GameObject> basketList) {
        DestroyApples();
            foreach (Color colour in colourList)
                if (colourList.IndexOf(colour) < basketList.Count)
                    basketList[colourList.IndexOf(colour)].GetComponent<Renderer>().material.color = colour;
    }
    void RotateColours() {
        DestroyApples();
        Color basketColour;
        foreach (GameObject basket in basketList) {
            basketColour = basket.GetComponent<Renderer>().material.color;
            basketColour = colourList[(colourList.IndexOf(basketColour) + 1 == 3) ? 0 : colourList.IndexOf(basketColour) + 1];
            basket.GetComponent<Renderer>().material.color = basketColour;
        }
        Invoke(nameof(RotateColours), 5f);
    }
}
