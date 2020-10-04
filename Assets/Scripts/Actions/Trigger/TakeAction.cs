using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAction : BaseUsableAction
{
    public ItemType itemType;
    
    public override void Run()
    {
        Inventory.Instance.AddItem(itemType);
        Destroy(gameObject);
    }
}
