using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighthouseToggleAction : BaseUsableAction
{
    public GameObject opened;
    private bool isOpen;

    public override void Run()
    {
        if (!isOpen)
        {
            isOpen = Inventory.Instance.RemoveItem(ItemType.safeCode);
            if (isOpen)
            {
                SubtitlesController.Instance.Show(Loc.Get("safe_opened"));
                opened.active = true;
            }
            else
            {
                SubtitlesController.Instance.Show(Loc.Get("safe_closed"));
            }
        }
    }
}
