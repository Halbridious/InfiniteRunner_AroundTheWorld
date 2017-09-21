using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Press : MonoBehaviour {

    public void Change_Scene() {
        Game_Manager.score = 0f;
        SceneManager.LoadScene("Game_Scene");
    }

}
