using System.Collections.Generic;

public static class Const{
    public static string STORY_PATH_BASE = "Assets/Resources/Data/Story/";
    public static string BGIMG_PATH_BASE = "Data/BgImg/";
    public static string CHARACTER_PATH_BASE = "Data/Character/";
    public static string ITEM_PATH_BASE = "Data/Item/";
    public static int CHARACTER_MAX_IDX = 0;
    public static int ITEM_MAX_IDX = 100;

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
        {"character_id",4},
        {"charimg_num",5},
        {"content",6},
        {"next_goto",7}
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
        {"mouse1",8},
        {"mouse2",9},
        {"mouse3",10},
        {"mouse4",11},
        {"goto1",12},
        {"goto2",13},
        {"goto3",14},
        {"goto4",15},
        {"gotoNULL",16}
    };
    public static string ITEM_TABLE = "ItemList";
    public static Dictionary<string,int> ITEM_ATTRIBUTE = new Dictionary<string,int>(){
        {"stage_id",0},
        {"item_id",1},
        {"name",2},
        {"interaction_type",3},
        {"is_gettable",4},
        {"max_quant",5}
    };
    public static string INVEN_TABLE = "Inventory";
    public static Dictionary<string,int> INVEN_ATTRIBUTE = new Dictionary<string,int>(){
        {"slot_id",0},
        {"stage_id",1},
        {"item_id",2},
        {"quantity",3}
    };
}