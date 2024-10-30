using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Boss;
    private int stage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerHealth>().GetCurrentHP() <= 0) {
            Player.GetComponent<PlayerHealth>().Kill();
        }
        if (Boss.GetComponent<PlayerHealth>().GetCurrentHP() <= 0) {
            NextPhase();
            Boss.GetComponent<PlayerHealth>().Kill();
        }
    }

    void NextPhase() {
        //Play Death Animation and Flesh Blobs going back to the middle.
        stage++;
        if (stage == 2) {
            //Summon Stage 2 Prefab
        }
        if (stage == 3) {
            //Summon Stage 3 Prefab
        }
        if (stage == 4) {
            //Summon Stage 4 Prefab
        }
    }
}
