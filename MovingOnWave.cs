using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingOnWave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            if (GameManager.springOnes.Count > 0)
            {
                transform.position += new Vector3(-GameManager.obsMoveSpeed, 0, 0) * Time.deltaTime;
                int minIndex1 = 0;
                int minIndex2 = 0;
                float minDist1 = 100;
                float minDist2 = 100;
                for (int i = 0; i < GameManager.springOnes.Count; i++)
                {
                    if (Vector3.Distance(transform.position, GameManager.springOnes[i].transform.position) < minDist1)
                    {
                        minDist2 = minDist1;
                        minIndex2 = minIndex1;
                        minDist1 = Vector3.Distance(transform.position, GameManager.springOnes[i].transform.position);

                        minIndex1 = i;
                        
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, GameManager.springOnes[i].transform.position) < minDist2 && i != GameManager.springOnes.Count - 1)
                        {
                            minDist2 = Vector3.Distance(transform.position, GameManager.springOnes[i].transform.position);
                            minIndex2 = i;
                        }
                    }
                }
                Vector3 newVectorRight = GameManager.springOnes[minIndex2].transform.position - GameManager.springOnes[minIndex1].transform.position;
                transform.rotation = Quaternion.Euler(0,0,(GameManager.springOnes[minIndex2].transform.position.y - GameManager.springOnes[minIndex1].transform.position.y) * 75f);
                
            }
            else {
                
            }
            
        }
    }
}
