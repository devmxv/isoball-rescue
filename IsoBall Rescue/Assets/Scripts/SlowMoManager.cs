using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int score = GameSession.Instance.GetScore();
        Debug.Log(score);
        GetComponent<Slider>().value = score; 
        
    }

    public void ResetSlowMo()
    {
        GetComponent<Slider>().value = 0;
    }
}
