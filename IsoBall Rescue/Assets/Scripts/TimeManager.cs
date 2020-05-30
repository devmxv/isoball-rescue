using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //---Make it singleton
    private static TimeManager _instance;
    public static TimeManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("TimeManager is null!");
            return _instance;
        }
    }


    [SerializeField] float slowdownFactor = 0.05f;
    [SerializeField] float slowdownLength = 2f;
    

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //---Using right click to activate slowmo
        //---This is for testing purposes, it is active using conditions
        //SetSlowMo();
        //UnsetSlowmo();
        
        

    }

    public void SetSlowMo()
    {
        //if (Input.GetMouseButton(1))
        //{

        //    Time.timeScale = slowdownFactor;
        //    // 1/0.05 = 20 times slower than normal

        //    //---smooth slowMo
        //    Time.fixedDeltaTime = Time.timeScale * .02f;
        //}
        GameSession.isSlowMoActive = true;
        Time.timeScale = slowdownFactor;
        // 1/0.05 = 20 times slower than normal

        //---smooth slowMo
        Time.fixedDeltaTime = Time.timeScale * .02f;

    }

    public void UnsetSlowmo()
    {
        //---if game is not paused and slowMo is disabled
        if (UIManager.gameIsPaused == false && GameSession.isSlowMoActive == false)
        {
           
            //---Return progresively the scale of time to 1
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            Debug.Log("Setting back time! GameIsPaused " + UIManager.gameIsPaused);
        }
    }    
}
