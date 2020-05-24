using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //public int speed;
    //public GameObject ball;
    public NavMeshAgent enemy;

    // Update is called once per frame
    void Update()
    {
        GameObject chaseTarget = GameSession.getClosestBall(transform.position).gameObject;
        //---Route to the ball in order to destroy it
        enemy.SetDestination(chaseTarget.transform.position);
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
