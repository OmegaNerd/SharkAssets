using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Waving : MonoBehaviour
{
    [SerializeField] private float xMin;
    [SerializeField] private float xRespawn;
    [SerializeField] private SpriteShapeController spriteShapeController;
    private Spline waterSpline;
    private int CornersCount = 2;
    [SerializeField] private int WavesCount = 8;
    [SerializeField] private SpringStack springStack;
    private Camera _cam;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private bool secondWave;
    private bool wavesFar = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        waterSpline = spriteShapeController.spline;
        SetWaves();
        //waterSpline.SetPosition(1, new Vector3(waterSpline.GetPosition(1).x, 2, waterSpline.GetPosition(1).z));
        if (secondWave == false) {
            springStack.SpawnSprings(WavesCount + 2);
            for (int i = 1; i < WavesCount + 3; i++)
            {
                springStack.springs[i - 1].transform.position = new Vector3(waterSpline.GetPosition(i).x * 15, 0, 0);
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.state == GameState.Game) {

            for (int i = 1; i < WavesCount + 3; i++)
            {
                float scale = 1000000000;
                if (i > 1) {
                    scale = (waterSpline.GetPosition(i - 1) - waterSpline.GetPosition(i)).magnitude;
                }
                if (i < WavesCount + 2)
                {
                    if ((waterSpline.GetPosition(i + 1) - waterSpline.GetPosition(i)).magnitude < scale) {
                        scale = (waterSpline.GetPosition(i + 1) - waterSpline.GetPosition(i)).magnitude;
                    }
                    
                }
                waterSpline.SetPosition(i, new Vector3(waterSpline.GetPosition(i).x, springStack.springs[i - 1].transform.position.y + 1, waterSpline.GetPosition(i).z));
                if (i > 1) {
                    Vector3 leftT = (waterSpline.GetPosition(i - 1) - waterSpline.GetPosition(i)).normalized;
                    //waterSpline.SetLeftTangent(i, leftT * scale * 0.33f);
                }
                if (i < WavesCount + 2)
                {
                    Vector3 rightT = (waterSpline.GetPosition(i + 1) - waterSpline.GetPosition(i)).normalized;
                    //waterSpline.SetRightTangent(i, rightT * scale * 0.33f);
                }
            }
        }
        
    }

    private void Update()
    {
        if (GameManager.state == GameState.Game) {
            if (_cam.transform.position.x - transform.position.x >= xMin) {
                transform.position = new Vector3(transform.position.x + xRespawn * 2, transform.position.y, transform.position.z);
            }
            /*
            if (playerController.transform.position.y > 0.1f) {
                //transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(_cam.transform.position.x, transform.localPosition.y, transform.localPosition.z), 0.05f);
            }
            if (_cam.transform.position.x - transform.position.x >= xMin) {
                wavesFar = true;
            }
            if (springStack.canMoveWater == true || wavesFar == true) {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(_cam.transform.position.x + 5, transform.localPosition.y, transform.localPosition.z), 0.1f);
                //transform.localPosition = new Vector3(_cam.transform.position.x + 5, transform.localPosition.y, transform.localPosition.z);
                if (transform.position.x - _cam.transform.position.x >= xMin - 1)
                {
                    wavesFar = false;
                }
            }*/
            if (secondWave == false) {
                for (int i = 1; i < WavesCount + 3; i++)
                {
                    springStack.springs[i - 1].transform.position = new Vector3(waterSpline.GetPosition(i).x * 15 + transform.position.x, springStack.springs[i - 1].transform.position.y, 0);
                }
            }
            
        }
    }

    public void SetWaves() {
        int waterPointsCount = waterSpline.GetPointCount();
        for (int i = CornersCount; i < waterPointsCount - CornersCount; i++)
        {
            waterSpline.RemovePointAt(CornersCount);
        }

        Vector3 waterTopLeftCorner = waterSpline.GetPosition(1);
        Vector3 waterTopRightCorner = waterSpline.GetPosition(2);
        float waterWidth = waterTopRightCorner.x - waterTopLeftCorner.x;

        float spacingPerWave = waterWidth / (WavesCount + 1);
        for (int i = WavesCount; i > 0; i--)
        {
            int index = CornersCount;

            float xPosition = waterTopLeftCorner.x + (spacingPerWave * i);
            Vector3 wavePoint = new Vector3(xPosition, waterTopLeftCorner.y, waterTopLeftCorner.z);
            waterSpline.InsertPointAt(index, wavePoint);
            waterSpline.SetHeight(index, 0.1f);
            waterSpline.SetCorner(index, false);
            waterSpline.SetTangentMode(index, ShapeTangentMode.Continuous);

        }
    }

}
