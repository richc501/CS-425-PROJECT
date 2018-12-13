using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void loadlevel(string level)
    {
        PlayerPrefs.SetInt("difficulty", 0);
        SceneManager.LoadScene(level);

    }

    public void quitGame() {
        Application.Quit();
    }
}
