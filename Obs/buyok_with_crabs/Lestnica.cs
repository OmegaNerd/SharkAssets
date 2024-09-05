using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lestnica : DamageObject
{
    [SerializeField] private GameObject Buyok;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Buyok, transform.position, new Quaternion());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.state == GameState.Game) {
            transform.position += new Vector3(-GameManager.obsMoveSpeed, 0, 0) * Time.deltaTime;
        }
    }
}
