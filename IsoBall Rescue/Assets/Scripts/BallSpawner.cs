using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    [SerializeField] GameObject chasedPrefab;
    [SerializeField] float spawnTime = 10f;
    //[SerializeField]
    // Start is called before the first frame update

    bool _spawn = true;

    IEnumerator Start()
    {
        while (_spawn)
        {
            yield return new WaitForSeconds(spawnTime);
            Spawn();
        }
    }


    //---Testing purposes
    //private void Start()
    //{
    //    SpawnBall();
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        GameObject ball = Instantiate(chasedPrefab, transform.position, Quaternion.identity);
        ////---Get the Goal object and assign it to goalTarget
        //var goalTarget = GameObject.Find("Goal Building");
        //ball.GetComponent<Ball>().goal = goalTarget;
        
    }
}
