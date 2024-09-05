using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject birdGreen;
    [SerializeField] private GameObject birdRed;
    [SerializeField] private GameObject birdRedLestnica;
    [SerializeField] private GameObject birdRedBomb;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject boat;
    [SerializeField] private GameObject lestnica;
    [SerializeField] private GameObject fishGreen;
    [SerializeField] private GameObject fishRed;
    [SerializeField] private GameObject bochka;
    [SerializeField] private GameObject podlodka;
    [SerializeField] private GameObject vozdShars;
    [SerializeField] private GameObject stolb;
    private float lastDistance;
    private GameObject lastObs;
    private int spawned = 0;
    private int spawned2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastObs = Instantiate(fishGreen, transform.position, new Quaternion());
        lastDistance = Random.RandomRange(12, 12.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            if (transform.position.x - lastObs.transform.position.x >= lastDistance) {

                if (spawned2 % 2 != 0)
                {
                    int dice = Random.Range(1, 6);
                    switch (dice)
                    {
                        case 1:
                            lastObs = Instantiate(bomb, transform.position, new Quaternion());
                            lastObs = Instantiate(vozdShars, transform.position, new Quaternion());
                            lastObs = Instantiate(bomb, transform.position + new Vector3(Random.RandomRange(5, 6), 0, 0), new Quaternion());
                            lastObs = Instantiate(bomb, lastObs.transform.position + new Vector3(Random.RandomRange(5, 6), 0, 0), new Quaternion());
                            break;

                        case 2:
                            lastObs = Instantiate(birdRedBomb, transform.position, new Quaternion());
                            Instantiate(fishGreen, transform.position + new Vector3(Random.RandomRange(-1f, -0.25f), 0, 0), new Quaternion());
                            break;


                        case 3:
                            lastObs = Instantiate(lestnica, transform.position, new Quaternion());
                            int diceBird = Random.Range(1, 5);
                            if (diceBird == 1)
                            {
                                Instantiate(birdRedLestnica, lastObs.transform.position + new Vector3(0, Random.RandomRange(2.5f, 3.5f), 0), new Quaternion());
                            }
                            break;

                        case 4:
                            lastObs = Instantiate(podlodka, transform.position, new Quaternion());
                            break;
                        case 5:
                            lastObs = Instantiate(stolb, transform.position, new Quaternion());
                            break;

                    }
                }
                else {
                    int dice = Random.Range(1, 5);
                    switch (dice)
                    {
                        case 1:
                            lastObs = Instantiate(bomb, transform.position, new Quaternion());
                            
                            lastObs = Instantiate(bomb, transform.position + new Vector3(Random.RandomRange(5, 6), 0, 0), new Quaternion());
                            Instantiate(birdGreen, lastObs.transform.position + new Vector3(Random.RandomRange(5, 6), 0, 0), new Quaternion());
                            lastObs = Instantiate(bomb, lastObs.transform.position + new Vector3(Random.RandomRange(5, 6), 0, 0), new Quaternion());

                            break;

                        case 2:
                            lastObs = Instantiate(boat, transform.position, new Quaternion());
                            break;
                        case 3:
                            lastObs = Instantiate(lestnica, transform.position, new Quaternion());
                            
                            Instantiate(birdGreen, lastObs.transform.position + new Vector3(0, Random.RandomRange(2.5f, 3.5f), 0), new Quaternion());
                            
                            break;
                        case 4:
                            lastObs = Instantiate(bochka, transform.position, new Quaternion());
                            Instantiate(birdGreen, transform.position, new Quaternion());
                            break;
                       

                    }
                }
                
                spawned += 1;
                spawned2 += 1;
                if (spawned >= 3)
                {
                    Instantiate(fishGreen, lastObs.transform.position + new Vector3(6f + Random.RandomRange(-0.5f, 0.5f), 0, 0), new Quaternion());
                    spawned = 0;
                }
                else {
                    int fishDice = Random.Range(1, 6);
                    if (fishDice == 1)
                    {
                        Instantiate(fishRed, lastObs.transform.position + new Vector3(6f + Random.RandomRange(-0.5f, 0.5f), 0, 0), new Quaternion());
                    }
                    if (fishDice == 2)
                    {
                        //Instantiate(fishGreen, lastObs.transform.position + new Vector3(6f + Random.RandomRange(-0.5f, 0.5f), 0, 0), new Quaternion());
                    }
                }
                
                int birdDice = Random.Range(1, 6);
                if (birdDice == 1)
                {
                    Instantiate(birdRed, lastObs.transform.position + new Vector3(6f + Random.RandomRange(-0.5f, 0.5f), 0, 0), new Quaternion());
                }
                if (birdDice == 2)
                {
                    //Instantiate(birdGreen, lastObs.transform.position + new Vector3(6f + Random.RandomRange(-0.5f, 0.5f), 0, 0), new Quaternion());
                }
                lastDistance = Random.RandomRange(12, 12.5f);
            }
        }
    }
}
