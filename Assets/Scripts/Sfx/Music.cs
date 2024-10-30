using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        FindObjectOfType<ManageAudio>().PlayLoop("music");
        FindObjectOfType<ManageAudio>().PlayLoop("heartbeat");
    }
}