using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float speed;
    private Vector2 mousePosition;
    public Camera sceneCam;
    private Vector2 movDir;
    private Rigidbody2D rb;
    public ProjectileShooter squirter;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        ProcessInputs();
    }

    void FixedUpdate() {
        Move();
    }

    void ProcessInputs() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButtonDown(0)) {
            squirter.Fire();
        }

        movDir = new Vector2(x, y).normalized;

        mousePosition = sceneCam.ScreenToWorldPoint(Input.mousePosition);
    }

    void Move() {
        rb.velocity = movDir * speed;

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}
