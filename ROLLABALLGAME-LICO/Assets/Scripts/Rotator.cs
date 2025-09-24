using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 180f; 
    private float currentRotation = 0f;

    void Update()
    {
        float rotationStep = speed * Time.deltaTime;
        currentRotation += rotationStep;

        if (currentRotation >= 360f)
        {
            currentRotation -= 360f; 
        }

        transform.rotation = Quaternion.Euler(0, currentRotation, 0);
    }
}