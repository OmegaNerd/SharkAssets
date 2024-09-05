using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pero : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            if (transform.position.y > 0)
            {
                transform.position += new Vector3(0, -2.5f, 0) * Time.deltaTime;
            }
            else {
                transform.position += new Vector3(0, -1f, 0) * Time.deltaTime;
            }
        }
    }
}
