using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

public class StorySystem : MonoBehaviour
{
    [SerializeField] GameObject convWin;
    [SerializeField] GameObject nameWinNPC;
    [SerializeField] GameObject nameWinPC;
    [SerializeField] Image charImageNPC;
    [SerializeField] Image charImagePC;
    [SerializeField] Image bgImage;
    [SerializeField] Image modalImage;
    [SerializeField] TextMeshProUGUI convText;
    [SerializeField] TextMeshProUGUI nameTextNPC;
    [SerializeField] TextMeshProUGUI nameTextPC;
    [SerializeField] GameObject choiceWin;
    [SerializeField] Button[] buttons = new Button[4];
    [SerializeField] Slider timerUi;
    

    private int storyMax = 0;
    private CharData charData = new CharData();
    private StoryObject storyObject;
    private Dictionary<int,ChoiceScript> choiceScripts = new Dictionary<int, ChoiceScript>();
    private int currIdx;
    private SingleScript currScript;

    // Text Effect 관련 변수
    private bool is_texteff = false;
    private int effect_cnt;
    private string completeDialogue;

    // 타이머 변수
    private float sliderVal;
    private bool isTimer = false;
    private float maxTime;
    private float currTime;

    private void Start(){
        InitConvDB();
        InitConvSystem();
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
            storyMax = stageReader.GetInt32(Const.STAGE_ATTRIBUTE["story_max"]);
        }
        storyObject = new StoryObject(storyMax);
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
            int illustNum = dataReader.GetInt32(Const.STORY_ATTRIBUTE["illust_num"]);
            int charId = dataReader.GetInt32(Const.STORY_ATTRIBUTE["character_id"]);
            int charImgNum = dataReader.GetInt32(Const.STORY_ATTRIBUTE["charimg_num"]);
            string content = dataReader.GetString(Const.STORY_ATTRIBUTE["content"]);
            int nextGoto = dataReader.GetInt32(Const.STORY_ATTRIBUTE["next_goto"]);

            // 해당 스토리에 필요한 캐릭터 id 저장
            if(!charNumList.Contains(charId)){
                charNumList.Add(charId);
            }

