using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController2 : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;
    private int count;
    public Transform cam;

    public TMP_Text countText;
    public TMP_Text winText;
    public TMP_Text timerText;

    public AudioSource bgMusic;
    public AudioSource winSound;
    public AudioSource loseMusic;

    private float timeRemaining = 60f;
    private bool timerIsRunning = true;
    private bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";

        if (bgMusic != null)
        {
            bgMusic.loop = true;
            bgMusic.Play();
        }

        if (loseMusic != null)
        {
            loseMusic.Stop();
        }
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                GameOver();
            }
        }
    }
    void UpdateLoseMusic()
    {
        if (isGameOver && loseMusic != null && !loseMusic.isPlaying)
        {
            loseMusic.Play();
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * moveZ + right * moveX).normalized;

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUps"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Coins: " + count.ToString();

        if (count >= 18)
        {
            winText.text = "CONGRATS YOU FINISH THE GAME!! :3";

            if (bgMusic != null) bgMusic.Stop();
            if (winSound != null) winSound.Play();

            timerIsRunning = false;

            Invoke("LoadLevelsScene", 5f);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        winText.text = "TIME'S UP! GAME OVER!";
        rb.isKinematic = true;

        if (bgMusic != null) bgMusic.Stop();

        isGameOver = true;
        UpdateLoseMusic();

        Invoke("LoadLevelsScene", 5f);
    }

    void LoadLevelsScene()
    {
        SceneManager.LoadScene("Levels");
    }
}
