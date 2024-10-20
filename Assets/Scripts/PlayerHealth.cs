using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            Debug.Log("worm hurt");
            // take damage stuff 
            Destroy(gameObject, 0.1f);
        }
    }
}
