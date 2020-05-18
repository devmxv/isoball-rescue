using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //public int speed;
    //public GameObject ball;

    
    public NavMeshAgent enemy;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject chaseTarget = GameSession.getClosestBall(transform.position).gameObject;
        //---Route to the ball in order to destroy it
        enemy.SetDestination(chaseTarget.transform.position);
        




        //---Chase ball based in position
        //Vector3 localPosition = ball.transform.position - transform.position;
        //localPosition = localPosition.normalized;
        //transform.Translate(localPosition.x * Time.deltaTime * speed, localPosition.y * Time.deltaTime * speed,
        //    localPosition.z * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            //---Game Over!
            Debug.Log("Game Over!");
            
        }
    }


    
    
}
