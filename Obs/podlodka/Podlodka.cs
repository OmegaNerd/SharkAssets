using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podlodka : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, Random.RandomRange(-0.5f, -3.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            transform.position += new Vector3(-GameManager.obsMoveSpeed, 0, 0) * Time.deltaTime;
        }
    }
}
