using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BujeokPuzzle : MonoBehaviour
{
    [SerializeField] GameObject spManager;
    [SerializeField] string spritePath;
    int[] pw = new int[4];
    string answer="0123";

    void Start(){
        for(int i = 0; i<pw.Length; i++){
            pw[i]=-1;
        }
    }

    public void SetPW(int slot, int id){
        pw[slot] = id;
        string pwString="";
        for(int i = 0; i<pw.Length; i++){
            pwString+=pw[i].ToString();
        }
        Debug.Log(pwString);
        if(pwString==answer){
            spManager.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public string GetPath(){
        return spritePath;
    }
}
