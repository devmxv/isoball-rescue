using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
        if (Input.GetMouseButton(0))
        {
            //---Returns a Vector 3 pos with mouse position
            //---I have been changing the z value to 50f in order to work
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50f));

            //---Get direction vector from camera pos to mouse pos in world space
            Vector3 direction = worldMousePos - Camera.main.transform.position;
            Debug.Log(direction);

            RaycastHit hit;

            //---Cast a ray from the camera in the given direction
            if(Physics.Raycast(Camera.main.transform.position, direction, out hit, Mathf.Infinity))
            {

                if (hit.collider.gameObject.tag == "Enemy")
                {
                    Debug.DrawLine(Camera.main.transform.position, hit.point, Color.green, 0.5f);
                    GameSession.Instance.AddToScore(1);
                    Destroy(hit.collider.gameObject);
                }
                //Debug.DrawLine(Camera.main.transform.position, hit.point, Color.green, 0.5f);
                //Destroy(hit.collider.gameObject);

            } else
            {
                Debug.DrawLine(Camera.main.transform.position, worldMousePos, Color.red, 0.5f);
            }

        }
    }
}
