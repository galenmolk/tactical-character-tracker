using System;
using System.Collections;
using UnityEngine;

public class JumpPowerUpExample : MonoBehaviour
{
    private int jumpForce = 20;

    private bool isPowerUpActive = false;

    private float powerUpStartTime;
    
    private void Update()
    {
        if (isPowerUpActive)
            return;

        powerUpStartTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
