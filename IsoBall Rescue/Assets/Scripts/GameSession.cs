using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    //---List to register/unregister Ball and recycle it
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
    public static bool _isSlowMoActive;
    public static bool slowmoReady;
    
    

    //---Get the index of the scene
    private int _currentSceneIndex;

    private void Awake()
    {
        //---Set instance of Singleton
        _instance = this;
    }

    private void Start()
    {
        _isSlowMoActive = false;
        slowmoReady = false;
        //losePanel.SetActive(false);
    }

    private void Update()
    {

        HandleSlowmo();

        if (_isSlowMoActive == true)
        {
            UIManager.Instance.ResetSlowMo();
            _isSlowMoActive = false;
        }
        
    }

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

    //---Helper of the enemy to get the closest ball so it can chase it
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
        //FindObjectOfType<UIManager>().EnableLosePanel();
        UIManager.Instance.EnableLosePanel();
    }


    //---Checks the points and if it is ready to
    //---activate Slow Mo!
    public void HandleSlowmo()
    {
        //---Obtain current score
        int score = GetScore();
        if (score >= 0)
        {
            Debug.Log("Slow Mo ready to use!");
            slowmoReady = true;
            UIManager.Instance.EnableSlowmoText();
            //---Using right click of mouse to activate SlowMo
            if (Input.GetMouseButton(1))
            {
                StartCoroutine(SlowMoStart());
                //FindObjectOfType<TimeManager>().SetSlowMo();                
            }


        }
    }

    IEnumerator SlowMoStart()
    {
        //---Enable Slow Mo
        TimeManager.Instance.SetSlowMo();
        //---Return slider to 0
        UIManager.Instance.ResetSlowMo();

        yield return new WaitForSecondsRealtime(5f);

        Debug.Log("Powerup ended" + _isSlowMoActive);
        _isSlowMoActive = false;
        
    }







}
