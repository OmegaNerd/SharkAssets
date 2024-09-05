using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : DamageObject
{
    [SerializeField] private float speedSky;
    [SerializeField] private float speedWater;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GameManager.state == GameState.Game) {
            if (transform.position.y > 0)
            {
                transform.position += new Vector3(0, -speedSky, 0) * Time.deltaTime;
            }
            else {
                transform.position += new Vector3(0, -speedWater, 0) * Time.deltaTime;
            }
        }
    }
}
