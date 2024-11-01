using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Boss;
    public GameObject Healthbar;
    public Dashing returnToZero;
    public GameObject phase2;
    public GameObject phase3;
    public GameObject phase4;
    private int stage = 1;
    // Start is called before the first frame update
    void Start()
    {
        Healthbar.GetComponent<HealthBar>().setMaxHealth((int) Player.GetComponent<PlayerHealth>().GetMaxHP());
    }

    // Update is called once per frame
    void Update()
    {
        Healthbar.GetComponent<HealthBar>().setHealth((int) Player.GetComponent<PlayerHealth>().GetCurrentHP());
        if (Player.GetComponent<PlayerHealth>().GetCurrentHP() <= 0) {
            Player.GetComponent<PlayerHealth>().Kill();
        }
        if (Boss.GetComponent<PlayerHealth>().GetCurrentHP() <= 0) {
            NextPhase();
        }
    }

    void NextPhase() {
        //Play Death Animation and Flesh Blobs going back to the middle.
        Dashing dashingComponent = returnToZero.GetComponent<Dashing>();
        dashingComponent.MoveToOrigin();
        stage++;
        if (stage == 2) {
            Instantiate(phase2, Vector2.zero, Quaternion.identity);
        }
        if (stage == 3) {
            Instantiate(phase3, Vector2.zero, Quaternion.identity);
        }
        if (stage == 4) {
            Instantiate(phase4, Vector2.zero, Quaternion.identity);
        }
        Boss.GetComponent<PlayerHealth>().Kill();
    }
}
