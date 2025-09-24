using UnityEngine;

public class LoseSoundManager : MonoBehaviour
{
    public static LoseSoundManager instance;
    public AudioSource loseMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (loseMusic != null)
        {
            loseMusic.Stop();
        }
    }

    public void PlayLoseSound()
    {
        if (loseMusic != null && !loseMusic.isPlaying)
        {
            loseMusic.Play();
        }
    }
}
