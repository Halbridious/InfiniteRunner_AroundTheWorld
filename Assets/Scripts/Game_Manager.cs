using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    public static int highScore = 0;

    public static float score = 0;

    private static bool created = false;

	void Awake () {

        if( !created ) {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        } else {
            Destroy(this.gameObject);
        }        
	}

    private void LateUpdate() {
        if(score > highScore ) {
            highScore = (int)score;
        }
    }
}
