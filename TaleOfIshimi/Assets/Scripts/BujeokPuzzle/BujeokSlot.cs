using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BujeokSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int slotNum = -1;
    [SerializeField] BujeokObj child;

    public void OnPointerClick(PointerEventData eventData)
    {
        int invenTarget = Inventory.imanager.GetTarget();
        if(invenTarget<0){
            return;
        }
        Item invenItem = Inventory.imanager.GetSlotItem(invenTarget);
        if(invenItem.getId()<4){
            child.gameObject.SetActive(true);
            child.SetBuNum(invenItem.getId());
            Inventory.imanager.DeleteItem(invenTarget);
            Inventory.imanager.ResetTarget();
        }
    }

    // 부적 아이템의 아이템 아이디는 0-3으로 한다 (변경 시 수정 필요)

}
