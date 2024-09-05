using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarDestroyer : MonoBehaviour
{
    [SerializeField] private float distanceToDestroy = 25f;
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
            if (_cam.transform.position.x - transform.position.x >= distanceToDestroy) {
                Destroy(this.gameObject);
            }
        }
    }
}
