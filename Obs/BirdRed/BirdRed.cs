using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdRed : DamageObject
{
    private Camera _cam;
    [SerializeField] private GameObject bombSpawn;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject bombSpawnPos;
    [SerializeField] private bool withBomb = false;
    [SerializeField] private bool lestnica = false;
    private bool bombSpawned = false;
    private float distanceToDrop;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        bomb.SetActive(withBomb);
        distanceToDrop = Random.RandomRange(6f, 8f);
        if (lestnica == false) {
            transform.position = new Vector3(transform.position.x, Random.RandomRange(0.75f, 3.5f), transform.position.z);
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (GameManager.state == GameState.Game)
        {
            transform.position += new Vector3(-GameManager.birdMoveSpeed, 0, 0) * Time.deltaTime;
            if (withBomb == true) {
                if (transform.position.x - _cam.transform.position.x <= distanceToDrop && bombSpawned == false)
                {
                    bombSpawned = true;
                    Instantiate(bombSpawn, bombSpawnPos.transform.position, new Quaternion());
                    bomb.SetActive(false);
                }
            }
            
        }
    }
}
