using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private IEnumerator Start()
    {
        while (true)
        {
            float newX = GetComponent<Rigidbody2D>().position.x - 0.1f;
            yield return new WaitForEndOfFrame();
            GetComponent<Rigidbody2D>().position = new Vector3(newX, GetComponent<Rigidbody2D>().position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        //other.gameObject.GetComponent<DestructibleTilemap>().Damage(other);
    }
}
