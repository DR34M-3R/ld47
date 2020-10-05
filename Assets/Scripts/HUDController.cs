using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    
    void Update()
    {
                
        if (Input.GetKeyUp(KeyCode.T))
        {
            SubtitlesController.Instance.Show(Loc.Get("projector_needs_battery_and_lens"));
        }
    }
}
