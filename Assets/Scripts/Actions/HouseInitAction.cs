using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInitAction : MonoBehaviour
{
    public GameObject facade;
    public GameObject guts;

    private void Awake()
    {
        guts.active = false;
        facade.active = true;
    }
}