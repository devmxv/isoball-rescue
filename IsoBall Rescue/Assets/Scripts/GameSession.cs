using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    //---List to register/unregister Ball
    private List<Ball> _ballList = new List<Ball>();
    //---Make it singleton
    private static GameSession _instance;
    public static GameSession Instance
    {
        get
        {
            if(_instance == null)
                Debug.LogError("GameSession is null!");
            return _instance;
        }
    }

    int score = 0;
    private bool _isSlowMoFinished;
    

    [SerializeField] GameObject losePanel;
    //---Get the index of the scene
    private int _currentSceneIndex;

    private void Awake()
    {
        //---Set instance of Singleton
        _instance = this;
    }

    private void Start()
    {
        _isSlowMoFinished = false;
        losePanel.SetActive(false);
        //---Get Text component

    }

    private void Update()
    {
        HandleSlowmoPoints();
        //if (_isSlowMoFinished == true)
        //    FindObjectOfType<SlowMoManager>().ResetSlowMo();
        //_isSlowMoFinished = false;
    }


    //---Ball funcionality

#region
    
    //---Here we register the ballRef as a new one
    public static void RegisterBall(Ball newBallRef)
    {
        Instance._ballList.Add(newBallRef);
    }

    //---Unregister the ball from List
    public static void RemoveBall(Ball removeBallRef)
    {
        Instance._ballList.Remove(removeBallRef);
    }

    public static Ball getClosestBall(Vector3 pos)
    {
        Ball closestBall = null;
        float closestDistance = float.MaxValue;
        for (int i = 0; i < Instance._ballList.Count; i++)
        {
            float distance = Vector3.Distance(pos, Instance._ballList[i].transform.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestBall = Instance._ballList[i]; 
            }
        }
        return closestBall;
    }

#endregion

    //---Retrieve score of the level
    public int GetScore()
    {
        return score;
    }


    //---Sums the points assigned in game
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(_currentSceneIndex);
    }

    public void ShowGameOver()
    {
        losePanel.SetActive(true);
    }


    //---Checks the points and if it is ready to
    //---activate Slow Mo!
    public void HandleSlowmoPoints()
    {
        int score = GetScore();
        if (score >= 2)
        {
            Debug.Log("Slow Mo Active!");
            FindObjectOfType<TimeManager>().SetSlowMo();
            _isSlowMoFinished = true;
            
        }

        
    }







}
