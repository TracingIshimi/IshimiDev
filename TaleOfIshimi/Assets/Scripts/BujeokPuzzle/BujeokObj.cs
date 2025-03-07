using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BujeokObj : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] BujeokPuzzle parent;
    [SerializeField] int slotNum = -1;
    //SpriteRenderer spriteRenderer;
    int buNum=-1;

    private void Start(){
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetBuNum(int num){
        buNum = num;
        parent.SetPW(slotNum,num);
        this.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(parent.GetPath()+'/'+buNum.ToString());

    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Inventory.imanager.AddItem(buNum);
        buNum = -1;
        parent.SetPW(slotNum,-1);
        gameObject.SetActive(false);
    }
}
