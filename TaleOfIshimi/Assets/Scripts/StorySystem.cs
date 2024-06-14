using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class StorySystem : MonoBehaviour
{
    [SerializeField] GameObject convWin;
    [SerializeField] GameObject nameWin;
    [SerializeField] TextMeshProUGUI convText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject choiceWin;
    [SerializeField] Button[] buttons = new Button[4];

    private StoryObject storyObject;
    private int idx;

    class StoryObject{
        SingleScript[] scripts;
        public SingleScript GetScript(int n){
            return scripts[n];
        }
        public int GetScriptLen(){
            return scripts.Length;
        }
    }
    enum ScriptType{
        NARR, 
        NPC,
        PC,
        CHOICE
    }
    class SingleScript{
        ScriptType type;
        int charNum;
        int spriteNum;
        string content;
        int choiceNum;
        string[] choice = new string[4];
        public ScriptType GetScriptType(){
            return type;
        }
        public string GetContent(){
            return content;
        }
        public int GetCharNum(){
            return charNum;
        }
    }
    public void ConvSystem(string fileName){
        string filePath = Const.STORY_PATH_BASE+fileName+".json";
        string jsonStr = File.ReadAllText(filePath);
        storyObject = JsonUtility.FromJson<StoryObject>(jsonStr);
        convWin.SetActive(true);
        idx = 0;
        SetConv(idx);
    }

    public void NextButton(){
        idx++;
        SetConv(idx);
    }
    
    void SetConv(int n){
        SingleScript singleScript = storyObject.GetScript(n);
            switch(singleScript.GetScriptType()){
                case ScriptType.NARR:
                    SetNarr(n);
                    break;
                case ScriptType.NPC:
                    SetNpc(n);
                    break;
                case ScriptType.PC:
                    SetPc(n);
                    break;
                case ScriptType.CHOICE:
                    SetChoice(n);
                    break;
                default:
                    Debug.Log("ERROR: "+n+"th script");
                    break;
            }
    }

    void SetNarr(int n){
        nameWin.SetActive(false);
        convWin.SetActive(true);
        convText.text = storyObject.GetScript(n).GetContent();
    }

    void SetNpc(int n){
        nameWin.SetActive(true);
        convWin.SetActive(true);
        nameText.text = CharData.characterList[n].GetName();
    }

    void SetPc(int n){

    }

    void SetChoice(int n){

    }
    
}
