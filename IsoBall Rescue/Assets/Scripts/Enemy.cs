using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //public int speed;
    //public GameObject ball;
    public NavMeshAgent enemy;
    [SerializeField] AudioClip enemyDestroySFX;
       
    // Update is called once per frame
    void Update()
    {        
        GameObject chaseTarget = GameSession.getClosestBall(transform.position).gameObject;        
        //---Route to the ball in order to destroy it
        enemy.SetDestination(chaseTarget.transform.position);
    }


    //---Check collision with player/chased player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            //Reduce Health
            //Debug.Log("HIT!");
            //FindObjectOfType<Player>().ReduceHealth(1);


            //---Game Over!
            //FindObjectOfType<Ball>().PlayDeathSound();
            
            //---Attempt so enemies stop moving
            //_myRigidBody.velocity = Vector3.zero;
            

            UIManager.Instance.EnableLosePanel();            
            Debug.Log("Game Over!");
            //LevelManager.Instance.LoadGameOver();
            FindObjectOfType<LevelManager>().LoadGameOver();

        }
    }

    public void PlayDestroyAudio()
    {
        AudioSource.PlayClipAtPoint(enemyDestroySFX, Camera.main.transform.position);
    }

    




}
