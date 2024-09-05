using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpringStack : MonoBehaviour
{
    [SerializeField] private float wavesSmooth;
    [SerializeField] private GameObject spring;
    [SerializeField] private float maxWaveLen;
    private float startWaveLen;
    public List<SpringOne> springs = new List<SpringOne>();
    public PlayerController playerController;
    public bool canMoveWater;
    [SerializeField] private float waveLenToStopMove;
    [SerializeField] private SpringStack otherStack;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < otherStack.transform.position.x)
        {
            
        }
        else {
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            
            if (GameManager.springOnes.Count == 0 || (GameManager.springOnes[0].transform.position.x > GameManager.springOnes[GameManager.springOnes.Count - 1].transform.position.x && springs[0].transform.position.x < otherStack.springs[0].transform.position.x)) { 
                GameManager.springOnes.Clear();
                for (int i = 0; i < springs.Count; i++) {
                    GameManager.springOnes.Add(springs[i]);
                }
                for (int i = 0; i < otherStack.springs.Count; i++)
                {
                    GameManager.springOnes.Add(otherStack.springs[i]);
                }
            }
            float maxDist = 0;
            for (int i = 0; i < springs.Count; i++) {
                if (springs[i].dampingLen > maxDist) { 
                    maxDist = springs[i].dampingLen;
                }
            }
            if (maxDist > waveLenToStopMove)
            {
                canMoveWater = false;
            }
            else {
                canMoveWater = true;
            }
            if (playerController.transform.position.y > -0.2f && playerController.transform.position.y < 0.2f)
            {
                
                float minDist = 100;
                int minDistIndex = 0;
                for (int i = 0; i < springs.Count; i++)
                {
                    if (Vector3.Distance(springs[i].transform.position, playerController.transform.position) < minDist)
                    {
                        minDist = Vector3.Distance(springs[i].transform.position, playerController.transform.position);
                        minDistIndex = i;
                    }
                }
                if (playerController.jumpSpeed < 0)
                {
                    startWaveLen = -playerController.jumpSpeed / 10 * maxWaveLen;
                    if (minDistIndex != 0)
                    {
                        if (minDistIndex != springs.Count - 1)
                        {
                            DampWave(minDistIndex, true);
                        }
                        else {
                            DampWave(minDistIndex, true);
                        }
                    }
                    else {
                        DampWave(minDistIndex, true);
                    }
                }
                else { 
                    startWaveLen = playerController.jumpSpeed / 10 * maxWaveLen * 0.5f;
                    if (minDistIndex != 0)
                    {
                        if (minDistIndex != springs.Count - 1)
                        {
                            DampWave(minDistIndex, true);
                        }
                        else
                        {
                            DampWave(minDistIndex, true);
                        }
                    }
                    else {
                        DampWave(minDistIndex, true);
                    }
                }
                
            }
        }
        
    }

    public void SpawnSprings(int springsCount) { 
        for (int i = 0; i < springsCount; i++)
        {
            SpringOne newSpring = Instantiate(spring, transform.position, new Quaternion()).GetComponent<SpringOne>();
            springs.Add(newSpring);
        }
    }

    public void DampWave(int startWaveIndex, bool startUp) {
        if (springs[startWaveIndex].dampingLen < startWaveLen) {
            bool currentWaveUp = startUp;
            float currentWaveLen = startWaveLen;
            springs[startWaveIndex].dampingLenMax = startWaveLen;
            springs[startWaveIndex].dampingLen = currentWaveLen - Mathf.Abs(playerController.transform.position.x - springs[startWaveIndex].transform.position.x) * wavesSmooth;
            springs[startWaveIndex].dampingUp = currentWaveUp;
            //currentWaveLen = currentWaveLen / wavesSmooth;
            currentWaveUp = !currentWaveUp;
            for (int i = startWaveIndex + 1; i < springs.Count; i++)
            {
                springs[i].dampingLenMax = startWaveLen;
                springs[i].dampingLen = currentWaveLen - Mathf.Abs(playerController.transform.position.x - springs[i].transform.position.x) * wavesSmooth;
                springs[i].dampingUp = currentWaveUp;
                //currentWaveLen = currentWaveLen / wavesSmooth;
                currentWaveUp = !currentWaveUp;
            }
            currentWaveUp = !currentWaveUp;
            if (springs[0].transform.position.x < otherStack.springs[0].transform.position.x) {
                for (int i = 0; i < otherStack.springs.Count; i++)
                {
                    otherStack.springs[i].dampingLenMax = startWaveLen;
                    otherStack.springs[i].dampingLen = currentWaveLen - Mathf.Abs(playerController.transform.position.x - otherStack.springs[i].transform.position.x) * wavesSmooth;
                    otherStack.springs[i].dampingUp = currentWaveUp;
                    //currentWaveLen = currentWaveLen / wavesSmooth;
                    currentWaveUp = !currentWaveUp;
                }
            }
            currentWaveUp = !startUp;
            //currentWaveLen = startWaveLen / wavesSmooth;
            for (int i = startWaveIndex - 1; i >= 0; i--)
            {
                springs[i].dampingLenMax = startWaveLen;
                springs[i].dampingLen = currentWaveLen - Mathf.Abs(playerController.transform.position.x - springs[i].transform.position.x) * wavesSmooth;
                springs[i].dampingUp = currentWaveUp;
                //currentWaveLen = currentWaveLen / wavesSmooth;
                currentWaveUp = !currentWaveUp;
            }
            currentWaveUp = !currentWaveUp;
            if (springs[0].transform.position.x > otherStack.springs[0].transform.position.x) {
                for (int i = otherStack.springs.Count - 1; i >= 0; i--)
                {
                    otherStack.springs[i].dampingLenMax = startWaveLen;
                    otherStack.springs[i].dampingLen = currentWaveLen - Mathf.Abs(playerController.transform.position.x - otherStack.springs[i].transform.position.x) * wavesSmooth;
                    otherStack.springs[i].dampingUp = currentWaveUp;
                    //currentWaveLen = currentWaveLen / wavesSmooth;
                    currentWaveUp = !currentWaveUp;
                }
            }
        }
        
    }


}
