using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirt : MonoBehaviour
{
    public Rigidbody2D rb;
    void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject, 0.1f);
    }
}
