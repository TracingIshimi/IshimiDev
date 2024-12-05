using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpStage1 : SpFunc
{
    protected override void SpActions(){
        Inventory.imanager.AddItem(5);
    }

}
