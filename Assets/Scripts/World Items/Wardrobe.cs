﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : Interaction
{

    protected override void PerformAction()
    {
        EventManager.Instance.ItemPickUp(InventoryManager.PAPER_CLIP_KEY_GO_NAME);
    }

    protected override bool CanInteract()
    {
        return base.CanInteract();
    }
}
