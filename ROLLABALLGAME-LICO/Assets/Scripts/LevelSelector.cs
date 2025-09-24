using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
