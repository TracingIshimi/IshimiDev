using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;

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
    [SerializeField] TextMeshProUGUI[] buttonTexts = new TextMeshProUGUI[4];

    private int story_max = 0;
    private CharData charData = new CharData();
    private StoryObject storyObject;
    private Dictionary<int,ChoiceScript> choiceScripts = new Dictionary<int, ChoiceScript>();
    private int currIdx;
    private SingleScript currScript;

    private void Start(){
        InitConvDB();
        InitConvSystem("TestConvData");
    }

    private void InitConvDB(){
        DBManager.dbManager.OpenDBConnection();
        List<int> charNumList = new List<int>();

        // 스크립트 라인 수 가져오기
        IDbCommand stageCommand = DBManager.dbManager.dbConnection.CreateCommand();
        //dbCommand.CommandText = "SELECT * FROM "+Const.STAGE_TABLE+" WHERE stage_id = "+StageManager.stageManager.GetStageNum().ToString();
        stageCommand.CommandText = "SELECT * FROM "+Const.STAGE_TABLE+" WHERE stage_id = 0";
        IDataReader stageReader = stageCommand.ExecuteReader();
        while(stageReader.Read()){
            story_max = stageReader.GetInt32(Const.STAGE_ATTRIBUTE["story_max"]);
        }
        storyObject = new StoryObject(story_max);
        stageReader.Close();
        stageCommand.Dispose();
        
        // 스토리 스크립트 가져오기
        //dbCommand.CommandText = "SELECT * FROM " + Const.STORY_TABLE+" WHERE stage_id = "+StageManager.stageManager.GetStageNum().ToString();
        IDbCommand scriptCommand = DBManager.dbManager.dbConnection.CreateCommand();
        scriptCommand.CommandText = "SELECT * FROM " + Const.STORY_TABLE+" WHERE stage_id = 0";
        IDataReader dataReader = scriptCommand.ExecuteReader();

        while(dataReader.Read()){
            int scriptId = dataReader.GetInt32(Const.STORY_ATTRIBUTE["script_id"]);
            ScriptType type = (ScriptType)dataReader.GetInt32(Const.STORY_ATTRIBUTE["type"]);
            int bgimgNum = dataReader.GetInt32(Const.STORY_ATTRIBUTE["bgimg_num"]);
            int charId = dataReader.GetInt32(Const.STORY_ATTRIBUTE["character_id"]);
            int charImgNum = dataReader.GetInt32(Const.STORY_ATTRIBUTE["charimg_num"]);
            string content = dataReader.GetString(Const.STORY_ATTRIBUTE["content"]);
            int nextGoto = dataReader.GetInt32(Const.STORY_ATTRIBUTE["next_goto"]);

            // 해당 스토리에 필요한 캐릭터 id 저장
            if(!charNumList.Contains(charId)){
                charNumList.Add(charId);
            }

            // 스크립트 객체 생성, append
            storyObject.SetScript(scriptId,new SingleScript(type,bgimgNum,charId,charImgNum,content,nextGoto));
            Debug.Log(":: "+scriptId+" :: Content>> "+storyObject.GetScript(scriptId).GetContent());
        }
        dataReader.Close();
        scriptCommand.Dispose();

        // 선택지 스크립트 가져오기
        //choiceCommand.CommandText = "SELECT * FROM "+Const.CHOICE_TABLE+"WHERE stage_id = "+StageManager.stageManager.GetStageNum().ToString();
        IDbCommand choiceCommand = DBManager.dbManager.dbConnection.CreateCommand();
        choiceCommand.CommandText = "SELECT * FROM "+Const.CHOICE_TABLE+"WHERE stage_id = 0";
        IDataReader choiceReader = scriptCommand.ExecuteReader();

        while(choiceReader.Read()){
            int scriptId = choiceReader.GetInt32(Const.CHOICE_ATTRIBUTE["script_id"]);
            int choiceMax = choiceReader.GetInt32(Const.CHOICE_ATTRIBUTE["choice_max"]);
            int timer = choiceReader.GetInt32(Const.CHOICE_ATTRIBUTE["timer"]);

            int choiceAttNum = Const.CHOICE_ATTRIBUTE["choice1"];
            int mouseAttNum = Const.CHOICE_ATTRIBUTE["mouse1"];
            int gotoAttNum = Const.CHOICE_ATTRIBUTE["goto1"];
            string[] choice = new string[4];
            string[] mouseover = new string[4];
            int[] gotoNum = new int[5];
            for(int i = 0; i<4; i++){
                if(choiceReader.GetString(choiceAttNum+i) != null){
                    choice[i] = choiceReader.GetString(choiceAttNum+i);
                }
                else{
                    choice[i] = "";
                }
                if(choiceReader.GetString(mouseAttNum+i) != null){
                    mouseover[i] = choiceReader.GetString(mouseAttNum+i);
                }
                else{
                    mouseover[i] = "";
                }
                gotoNum[i] = choiceReader.GetInt32(gotoAttNum+i);
            }
            gotoNum[4] = choiceReader.GetInt32(gotoAttNum+4);
            
            choiceScripts.Add(scriptId,new ChoiceScript(choiceMax,timer,choice,mouseover,gotoNum));
        }
        choiceReader.Close();
        choiceCommand.Dispose();

        // 캐릭터 정보 가져오기
        IDbCommand characCommand = DBManager.dbManager.dbConnection.CreateCommand();
        characCommand.CommandText = "SELECT * FROM "+Const.CHAR_TABLE;
        IDataReader characReader = characCommand.ExecuteReader();

        while(characReader.Read()){
            int id = characReader.GetInt32(Const.CHAR_ATTRIBUTE["id"]);
            if(charNumList.Contains(id)){
                string name = characReader.GetString(Const.CHAR_ATTRIBUTE["name"]);
                string spriteBase = characReader.GetString(Const.CHAR_ATTRIBUTE["sprite_base"]);
                int spriteMax = characReader.GetInt32(Const.CHAR_ATTRIBUTE["sprite_max"]);
                charData.AddCharacter(id, new Character(name, spriteBase, spriteMax));
            }
        }
        characReader.Close();
        characCommand.Dispose();
        
        DBManager.dbManager.CloseDBConnection();
    }


    public void InitConvSystem(string fileName){
        convWin.SetActive(true);
        currIdx = 0;
        SetConv(currIdx);
    }

    public void NextButton(){
        if(currScript.GetScriptType() != ScriptType.CHOICE && currScript.GetNextGoto() >= 0){
            currIdx = currScript.GetNextGoto();
        }
        else{
            currIdx++;
        }
        if(currIdx<storyObject.GetScriptLen()){
            SetConv(currIdx);
        }
        else{
            Debug.Log("EOF: Json end");
        }
        
    }

    public void ChoiceButton(int choiceIdx){
        SetConv(choiceScripts[currIdx].GetChoiceGoto(choiceIdx));
    }
    
    void SetConv(int n){
        convWin.SetActive(true);
        nameWinNPC.SetActive(false);
        nameWinNPC.SetActive(false);
        choiceWin.SetActive(false);
        charImageNPC.gameObject.SetActive(false);
        charImagePC.gameObject.SetActive(false);

        currScript = storyObject.GetScript(n);
        bgImage.sprite = Resources.Load<Sprite>(Const.BGIMG_PATH_BASE +currScript.GetBgimgNum().ToString());
        
            switch(currScript.GetScriptType()){
                case ScriptType.NARR:
                    Debug.Log(":: SetNarr() Call");
                    SetNarr();
                    break;
                case ScriptType.CHAR:
                    Debug.Log(":: SetCharDialogue() Call");
                    SetCharDialogue();
                    break;
                case ScriptType.CHOICE:
                    Debug.Log(":: SetChoice() Call");
                    SetChoice();
                    break;
                default:
                    Debug.Log("ERROR: "+n+"th script");
                    break;
            }
    }

    void SetNarr(){
        Debug.Log(currScript.GetContent());
        convText.text = currScript.GetContent();
    }

    void SetCharDialogue(){
        int charNum = currScript.GetCharNum();
        if(charNum==0){
            nameWinPC.SetActive(true);
            charImagePC.gameObject.SetActive(true);
            nameTextPC.text = charData.GetCharacter(charNum).GetName();
            charImagePC.sprite = Resources.Load<Sprite>(charData.GetCharacter(charNum).GetSpriteAddress()+currScript.GetSpriteNum().ToString());
        }
        else{
            nameWinNPC.SetActive(true);
            charImageNPC.gameObject.SetActive(true);
            nameTextNPC.text = charData.GetCharacter(charNum).GetName();
            charImageNPC.sprite = Resources.Load<Sprite>(charData.GetCharacter(charNum).GetSpriteAddress()+currScript.GetSpriteNum().ToString());
        }
        convText.text = currScript.GetContent();
    }

    void SetChoice(){
        choiceWin.SetActive(true);
        for(int i =0; i<choiceScripts[currIdx].GetChoiceMax(); i++){
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = choiceScripts[currIdx].GetChoice(i);
            buttonTexts[i].text = choiceScripts[currIdx].GetMouseover(i);
        }
    }
}
