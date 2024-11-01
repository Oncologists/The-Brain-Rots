using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
   public void PlayGame() {
        SceneManager.LoadSceneAsync("Phase 1");
    }

    void Start() {
        FindObjectOfType<ManageAudio>().PlayLoop("menumetal");
    }
}