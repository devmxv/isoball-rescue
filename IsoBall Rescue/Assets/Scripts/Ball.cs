using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ball : MonoBehaviour
{
    public GameObject goal;
    [SerializeField] NavMeshAgent ball;
    [SerializeField] int scoreBall = 5;

    [SerializeField] int health = 2;

    [SerializeField] AudioClip playerDeathSFX;




    // Start is called before the first frame update
    void Start()
    {
        GameSession.RegisterBall(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        //---transform.position gets the position of the goal
        ball.SetDestination(goal.transform.position);
        //Debug.Log("Health " + health);
    }

    private void OnTriggerEnter(Collider other)
    {
        //---if ball reaches the goal, it destroys and add points
        if (GameObject.FindGameObjectWithTag("Goal"))
        {            
            //---Add score to game
            GameSession.Instance.AddToScore(scoreBall);
            //---Sound of victory
            GameSession.Instance.PlayReachGoalSound();
            Destroy(this.gameObject);
        }
    }

    public void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(playerDeathSFX, Camera.main.transform.position);
    }

    private void OnDestroy()
    {
        GameSession.RemoveBall(this);
    }

    


}
