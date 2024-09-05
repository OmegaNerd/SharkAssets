using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : FoodObject
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, Random.RandomRange(2f, 3.5f));
    }

    void FixedUpdate()
    {

        if (GameManager.state == GameState.Game)
        {
            transform.position += new Vector3(-GameManager.birdMoveSpeed, 0, 0) * Time.deltaTime;
        }
    }
}
