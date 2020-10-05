using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ApplePicker : MonoBehaviour {
    private static List<Color> colourList = new List<Color> { Color.red, Color.yellow, Color.green };
    private static bool extraHard = false;
    public bool ExtraHard { get => extraHard; set => extraHard = value; }
    public int NumBaskets { get => numBaskets; set => numBaskets = value; }
    public List<GameObject> BasketList { get => basketList; set => basketList = value; }
    // [Header("Set in Inspector")] No. 
    public GameObject basketPrefab;
    private int numBaskets = 3;
    private static float basketBottomY = -14f, basketSpacingY = 2f;
    private static List<GameObject> basketList;

    private void Start() {        
        BasketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++) {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY*i);
            tBasketGO.transform.position = pos;
            BasketList.Add(tBasketGO);
        }
    }
    private void Update() {
        if (extraHard && BasketList.Count != 0)
                if (!colourList.Contains(BasketList[0].GetComponent<Renderer>().material.color))
                    StartExtraHardMode();
    }
    private void StartExtraHardMode() {
        InitializeColours(BasketList);
        Invoke(nameof(RotateColours), 5f);
    }
    public void AppleDestroyed() {
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tGO in tAppleArray) // Destroy all of the falling apples.
            Destroy(tGO);
        BasketDestroyed();
    }

    private void DestroyApples() {
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tGO in tAppleArray) // Destroy all of the falling apples.
            Destroy(tGO);
    }

    public void BasketDestroyed() {
        int basketIndex = BasketList.Count - 1; // Get the index of the last Basket in basketList, destroy.
        GameObject tBasketGO = BasketList[basketIndex]; // Get a reference to that Basket GameObject
        BasketList.RemoveAt(basketIndex); // Remove the Basket from the list.

        Destroy(tBasketGO); // Destroy GameObject.
        if (BasketList.Count == 0)
        { // If there are no Baskets left, restart the game.
            Basket.scoreGT.text = "0";
            extraHard = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private void InitializeColours(List<GameObject> basketList) {
        DestroyApples();
            foreach (Color colour in colourList)
                if (colourList.IndexOf(colour) < basketList.Count)
                    basketList[colourList.IndexOf(colour)].GetComponent<Renderer>().material.color = colour;
    }
    private void RotateColours() {
        DestroyApples();
        Color basketColour;
        foreach (GameObject basket in BasketList) {
            basketColour = basket.GetComponent<Renderer>().material.color;
            basketColour = colourList[(colourList.IndexOf(basketColour) + 1 == 3) ? 0 : colourList.IndexOf(basketColour) + 1];
            basket.GetComponent<Renderer>().material.color = basketColour;
        }
        DestroyApples();
        Invoke(nameof(RotateColours), 10f);
    }
}
