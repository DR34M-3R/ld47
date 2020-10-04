using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerAction : MonoBehaviour
{
    public float time = 0.0f;
    public float timeLimit = 0.0f;
    
    protected virtual void Run()
    {
        
    }
    
    private void Update()
    {
        time += Time.deltaTime;

        if (time >= timeLimit) 
        {
            Run();
            Destroy(this);
        }
    }
}
