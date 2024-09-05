using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class DamageObject : DisappearObject
{
    private float damage = 0.25f;
    private bool used = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    public void DamageShark(Collider2D collision) {
        
        if (GameManager.state == GameState.Game)
        {
            if (collision.name.StartsWith("Shark") == true && used == false)
            {
                
                used = true;
                GameManager.GetDamage(damage);
                DisappearStart();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DamageShark(collision);
    }
}
