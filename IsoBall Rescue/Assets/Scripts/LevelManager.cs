using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if(_instance == null)
                Debug.LogError("Level Manager is null!");
            return _instance;            
        }
    }

    private int _currentSceneIndex;
    [SerializeField] AudioClip acceptActionSFX;

    //public void



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_currentSceneIndex + 1);
        PlayButtonPressedSound();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main");
    }    

    public void ResumeGame()
    {
        UIManager.Instance.ResumeGame();
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_currentSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
        //SceneManager.LoadScene(2);
    }


    public IEnumerator WaitAndLoad()
    {
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(2);        
    }

    public void PlayButtonPressedSound() 
    {
        AudioSource.PlayClipAtPoint(acceptActionSFX, Camera.main.transform.position);
    }


}
