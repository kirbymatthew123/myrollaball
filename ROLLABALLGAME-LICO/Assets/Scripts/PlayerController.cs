using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed = 10f;
    private Rigidbody rb;
    private int count;

    [Header("Camera Settings")]
    public Transform cam;

    [Header("UI References")]
    public TMP_Text countText; 
    public TMP_Text winText; 

    [Header("Audio")]
    public AudioSource bgMusic;   
    public AudioSource winSound; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        if (countText != null)
            countText.text = "Coins: 0";

        if (winText != null)
            winText.text = "";

        if (bgMusic != null)
        {
            bgMusic.loop = true;
            bgMusic.Play();
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = cam != null ? cam.forward : Vector3.forward;
        Vector3 right = cam != null ? cam.right : Vector3.right;

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
        if (countText != null)
            countText.text = "Coins: " + count.ToString();

        if (count >= 38)  
        {
            if (winText != null)
                winText.text = "CONGRATS YOU FINISH THE GAME!! :3";

            if (bgMusic != null)
                bgMusic.Stop();

            if (winSound != null)
                winSound.Play();
        }
    }
}
