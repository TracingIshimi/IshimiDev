using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] Image bgImage;
    [SerializeField] Image itemImage;

    int slotId;
    bool isEmpty = true;
    bool selected = false;
    Item item;
    public void SetSlot(Item item){
        isEmpty = false;
        this.item = item;
        itemImage.gameObject.SetActive(true);
        // 아이템 스프라이트 바꾸는 코드 필요 
    }
    public void ClearSlot(){
        isEmpty = true;
        item = null;
        itemImage.gameObject.SetActive(false);
    }

    public void SelectSlot(){
        bgImage.color = new Color(1f,1f,1f,1f);
        selected = true;
        Inventory.imanager.SetItemText(item.getName());
    }

    public void DeselectSlot(){
        bgImage.color = new Color(1f,1f,1f,0.5f);
        selected = false;
        Inventory.imanager.SetItemTextTarget();
    }

    // Mouse Event
    public void OnPointerClick(PointerEventData eventData){
        Inventory.imanager.ClickSlot(slotId);
    }

    public void OnPointerEnter(PointerEventData eventData){
        if(selected){
            return;
        }
        if(!isEmpty){
            Inventory.imanager.SetItemText(item.getName());
            bgImage.color = new Color(1f,1f,1f,0.7f);
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        if(!selected){
            Inventory.imanager.SetItemTextTarget();
            DeselectSlot();
        }
    }

    // Setter
    public void SetSlotID(int id){
        slotId = id;
    }

    // Getter
    public bool SlotEmpty(){
        return isEmpty;
    }
    public Item GetItem(){
        return item;
    }
}
