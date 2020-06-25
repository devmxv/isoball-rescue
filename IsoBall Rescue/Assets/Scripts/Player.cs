using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int health;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyEnemy();
    }

    private void DestroyEnemy()
    {

        //---Mouse click to destroy enemy cars
        if (Input.GetMouseButton(0) && GameSession.Instance.stopAllGame == false)
        {
            RaycastHit hit;
            //---Get the mousePosition when clicking
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //---Only applies for enemies showing up
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    //Debug.DrawLine(Camera.main.transform.position, hit.point, Color.green, 0.5f);
                    FindObjectOfType<Enemy>().DestroyEnemy();                    
                                        
                    Destroy(hit.collider.gameObject);
                }
            }            
        }
    }


    //---TODO: For next release
    public int GetHealth()
    {
        return health;
    }

    public void ReduceHealth(int health)
    {
        health -= 1;
    }

    
}
