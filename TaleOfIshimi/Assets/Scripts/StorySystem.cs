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

    
    private IDbConnection dbConnection;
    private int story_max = 0;
    private StoryObject storyObject;
    private List<ChoiceScript> choiceScripts = new List<ChoiceScript>();

    class StoryObject{
        public SingleScript[] scripts;
        public StoryObject(int num){
            scripts = new SingleScript[num];
        }
        public void SetScript(int n, SingleScript singleObj){
            scripts[n] = singleObj;
        }
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
        int nextGoto;

        // 생성자
        public SingleScript(ScriptType type, int bgimgNum, int charNum, int charImgNum, string content, int nextGoto){
            this.type = type;
            this.bgimgNum = bgimgNum;
            this.charNum = charNum;
            spriteNum = charImgNum;
            this.content = content;
            this.nextGoto = nextGoto;
        }

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
        public int GetNextGoto(){
            return nextGoto;
        }
    }

    class ChoiceScript{
        int scriptNum;
        int choiceMax;
        int timer;
        string[] choice = new string[4];
        int[] choiceGoto = new int[5];

    }



    private void InitConvDB(){
        OpenDBConnection();
        IDbCommand stageCommand = dbConnection.CreateCommand();
        // 캐릭터 데이터 가져오기

        // 스크립트 라인 수 가져오기
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
        IDbCommand scriptCommand = dbConnection.CreateCommand();
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
            storyObject.SetScript(scriptId,new SingleScript(type,bgimgNum,charId,charImgNum,content,nextGoto));
            Debug.Log(scriptId+" | Content>> "+storyObject.GetScript(scriptId).GetContent());
        }
        dataReader.Close();
        scriptCommand.Dispose();

        // 선택지 스크립트 가져오기
        
        CloseDBConnection();
    }

    // DB 관련 코드 리팩토링 - 코드 파일 분리 필요
    private void OpenDBConnection(){
        string connectionString = "URI=file:"+Application.streamingAssetsPath+Const.DB_NAME;
        dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();
    }

    private void CloseDBConnection(){
        dbConnection.Close();
    }
}
