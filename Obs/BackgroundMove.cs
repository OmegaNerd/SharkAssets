using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private float xMin;
    [SerializeField] private float xRespawn;
    [SerializeField] private float paralaxMultiply;
    private Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            transform.position += new Vector3(-GameManager.sharkMoveSpeed * paralaxMultiply, 0, 0) * Time.deltaTime;
            if (_cam.transform.position.x - transform.position.x >= xMin) {
                transform.position = new Vector3(transform.position.x + xRespawn + xMin, transform.position.y, transform.position.z);
            }
        }
    }
}
