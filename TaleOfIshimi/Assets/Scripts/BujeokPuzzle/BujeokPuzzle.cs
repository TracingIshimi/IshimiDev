using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BujeokPuzzle : MonoBehaviour
{
    [SerializeField] GameObject spManager;
    [SerializeField] string spritePath;
    int[] pw = {-1,-1,-1,-1};
    string answer="0123";

    public void SetPW(int slot, int id){
        pw[slot] = id;
        string pwString="";
        for(int i = 0; i<pw.Length; i++){
            pwString+=pw[i].ToString();
        }
        if(pwString==answer){
            spManager.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public string GetPath(){
        return spritePath;
    }
}
