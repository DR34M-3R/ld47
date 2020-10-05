using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DoorAction : BaseUsableAction
{

    public GameObject objectToHide;
    public GameObject objectToShow;
    public Transform teleportationPoint;
    public override void Run()
    {
        if (objectToHide != null)
        {
            objectToHide.active = false;
        }

        if (objectToShow != null)
        {
            objectToShow.active = true;
        }

        if (teleportationPoint != null)
        {
            CharacterController.Instance.transform.position = new Vector3(teleportationPoint.position.x, teleportationPoint.position.y, 0); //weird stuff
        }
    }
}
