using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearObject : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private bool spawnBubble = false;
    private bool started = false;
    [SerializeField] private float disapSpeed = 10f;
    [SerializeField] private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            if (started == true) {
                
                bool disFlag = false;
                for (int i = 0; i< spriteRenderers.Count; i++) {
                    spriteRenderers[i].color = new Color (spriteRenderers[i].color.r, spriteRenderers[i].color.g, spriteRenderers[i].color.b, spriteRenderers[i].color.a - disapSpeed * Time.deltaTime);
                    if (spriteRenderers[i].color.a > 0) {
                        disFlag = true;
                    }
                }
                if (disFlag == false) {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void DisappearStart() {
        if (started == false)
        {
            started = true;
            if (spawnBubble == true)
            {
                Instantiate(bubble, transform.position, new Quaternion());
            }
        }
        
    }

    
}
