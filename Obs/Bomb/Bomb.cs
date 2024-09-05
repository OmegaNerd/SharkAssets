using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : DamageObject
{
    
    
    [SerializeField] private float speedMin;
    [SerializeField] private float speedMax;
    [SerializeField] private float yMax;
    [SerializeField] private float yMin;
    private bool movingUp = false;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.RandomRange(speedMin, speedMax);
        transform.position = new Vector3(transform.position.x, Random.RandomRange(yMin, yMax), transform.position.z);
        int dice = Random.Range(1, 3);
        if (dice == 1) { 
            movingUp = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.state == GameState.Game) {
            if (movingUp == true)
            {
                transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
                if (transform.position.y >= yMax)
                {
                    transform.position = new Vector3(transform.position.x, yMax, transform.position.z);
                    movingUp = false;
                }
            }
            else {
                transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
                if (transform.position.y <= yMin)
                {
                    transform.position = new Vector3(transform.position.x, yMin, transform.position.z);
                    movingUp = true;
                }
            }
        }
    }

    
}
