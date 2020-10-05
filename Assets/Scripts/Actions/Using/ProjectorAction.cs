using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorAction : BaseUsableAction
{
    public GameObject ray;
    private bool hasBattery;
    private bool hasLens;
    private bool isActive;

    public override void Run()
    {
        if (isActive)
        {
            return;
        }
        
        hasBattery = hasBattery || Inventory.Instance.RemoveItem(ItemType.battery);
        hasLens = hasLens || Inventory.Instance.RemoveItem(ItemType.lens);

        if (hasBattery && hasLens)
        {
            TurnOnProjector();
            SubtitlesController.Instance.Show(Loc.Get("projector_turned_on"));
        }
        else if (hasBattery)
        {
            SubtitlesController.Instance.Show(Loc.Get("projector_needs_lens"));
        }
        else if (hasLens)
        {
            SubtitlesController.Instance.Show(Loc.Get("projector_needs_battery"));
        }
        else
        {
            SubtitlesController.Instance.Show(Loc.Get("projector_needs_battery_and_lens"));
        }
    }

    private void TurnOnProjector()
    {
        isActive = true;
        ray.active = true;
        Inventory.Instance.AddItem(ItemType.safeCode);
    }
}
