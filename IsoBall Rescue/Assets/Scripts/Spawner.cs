using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject ballPrefab;
    [SerializeField] float spawnTime = 10f;
    //[SerializeField]
    // Start is called before the first frame update

    bool _spawn = true;

    IEnumerator Start()
    {
        while (_spawn)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnBall();
        }
    }

    //private void Start()
    //{
    //    SpawnBall();
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnBall()
    {
        Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }
}
