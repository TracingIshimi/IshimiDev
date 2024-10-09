using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWDPrefab : MonoBehaviour
{
    PWDPuzzle parent;
    int dig;
    int maxIdx;
    int index = 0;
    public SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sprite;
    public void InitPrefab(int dig, int idx, PWDPuzzle parent){
        Debug.Log("InitPrefab Call: "+dig);
        this.dig = dig;
        maxIdx = idx;
        this.parent = parent;
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSpriteField();
    }
    void SetSpriteField(){
        spriteRenderer.sprite = Resources.Load<Sprite>(parent.GetPath()+'/'+index.ToString());
        parent.SetCurrPWD(dig,index);
    }

    public void IdxUp(){
        index++;
        index%=maxIdx;
        SetSpriteField();
    }
    public void IdxDown(){
        if(index == 0){
            index = maxIdx-1;
            SetSpriteField();
            return;
        }
        index--;
        index%=maxIdx;
        SetSpriteField();
    }
}
