using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    private const float SLOWMO_WAIT_TIME = 5.0f;
    private const float SLOWMO_TIMESCALE_VALUE = 0.25f;

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

    [SerializeField] int scoreToEnableSlowmo = 5;
    //---Sound when slowmo is enabled by user
    [SerializeField] AudioClip slowmoEnabledSFX;
    //---Sound when the chased object (ambulance) reaches the goal
    [SerializeField] AudioClip chasedSurvivedSFX;
    //---Sound when SlowMo is ready to use
    [SerializeField] AudioClip slowmoReadySFX;

    int score = 0;
    int slowmoCount = 0;
    //---This will help to check how many times the slowmo has been activated
    int slowmoUsage = 0;

    //---When it is active
    public static bool isSlowMoActive;
    //---When it is ready based on conditions apply
    public static bool slowmoReady;
    //---When it is available to start processing
    public static bool slowmoAvailable;

    public bool stopAllGame;

    private Coroutine _slowMoCoroutine;
        
    //---Get the index of the scene
    private int _currentSceneIndex;

    private void Awake()
    {
        //---Set instance of Singleton
        _instance = this;
    }

    private void Start()
    {
        //Time.timeScale = 1;
        isSlowMoActive = false;
        slowmoReady = false;
        slowmoAvailable = true;
        stopAllGame = false;
        //losePanel.SetActive(false);   

        
        //Debug.Log("Final Score: " + currentScore);
    }

    private void Update()
    {              
        HandleSlowmo();                
    }

