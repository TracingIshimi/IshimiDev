using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWDPuzzle : MonoBehaviour
{
    public int digits;
    public int maxidx;
    public string path = "Data/Puzzle/";
    public string answer;
    [SerializeField] Sprite[] sprites;
    public PWDPrefab prefab;
    PWDPrefab[] slots;
    int[] currPwd;
    void Start()
    {
        Debug.Log("Puzzle Prefab Initialize");
        currPwd = new int[digits];
        slots = new PWDPrefab[digits];
        float xPos = 0;
        for(int i = 0; i<digits; i++){
            currPwd[i] = 0;
            slots[i] = Instantiate(prefab);
            slots[i].transform.SetParent(this.transform);
            slots[i].transform.localPosition = new Vector3(xPos,0,0);
            slots[i].InitPrefab(i,maxidx,this);
            xPos+=1.3f;
        }
    }

    public string GetPath(){
        return path;
    }
    public void SetCurrPWD(int dig, int idx){
        if(dig>=currPwd.Length){
            return;
        }
        currPwd[dig] = idx;
        string pwString ="";
        for(int i = 0; i<digits; i++){
            pwString+=currPwd[i].ToString();
        }
        if(pwString == answer.Trim()){
            Debug.Log("Password Correct: "+answer);
        }
        else{
            Debug.Log("Password Wrong: "+pwString+"\t Answer: "+answer);
        }
    }
}
