using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorySystem : MonoBehaviour
{
    [SerializeField] GameObject convWin;
    [SerializeField] GameObject nameWinNPC;
    [SerializeField] GameObject nameWinPC;
    [SerializeField] Image charImageNPC;
    [SerializeField] Image charImagePC;
    [SerializeField] Image bgImage;
    [SerializeField] TextMeshProUGUI convText;
    [SerializeField] TextMeshProUGUI nameTextNPC;
    [SerializeField] TextMeshProUGUI nameTextPC;
    [SerializeField] GameObject choiceWin;
    [SerializeField] Button[] buttons = new Button[4];

    private StoryObject storyObject;
    private SingleScript singleScript;
    private int idx;
    private CharData charData = new CharData();

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
        CHAR,
        CHOICE
    }
    class SingleScript{
        ScriptType type;
        int charNum;
        int spriteNum;
        int bgimgNum;
        string content;
        int choiceNum;  // 갯수: 최저1 최대4
        string[] choice = new string[4];
        int[] choiceGoto = new int[4];
        int nextGoto;

        // Getter
        public ScriptType GetScriptType(){
            return type;
        }
        public string GetContent(){
            return content;
        }
        public int GetCharNum(){
            return charNum;
        }
        public int GetSpriteNum(){
            return spriteNum;
        }
        public int GetBgimgNum(){
            return bgimgNum;
        }
        public int GetChoiceNum(){
            return choiceNum;
        }
        public string GetChoiceContent(int idx){
            return choice[idx];
        }
        public int GetChoiceGoto(int idx){
            return choiceGoto[idx];
        }
        public int GetNextGoto(){
            return nextGoto;
        }
    }
    public void InitConvSystem(string fileName){
        string filePath = Const.STORY_PATH_BASE+fileName+".json";
        string jsonStr = File.ReadAllText(filePath);
        storyObject = JsonUtility.FromJson<StoryObject>(jsonStr);
        convWin.SetActive(true);
        idx = 0;
        SetConv(idx);
    }

    public void NextButton(){
        if(singleScript.GetScriptType() != ScriptType.CHOICE && singleScript.GetNextGoto() >= 0){
            idx = singleScript.GetNextGoto();
        }
        else{
            idx++;
        }
        SetConv(idx);
    }

    public void ChoiceButton(int idx){
        SetConv(singleScript.GetChoiceGoto(idx));
    }
    
    void SetConv(int n){
        convWin.SetActive(true);
        nameWinNPC.SetActive(false);
        nameWinNPC.SetActive(false);
        choiceWin.SetActive(false);

        singleScript = storyObject.GetScript(n);
        bgImage.sprite = Resources.Load<Sprite>(Const.BGIMG_PATH_BASE +singleScript.GetBgimgNum().ToString());
        
            switch(singleScript.GetScriptType()){
                case ScriptType.NARR:
                    SetNarr();
                    break;
                case ScriptType.CHAR:
                    SetCharDialogue();
                    break;
                case ScriptType.CHOICE:
                    SetChoice();
                    break;
                default:
                    Debug.Log("ERROR: "+n+"th script");
                    break;
            }
    }

    void SetNarr(){
        convText.text = singleScript.GetContent();
    }

    void SetCharDialogue(){
        int charNum = singleScript.GetCharNum();
        if(charNum==0){
            nameWinPC.SetActive(true);
            nameTextPC.text = charData.characterArray[charNum].GetName();
            charImagePC.sprite = Resources.Load<Sprite>(charData.characterArray[charNum].GetSpriteAddress()+singleScript.GetSpriteNum().ToString());
        }
        else{
            nameWinNPC.SetActive(true);
            nameTextNPC.text = charData.characterArray[charNum].GetName();
            charImageNPC.sprite = Resources.Load<Sprite>(charData.characterArray[charNum].GetSpriteAddress()+singleScript.GetSpriteNum().ToString());
        }
        convText.text = singleScript.GetContent();
    }

    void SetChoice(){
        choiceWin.SetActive(true);
        for(int i =0; i<singleScript.GetChoiceNum(); i++){
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = singleScript.GetChoiceContent(i);
        }
    }
    
}
