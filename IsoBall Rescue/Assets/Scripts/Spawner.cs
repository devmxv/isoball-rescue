using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject objectPrefab;
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
        Instantiate(objectPrefab, transform.position, Quaternion.identity);
    }
}
