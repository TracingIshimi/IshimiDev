using System.Collections.Generic;

public static class Const{

    public static int IDLE_SCREEN_WIDTH = 1920;
    public static int IDLE_SCREEN_HEIGHT = 1080;
    public static float SP_SCREEN_WIDTH = 2.39f;
    public static float SP_SCREEN_HEIGHT = 1;
    public static float SCREEN_EFF_SPEED = 200;  // 클수록 느려짐

    public static string STORY_PATH_BASE = "Assets/Resources/Data/Story/";
    public static string BGIMG_PATH_BASE = "Data/BgImg/";
    public static string ILLUST_PATH_BASE = "Data/Illust/";
    public static string CHARACTER_PATH_BASE = "Data/Character/";
    public static string ITEM_PATH_BASE = "Data/Item/";
    public static int CHARACTER_MAX_IDX = 0;

    // 인벤토리 슬롯 UI 수동조정 필요
    public static int ITEM_MAX_IDX = 10;
    public static int INVEN_MAX_IDX = 6;

    public static float TEXT_EFF_SPEED = 2;

    // DB 관련
    public static string DB_NAME = "/TaleOfIshimi.db";
    public static string STAGE_TABLE = "StageInfo";
    public static Dictionary<string,int> STAGE_ATTRIBUTE = new Dictionary<string,int>(){
        {"stage_id",0},
        {"stage_type",1},
        {"story_max",2},
        {"item_max",3}
    };
    public static string CHAR_TABLE = "Character";
    public static Dictionary<string,int> CHAR_ATTRIBUTE = new Dictionary<string,int>(){
        {"id",0},
        {"name",1},
        {"sprite_base",2},
        {"sprite_max",3}
    };
    public static string STORY_TABLE = "SingleScript";
    public static Dictionary<string,int> STORY_ATTRIBUTE = new Dictionary<string,int>()
    {
        {"stage_id",0},
        {"script_id",1},
        {"type",2},
        {"bgimg_num",3},
        {"illust_num",4},
        {"character_id",5},
        {"charimg_num",6},
        {"content",7},
        {"next_goto",8}
    };
    public static string CHOICE_TABLE = "Choice";
    public static Dictionary<string,int> CHOICE_ATTRIBUTE = new Dictionary<string,int>(){
        {"stage_id",0},
        {"script_id",1},
        {"choice_max",2},
        {"timer",3},
        {"choice1",4},
        {"choice2",5},
        {"choice3",6},
        {"choice4",7},
        {"goto1",8},
        {"goto2",9},
        {"goto3",10},
        {"goto4",11},
        {"gotoNULL",12}
    };
    public static string ITEM_TABLE = "ItemList";
    public static Dictionary<string,int> ITEM_ATTRIBUTE = new Dictionary<string,int>(){
        {"stage_id",0},
        {"item_id",1},
        {"name",2},
        {"interaction_type",3},
        {"etc",4}
    };
    public static string INVEN_TABLE = "Inventory";
    public static Dictionary<string,int> INVEN_ATTRIBUTE = new Dictionary<string,int>(){
        {"slot_id",0},
        {"stage_id",1},
        {"item_id",2}
    };
}