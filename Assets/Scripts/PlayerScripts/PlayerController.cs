using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float slidingFactor = 0.1f;
    private Vector2 mousePosition;
    public Camera sceneCam;
    private Vector2 movDir;
    private Rigidbody2D rb;
    public ProjectileShooter squirter;
    public BiteScript biting;

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

        if(Input.GetMouseButtonDown(1)) {
            biting.Bite();
        }

        movDir = new Vector2(x, y).normalized;

        mousePosition = sceneCam.ScreenToWorldPoint(Input.mousePosition);
    }

    void Move() {
        Vector2 targetVelocity = movDir * speed;

        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, slidingFactor);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}
