using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PWDPrefab : MonoBehaviour
{
    public bool isUI;
    PWDPuzzle parent;
    int dig;
    int maxIdx;
    int index = 0;
    public SpriteRenderer spriteRenderer;

    public void InitPrefab(int dig, int idx, PWDPuzzle parent){
        Debug.Log("InitPrefab Call: "+dig);
        this.dig = dig;
        maxIdx = idx;
        this.parent = parent;
        if(!isUI){
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        SetSpriteField();
    }
    void SetSpriteField(){
        if(isUI){
            GetComponent<Image>().sprite = Resources.Load<Sprite>(parent.GetPath()+'/'+index.ToString());
        }
        else{
            spriteRenderer.sprite = Resources.Load<Sprite>(parent.GetPath()+'/'+index.ToString());
        }
        parent.SetCurrPWD(dig,index);
    }

    public void SetIndex(int num){
        if(num<0 || num>=maxIdx){
            return;
        }
        index = num;
        SetSpriteField();
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
