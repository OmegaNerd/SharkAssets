using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObject : DisappearObject
{
    public bool used = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    public virtual void Die() { 
        DisappearStart();
    }
}
