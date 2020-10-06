using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DoorAction : BaseUsableAction
{

    public GameObject objectToHide;
    public GameObject objectToShow;
    public Transform teleportationPoint;
    public bool isMainRoom;
    public override void Run()
    {
        if(objectToHide != null)
        {
            objectToHide.SetActive(false);
        }

        if(objectToShow != null)
        {
            objectToShow.SetActive(true);
        }

        if (teleportationPoint != null)
        {
            //if(isMainRoom)
            CharacterController.Instance.transform.position = new Vector3(teleportationPoint.position.x, teleportationPoint.position.y, 0); //weird stuff
        }
    }
}
