using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighthouseToggleAction : BaseUsableAction
{
    public GameObject enabledSkin;
    public GameObject disabledSkin;
    public GameObject light;
    private bool isEnabled;

    public override void Run()
    {
        isEnabled = !isEnabled;

        light.active = isEnabled;
        enabledSkin.active = isEnabled;
        disabledSkin.active = !isEnabled;
    }
}
