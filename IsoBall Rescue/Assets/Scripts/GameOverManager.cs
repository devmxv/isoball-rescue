using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private GameObject _finalScore;


    // Start is called before the first frame update
    void Start()
    {
        _finalScore = transform.Find("Final Score Number").gameObject;
        _finalScore.GetComponent<Text>().text = PlayerPrefs.GetInt("HighScores").ToString();
        //if (_finalScore)
        //{
        //    Debug.Log("Found!");
        //} else
        //{
        //    Debug.Log("Not found");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
