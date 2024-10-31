using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float speed; 
    [SerializeField] private Animator animator;
    [SerializeField] private float crawlingIdleTime; // how long he stands per "crawl" movement
    [SerializeField] private float transitionLength; // how long each transition is
    [SerializeField] private float  dashStrength; // strength of dash in terms of the force 
    [SerializeField] private Transform movepoint; // movepoint for dash 
    private Vector3 dashTargetPosition;
    private float crawlingTimer = 0f; // timer for the little crawl animation
    private float transitionTimer = 0f;
    private bool moving = true; // this is the transition of whether or not he is moving at all 
    private bool crawling = false; // this is just to alternate between moving and not moving for choppy movement 
    private bool tired = false;
    private bool attack = false;
    private Quaternion originalRotation; // original rotation

    void Start() {
        originalRotation = transform.rotation;
    }
    IEnumerator Dash() {
        // Capture the target's position once at the beginning of the dash
        dashTargetPosition = target.position;  

        StartCoroutine(RotateTowardsTarget());

        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Use the static dashTargetPosition to dash toward
        rb.AddForce((dashTargetPosition - transform.position).normalized * dashStrength, ForceMode2D.Impulse);

        // End phase quickly so it doesn't keep hugging the player
        yield return new WaitForSeconds(1f);
        attack = false;
        tired = true;
        StartCoroutine(RotateBackToOriginal());
        yield break;
    }

    IEnumerator RotateTowardsTarget() {
        // direction to the target
        Vector2 targetDirection = (target.position - transform.position).normalized;
        
        // angle to go to
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        // current angle
        float currentAngle = transform.eulerAngles.z;

        float rotationSpeed = 100f;

        while (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) > 0.1f) {
            // interpolate
            currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
            
            // apply the rotation
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            // wait for the next frame
            yield return null;
        }

        transform.right = targetDirection;
    }

    IEnumerator RotateBackToOriginal() {
        float rotationSpeed = 100f; 
        float duration = 1f; //  time to rotate back
        float elapsed = 0f; // timer

        // rotate back to the original position
        while (elapsed < duration) {
            // interpolation thing
            float t = elapsed / duration;

            // interpolate between the current rotation and the original rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, t);

            elapsed += Time.deltaTime;

            // wait for the next frame
            yield return null;
        }

        transform.rotation = originalRotation;
    }



    // this script just makes it so every "toggleInterval" seconds the boss moves slightly towards 
    // the player, for a "crawling" animation
    void Update() {
        Debug.Log(transitionTimer);
        Debug.Log("Moving " + moving + "\tTired: " + tired + "\tAttacking: " + attack);

        animator.SetBool("Phase1Crawl", crawling); // the non-crawling stand and "idle / tired" will look different 
        animator.SetBool("Phase1Tired", tired); 
        animator.SetBool("Phase1Attack", attack);

        // if tired, don't do anything other than do the tired idle animation
        // if moving, start crawling
        // if crawling, follow the player in small steps
        if (moving & target != null) {
            if (crawling & target != null) {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, target.position, step);
                FindObjectOfType<ManageAudio>().Play("splat");
            }
        }

        // if attacking dash!
        if (attack & target != null) {
            StartCoroutine(RotateTowardsTarget());
            StartCoroutine(Dash());
            attack = false;
            moving = tired;
        }

        // timer for crawling and transitions in animator
        crawlingTimer += Time.deltaTime;
        transitionTimer += Time.deltaTime;
        
        // crawl move : stand alternator 
        if (crawlingTimer >= crawlingIdleTime) {
            crawling = !crawling;
            crawlingTimer = 0f;
        }

        // check if however many seconds have passed and reset it to change transition to a random boss state
        // can be tired, attacking, or moving
        if (transitionTimer >= transitionLength) {
            int random = new System.Random().Next(1, 3); // change it back to (1, 4 if tired is included)

            // makes a random state happen
            if (random == 1) {
                // moving
                FindObjectOfType<ManageAudio>().Play("screetch");
                moving = true;
                attack = false;
                tired = false;
            } 
            
            if (random == 2) {
                // attacking = dashing
                FindObjectOfType<ManageAudio>().Play("screetch");
                moving = false;
                attack = true;
                tired = false;
            }
            
            if (random == 3) {
                // tired
                FindObjectOfType<ManageAudio>().Play("screetch");
                moving = false;
                attack = false;
                tired = true;
            }

            transitionTimer = 0f;
        }
    }
}