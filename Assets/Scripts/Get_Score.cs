using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Get_Score : MonoBehaviour {

    [Tooltip("The last game's score box")]
    [SerializeField]
    private GameObject boxScore;

    [Tooltip("The session's high score box")]
    [SerializeField]
    private GameObject boxHScore;

    // Update is called once per frame
    void Start () {
        boxScore.GetComponent<Text>().text = Game_Manager.score.ToString();
        boxHScore.GetComponent<Text>().text = Game_Manager.highScore.ToString();
	}
}
