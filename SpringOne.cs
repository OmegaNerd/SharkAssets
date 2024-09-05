using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringOne : MonoBehaviour
{
    private float startPosY;
    public float dampingLen;
    public float dampingLenMax;
    [SerializeField] private float dampingLenMin;
    [SerializeField] private float dampSpeed;
    public bool dampingUp = false;
    [SerializeField] private float dampMinus;
    public bool showCircle = false;
    [SerializeField] GameObject circle;
    // Start is called before the first frame update
    void Start()
    {
        startPosY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            circle.SetActive(showCircle);
            if (dampingLen < dampingLenMin)
            {
                dampingLen = dampingLenMin;
                dampingLenMax = 0.075f;
            }
            if (dampingLen > 0)
            {
                if (dampingUp == true)
                {
                    transform.position += new Vector3(0, dampSpeed * (dampingLen / dampingLenMax), 0) * Time.deltaTime;
                    if (transform.localPosition.y >= startPosY + dampingLen)
                    {
                        dampingUp = false;
                        dampingLen -= dampMinus;

                    }
                }
                else
                {
                    transform.position += new Vector3(0, -dampSpeed * (dampingLen / dampingLenMax), 0) * Time.deltaTime;
                    if (transform.localPosition.y <= startPosY - dampingLen)
                    {
                        dampingUp = true;
                        dampingLen -= dampMinus;
                    }
                }
                
            }
            else {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, startPosY, transform.localPosition.z), 0.05f) ;
            }
        }
    }

    public void StartDamp(bool isUp, float dampLen) {
        dampingUp = isUp;
        dampingLen = dampLen;
    }
}