#region
    
    //---Here we register the ballRef as a new one
    public static void RegisterBall(Ball newBallRef)
    {
        Instance._ballList.Add(newBallRef);
        //---Here, add goal reference to the new instance of the chased object
        GameObject _goalTarget = GameObject.Find("Goal Building");
        newBallRef.GetComponent<Ball>().goal = _goalTarget;
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
    public int GetSlowmoCounter()
    {
        return slowmoCount;
    }

    public void AddSlowmoCount(int value)
    {        
        slowmoCount += value;
    }

    private void AddSlowmoUsage(int usage)
    {
        slowmoUsage += 1;
    }

    public int GetSlowmoUsage()
    {
        return slowmoUsage;
    }           

    public void ShowGameOver()
    {
        //FindObjectOfType<UIManager>().EnableLosePanel();        
        UIManager.Instance.EnableLosePanel();                
    }

    public void ResetSlowMoStatus()
    {
        //yield return new WaitForSeconds(5f);
        slowmoAvailable = true;
        Debug.Log("<color=red>Slow Mo Ready to process</color>");

    }

    private void PlaySlowmoSound()
    {
        AudioSource.PlayClipAtPoint(slowmoEnabledSFX, Camera.main.transform.position);
    }

    public void PlayReachGoalSound()
    {
        AudioSource.PlayClipAtPoint(chasedSurvivedSFX, Camera.main.transform.position);
    }

    public void PlaySlowmoReadySound()
    {
        AudioSource.PlayClipAtPoint(slowmoReadySFX, Camera.main.transform.position);
    }


    //---Checks the points and if it is ready to
    //---activate Slow Mo!
    public void HandleSlowmo()
    {
        //---Obtain current score
        int slowmoCountValue = GetSlowmoCounter();        
        int _slowmoActivation = GetSlowmoUsage();

        //Debug.Log("<color=orange>Counter value:</color> " + GetSlowmoCounter());
        //Debug.Log("<color=orange><b>Number of slowmo used: </b></color>" + _slowmoActivation);
        //Debug.Log("Available?: " + slowmoAvailable);

        if (slowmoCountValue >= scoreToEnableSlowmo && slowmoAvailable == true)
        {
            slowmoReady = true;
            
        } else
        {
            slowmoReady = false;
        }

        if(slowmoReady == true)
        {
            UIManager.Instance.EnableSlowmoText();            
            //---Using right click of mouse to activate SlowMo
            if (Input.GetMouseButtonDown(1) && isSlowMoActive == false)
            {
                //---Set the counter back to 0
                slowmoCount = 0;
                //---if there is a coroutine executing, then it will stop
                if (_slowMoCoroutine != null)
                {
                    StopCoroutine(_slowMoCoroutine);
                    //StartCoroutine(ResetSlowmo());                    
                }
                _slowMoCoroutine = StartCoroutine(SlowMoStart());               
                slowmoAvailable = false;
                Invoke("ResetSlowMoStatus", 5f);
                UIManager.Instance.ResetSlowMo();
                AddSlowmoUsage(1);
                
                
            }

        //--Checks if there is enough score to activate SloMo
        //if (slowmoCountValue >= scoreToEnableSlowmo)
        //{
            
        //    UIManager.Instance.EnableSlowmoText();
        //    //---Using right click of mouse to activate SlowMo
        //    if (Input.GetMouseButtonDown(1) && isSlowMoActive == false)
        //    {
        //        //---Set the counter back to 0
        //        slowmoCount = 0;
        //        //---if there is a coroutine executing, then it will stop
        //        if (_slowMoCoroutine != null)
        //        {
        //            StopCoroutine(_slowMoCoroutine);
        //            //StartCoroutine(ResetSlowmo());                    
        //        }
        //        _slowMoCoroutine = StartCoroutine(SlowMoStart());
        //        AddSlowmoUsage(1);
        //        slowmoReady = false;
        //    }
        }        
    }
    
    IEnumerator SlowMoStart()
    {
        isSlowMoActive = true;
        PlaySlowmoSound();
        Time.timeScale = SLOWMO_TIMESCALE_VALUE;
        Debug.Log("<color=green><b>Starting SlowMo</b></color> " + Time.unscaledTime);        
               
        yield return new WaitForSecondsRealtime(SLOWMO_WAIT_TIME);
               
        Time.timeScale = 1;
        Debug.Log("<color=red><b>Ending SlowMo</b></color> " + Time.unscaledTime);
        isSlowMoActive = false;        
    }

    //---Saves Score to PlayerPrefs file (WIP)
    public void SaveScore()
    {
        PlayerPrefs.SetInt("HighScores", GetScore());
    }




    //---Enable Slow Mo
    //TimeManager.Instance.SetSlowMo();
    //yield return new WaitForSecondsRealtime(1f);
    //---Return slider value to 0
    //UIManager.Instance.ResetSlowMo();
    //Debug.Log("SlowMo Active: " + _isSlowMoActive);
    //_isSlowMoActive = false;
    //TimeManager.Instance.UnsetSlowmo();


    //private const float SLOWMO_EASE_IN_TIME = 1.0f;
    //private const float SLOWMO_WAIT_TIME = 3.0f;
    //private const float SLOWMO_EASE_OUT_TIME = 1.0f;

    //IEnumerator SlowMoStart()
    //{
    //    _isSlowMoActive = true;
    //    Time.timeScale = 1;
    //    Debug.Log("<color=green><b>Starting SlowMo</b></color> " + Time.unscaledTime);
    //    float lerpedTime = SLOWMO_EASE_IN_TIME;
    //    do
    //    {
    //        lerpedTime -= Time.unscaledDeltaTime;
    //        if(lerpedTime < 0)
    //        {
    //            lerpedTime = 0;
    //        }
    //        Time.timeScale = lerpedTime;
    //        Debug.Log(lerpedTime);
    //        yield return new WaitForEndOfFrame();

    //    } while (lerpedTime > 0);

    //    Time.timeScale = 0;

    //    Debug.Log("<color=white><b>SlowMo Fully Active</b></color> " + Time.unscaledTime);
    //    yield return new WaitForSecondsRealtime(SLOWMO_WAIT_TIME);

    //    lerpedTime = SLOWMO_EASE_OUT_TIME;
    //    do
    //    {
    //        lerpedTime -= Time.unscaledDeltaTime;
    //        if (lerpedTime < 0)
    //        {
    //            lerpedTime = 0;
    //        }
    //        Time.timeScale = 1 - lerpedTime;
    //        Debug.Log(lerpedTime);
    //        yield return new WaitForEndOfFrame();

    //    } while (lerpedTime > 0);




    //    Time.timeScale = 1;
    //    Debug.Log("<color=red><b>Ending SlowMo</b></color> " + Time.unscaledTime);
    //    _isSlowMoActive = false;


    //    //---Enable Slow Mo
    //    //TimeManager.Instance.SetSlowMo();
    //    //yield return new WaitForSecondsRealtime(1f);
    //    //---Return slider value to 0
    //    //UIManager.Instance.ResetSlowMo();
    //    //Debug.Log("SlowMo Active: " + _isSlowMoActive);
    //    //_isSlowMoActive = false;
    //    //TimeManager.Instance.UnsetSlowmo();



    //}   
}
