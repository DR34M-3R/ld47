using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAction : BaseUsableAction
{
    public GameObject openned;
    private bool isOpen;

    public override void Run()
    {
        if (!isOpen)
        {
            isOpen = Inventory.Instance.RemoveItem(ItemType.safeCode);
            if (isOpen)
            {
                SubtitlesController.Instance.Show(Loc.Get("safe_opened"));
                //Add part of clock, show opened safe
            }
            else
            {
                SubtitlesController.Instance.Show(Loc.Get("safe_closed"));
            }
        }
    }
}
