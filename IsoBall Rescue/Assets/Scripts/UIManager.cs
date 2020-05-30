using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UI Manager not defined!");
            return _instance;
        }

    }


    private GameObject _scoreNumberObj;
    private GameObject _sliderSlowMo;

    private GameObject _finalScoreObj;
    private Text _finalScoreText;

    private Slider _sliderSlowMoValue;
    private GameObject _slowMoText;
    private Text _scoreText;
    private Text _slowMoTextActive;
    private int _score;

    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject pausePanel;

    public static bool gameIsPaused = false;



    private void Awake()
    {
        //---Instance of singleton for UI
        _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        _scoreNumberObj = this.transform.Find("Score Number").gameObject;
        _scoreText = _scoreNumberObj.GetComponent<Text>();

        _sliderSlowMo = this.transform.Find("Slider").gameObject;
        _sliderSlowMoValue = _sliderSlowMo.GetComponent<Slider>();

        _slowMoText = this.transform.Find("Right Click").gameObject;               

        losePanel.SetActive(false);
        _slowMoText.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {

        _score = GameSession.Instance.GetSlowmoCounter();
        _scoreText.text = GameSession.Instance.GetScore().ToString();        
        _sliderSlowMoValue.value = _score;
        
        //---Pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                ShowPauseMenu();
            }

        }
    }


    //---Reset the slowMo Slider when the powerup depletes
    public void ResetSlowMo()
    {        
        //Debug.Log("Slow Mo Restarted!" + GameSession._isSlowMoActive);
         _sliderSlowMoValue.value = 0;                
    }

    //---Set off the lose panel when losing (lol)
    public void DisableLosePanel()
    {
        losePanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void EnableLosePanel()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowPauseMenu()
    {
        //bool gamePaused = GameSession.GameIsPaused;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        
        
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void EnableSlowmoText()
    {
        _slowMoText.SetActive(true);        
    }
    
}
