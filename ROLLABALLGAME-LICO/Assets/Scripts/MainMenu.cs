using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Levels");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
