using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;

    float velocityX = 0f;
    float velocityY = 0f;
    void Update() {
        velocityX = Input.GetAxisRaw("Horizontal");
        velocityY = Input.GetAxisRaw("Vertical");
        if (velocityY < 0f) {
            velocityY = 0f;
        }
    }

    private void FixedUpdate() {
        transform.Translate(Vector2.up * velocityY * moveSpeed * Time.deltaTime, Space.Self);

        transform.Rotate(Vector3.forward * -velocityX * turnSpeed * Time.deltaTime);
    }
}
