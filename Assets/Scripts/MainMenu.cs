using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("LevelSelector");
    }

    public void BuildGame()
    {
        SceneManager.LoadScene("Editor");
    }
    public void QuitGame() {
        Application.Quit();
    }

    public void MenuReturn() {
        SceneManager.LoadScene("Menu");
    }
}
