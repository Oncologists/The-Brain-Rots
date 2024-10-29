using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] private float speed; 
    [SerializeField] private Animator animator;
    [SerializeField] private float crawlingIdleTime; // how long he stands per "crawl" movement
    [SerializeField] private float transitionLength; // how long each transition is
    private float crawlingTimer = 0f; // timer for the little crawl animation
    private float transitionTimer = 0f;
    private bool moving = true; // this is the transition of whether or not he is moving at all 
    private bool crawling = false; // this is just to alternate between moving and not moving for choppy movement 
    private bool tired = false;
    private bool attack = false;

    // this script just makes it so every "toggleInterval" seconds the boss moves slightly towards 
    // the player, for a "crawling" animation
    void Update() {
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

        // if attacking, play the animation signalling the attack and then summon the blood aoe
        if (attack & target != null) {
            // TBD
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
                // attacking
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