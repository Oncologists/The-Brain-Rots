using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathAnimation : MonoBehaviour {
    public Transform target;
    public Animator animator;

    void Start() {
        
    }

    void Update() {
        transform.position = target.position;
    }

    public void startAnimation() {
        transform.position = target.position;
        animator.SetBool("Death", true);
        StartCoroutine(leave());
    }

    private IEnumerator leave () {
        yield return new WaitForSeconds(1.7f);
        transform.position = new Vector2(99, 99);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Win");
    }
}