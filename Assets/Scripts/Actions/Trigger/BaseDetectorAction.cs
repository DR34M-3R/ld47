using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDetectorAction : MonoBehaviour
{
    public LayerMask layerMask;
    
    protected virtual void Begin()
    {
        Debug.Log("Begin");
    }
        
    protected virtual void End()
    {
        Debug.Log("End");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if((layerMask.value & (1 << other.gameObject.layer)) != 0)
        {
            Begin();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if((layerMask.value & (1 << other.gameObject.layer)) != 0)
        {
            End();
        }
    }
}
