using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float slowdownFactor = 0.05f;
    [SerializeField] float slowdownLength = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //---Using right click to activate slowmo
         
        //SetSlowMo();
        

        //---Return progresively the scale of time to 1
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

    }

    public void SetSlowMo()
    {
        if (Input.GetMouseButton(1))
        {

            Time.timeScale = slowdownFactor;
            // 1/0.05 = 20 times slower than normal

            //---smooth slowMo
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }
        
    }
}
