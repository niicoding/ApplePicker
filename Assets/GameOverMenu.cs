using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {
    // Start is called before the first frame update
    public void Start() {
        
    }
    public void SwitchMainMenu() {
        SceneManager.LoadScene("Scenes/Menu"); // Load Main Menu.
    }
}