            // 스크립트 객체 생성, append
            storyObject.SetScript(scriptId,new SingleScript(type,bgimgNum,illustNum,charId,charImgNum,content,nextGoto));
            Debug.Log(":: "+scriptId+" :: Content>> "+storyObject.GetScript(scriptId).GetContent());
        }
        dataReader.Close();
        scriptCommand.Dispose();

        // 선택지 스크립트 가져오기
        //choiceCommand.CommandText = "SELECT * FROM "+Const.CHOICE_TABLE+"WHERE stage_id = "+StageManager.stageManager.GetStageNum().ToString();
        IDbCommand choiceCommand = DBManager.dbManager.dbConnection.CreateCommand();
        choiceCommand.CommandText = "SELECT * FROM "+Const.CHOICE_TABLE+" WHERE stage_id = 0";
        IDataReader choiceReader = choiceCommand.ExecuteReader();

        while(choiceReader.Read()){
            int scriptId = choiceReader.GetInt32(Const.CHOICE_ATTRIBUTE["script_id"]);
            int choiceMax = choiceReader.GetInt32(Const.CHOICE_ATTRIBUTE["choice_max"]);
            int timer = choiceReader.GetInt32(Const.CHOICE_ATTRIBUTE["timer"]);

            int choiceAttNum = Const.CHOICE_ATTRIBUTE["choice1"];
            int gotoAttNum = Const.CHOICE_ATTRIBUTE["goto1"];
            string[] choice = new string[4];
            int[] gotoNum = new int[5];
            for(int i = 0; i<4; i++){
                if(!choiceReader.IsDBNull(choiceAttNum+i)){
                    choice[i] = choiceReader.GetString(choiceAttNum+i);
                }
                else{
                    choice[i] = "";
                }
                if(!choiceReader.IsDBNull(gotoAttNum+i)){
                    gotoNum[i] = choiceReader.GetInt32(gotoAttNum+i);
                }
                else{
                    gotoNum[i] = -1;
                }
                
            }
            if(!choiceReader.IsDBNull(gotoAttNum+4)){
                gotoNum[4] = choiceReader.GetInt32(gotoAttNum+4);
            }
            else{
                gotoNum[4] = -1;
            }
            
            choiceScripts.Add(scriptId,new ChoiceScript(choiceMax,timer,choice,gotoNum));
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


    public void InitConvSystem(){
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

    public void ChoiceButton(int choiceIdx){    // 외부에서 call 할 때도 이 함수 사용
        currIdx = choiceScripts[currIdx].GetChoiceGoto(choiceIdx);
        SetConv(currIdx);   
    }
    
    void SetConv(int n){
        convWin.SetActive(true);

        bgImage.color = Color.white;
        charImageNPC.color = Color.white;
        charImagePC.color = Color.white;

        choiceWin.SetActive(false);
        modalImage.gameObject.SetActive(false);
        nameWinNPC.SetActive(false);
        nameWinPC.SetActive(false);
        charImageNPC.gameObject.SetActive(false);
        charImagePC.gameObject.SetActive(false);
        convText.gameObject.SetActive(true);
        isTimer = false;

        ScriptType prevType = (currScript==null)?ScriptType.NARR:currScript.GetScriptType();

        currScript = storyObject.GetScript(n);
        if(prevType == ScriptType.CHAR && currScript.GetScriptType() == ScriptType.CHAR){
            charImageNPC.gameObject.SetActive(true);
            charImagePC.gameObject.SetActive(true);
            nameWinNPC.SetActive(true);
            nameWinPC.SetActive(true);
        }
        bgImage.sprite = Resources.Load<Sprite>(Const.BGIMG_PATH_BASE +currScript.GetBgimgNum().ToString());
        
            switch(currScript.GetScriptType()){
                case ScriptType.NARR:
                    Debug.Log(":: SetNarr() Call");
                    SetNarr();
                    break;
                case ScriptType.CHAR:
                    Debug.Log(":: SetCharDialogue() Call");
                    bgImage.color = Color.gray;
                    SetCharDialogue();
                    break;
                case ScriptType.CHOICE:
                    Debug.Log(":: SetChoice() Call");
                    SetChoice();
                    break;
                case ScriptType.ILLUST_FULL:
                    SetFullIllust();
                    break;
                case ScriptType.ILLUST_MODAL:
                    bgImage.color = Color.gray;
                    SetModalIllust();
                    break;
                case ScriptType.END_CONV:
                    EndConv();
                    break;
                default:
                    Debug.Log("TYPE ERROR: "+n+"th script");
                    break;
            }
    }

    void SetNarr(){
        Debug.Log(currScript.GetContent());
        TextEffectStart(currScript.GetContent());
    }

    void SetCharDialogue(){
        int charNum = currScript.GetCharNum();
        if(charNum==0){
            nameWinNPC.SetActive(false);
            nameWinPC.SetActive(true);
            charImagePC.gameObject.SetActive(true);
            charImageNPC.color = Color.gray;
            nameTextPC.text = charData.GetCharacter(charNum).GetName();
            charImagePC.sprite = Resources.Load<Sprite>(Const.CHARACTER_PATH_BASE+charData.GetCharacter(charNum).GetSpriteAddress()+"/"+currScript.GetSpriteNum().ToString());
        }
        else{
            nameWinPC.SetActive(false);
            nameWinNPC.SetActive(true);
            charImageNPC.gameObject.SetActive(true);
            charImagePC.color = Color.gray;
            nameTextNPC.text = charData.GetCharacter(charNum).GetName();
            charImageNPC.sprite = Resources.Load<Sprite>(Const.CHARACTER_PATH_BASE+charData.GetCharacter(charNum).GetSpriteAddress()+"/"+currScript.GetSpriteNum().ToString());
        }
        TextEffectStart(currScript.GetContent());
    }

    void SetChoice(){
        choiceWin.SetActive(true);
        if(choiceScripts[currIdx].GetTimer()>0){
            // 타이머 셋팅 코드 -> invoke 재귀함수 사용
            isTimer = true;
            maxTime = choiceScripts[currIdx].GetTimer();
            currTime = 0;
        }
        for(int i =0; i<choiceScripts[currIdx].GetChoiceMax(); i++){
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = choiceScripts[currIdx].GetChoice(i);
        }
        for(int j = choiceScripts[currIdx].GetChoiceMax(); j < 4; j++){
            buttons[j].gameObject.SetActive(false);
        }
        SetNarr();
    }

    void SetFullIllust(){
        convWin.SetActive(false);
        convText.gameObject.SetActive(true);
        bgImage.sprite = Resources.Load<Sprite>(Const.ILLUST_PATH_BASE +currScript.GetBgimgNum().ToString());
    }

    void SetModalIllust(){
        modalImage.gameObject.SetActive(true);
        modalImage.sprite = Resources.Load<Sprite>(Const.ILLUST_PATH_BASE +currScript.GetIllustNum().ToString());
        if(currScript.GetCharNum()>=0){
            SetCharDialogue();
        }
        else{
            SetNarr();
        }
    }

    void EndConv(){
        convWin.SetActive(false);
    }

    // Text Effect 관련 코드
    void TextEffectStart(string dialogue){
        completeDialogue = dialogue;
        convText.text = "";
        effect_cnt=0;
        is_texteff = true;
        Invoke("TextEffecting",1/Const.TEXT_EFF_SPEED);
    }
    void TextEffecting(){
        if(convText.text==completeDialogue){
            TextEffectEnd();
            return;
        }
        while(effect_cnt<completeDialogue.Length){
            convText.text += completeDialogue[effect_cnt];
            if(completeDialogue[effect_cnt]==' ' || completeDialogue[effect_cnt]=='\n'){
                effect_cnt++;
                break;
            }
            effect_cnt++;
        }
        Debug.Log("End Effecting");
        Invoke("TextEffecting",1/Const.TEXT_EFF_SPEED);
    }
    void TextEffectEnd(){
        is_texteff = false;
        convText.text = completeDialogue;
    }
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            if(is_texteff){
                CancelInvoke();
                TextEffectEnd();
            }

            else if(currScript.GetScriptType()!=ScriptType.CHOICE) {
                NextButton();
            }
        }
        if(isTimer){
            currTime+=Time.deltaTime;
            if(maxTime>=currTime){
                timerUi.value = 1-(currTime/maxTime);
            }
            else{
                ChoiceButton(Const.CHOICE_ATTRIBUTE["gotoNULL"]-Const.CHOICE_ATTRIBUTE["goto1"]);
            }
        }
    }
}
