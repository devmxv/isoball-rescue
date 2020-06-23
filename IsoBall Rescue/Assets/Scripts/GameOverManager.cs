using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private GameObject _finalScore;
    private GameObject _sessionScoreObj;

    private int _sessionScore;


    // Start is called before the first frame update
    void Start()
    {
        _sessionScore = GameSession.Instance.GetScore();

        _finalScore = transform.Find("Final Score Number").gameObject;
        _finalScore.GetComponent<Text>().text = PlayerPrefs.GetInt("HighScores").ToString();

        _sessionScoreObj = transform.Find("Current Score Number").gameObject;
        _sessionScoreObj.GetComponent<Text>().text = _sessionScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
