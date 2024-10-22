using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float speed;

    void Update() {
        // temporary simple movement for just a worm head
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        Vector3 movement = new Vector3(x, y, 0);

        if (movement.magnitude > 1) {
            movement.Normalize();
        }

        transform.position += movement * speed * Time.deltaTime;
    }
}