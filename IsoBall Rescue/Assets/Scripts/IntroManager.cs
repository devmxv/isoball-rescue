using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] GameObject _panelOne;
    [SerializeField] GameObject _panelTwo;

    private GameObject _nextButton;
    private GameObject _prevButton;

    private int _currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        //_panelOne = GameObject.Find("Panel 1");
        //_panelTwo = GameObject.Find("Panel 2");
        _panelOne.SetActive(true);
        _panelTwo.SetActive(false);

        _nextButton = GameObject.Find("Right Button");
        _prevButton = GameObject.Find("Left Button");

        _prevButton.SetActive(false);

        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPanel()
    {
        _panelOne.SetActive(false);
        _panelTwo.SetActive(true);

        _prevButton.SetActive(true);
        _nextButton.SetActive(false);
    }

    public void PreviousPanel()
    {
        _panelOne.SetActive(true);
        _panelTwo.SetActive(false);

        _nextButton.SetActive(true);
        _prevButton.SetActive(false);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(_currentSceneIndex + 1);
    }
}
