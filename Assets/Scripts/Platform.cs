using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("COLLISION !!!!");
            var rB = other.gameObject.GetComponent<Rigidbody2D>();
            rB.velocity = new Vector2(0, 400 * Time.deltaTime);
        }
    }

}