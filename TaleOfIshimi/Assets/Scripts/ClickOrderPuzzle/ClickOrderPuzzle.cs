using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ClickOrderPuzzle : MonoBehaviour
{
    [SerializeField] int[] answer = {1,2,0};
    int currOrder = 0;
    int[] clickInput = new int[3];
    [SerializeField] ClickOrderObj[] fire;

    [SerializeField] GameObject[] deactivateItems;
    [SerializeField] GameObject[] activateItems;

    void PutOutFire()  {
        for(int i = 0; i < 3; i++) {
            fire[i].PutOutFire();
        }
    }
    public void SetPW(int id){
        clickInput[currOrder] = id;
        currOrder++;
        if(currOrder==3){
            currOrder = 0;
            for(int i = 0; i<3; i++){
                if(clickInput[i]!=answer[i]){
                    Invoke("PutOutFire",0.2f);
                    return;
                }
            }
            ClearFunc();
        }
    }

    void ClearFunc(){
        for(int i = 0; i<deactivateItems.Length; i++){
            deactivateItems[i].gameObject.SetActive(false);
        }
        for(int j = 0; j<activateItems.Length; j++){
            activateItems[j].gameObject.SetActive(true);
        }
    }
    
}
