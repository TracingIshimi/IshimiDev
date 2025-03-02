using System;

public enum InteractionType {
    DIRECT_USE,
    GET_ONLY,
    CHANGE,
    ADDWITH_ITEM,
    ADDWITH_MAP,
    SPSIGHT,
    CONV,
    READ
};

public class Item
{
    int id;
    string name;
    InteractionType type;
    string etc;

    public Item(){
        name = "";
        type = 0;
        etc = "";
    }
    public Item(int id, string name, string type, string etc){
        this.id = id;
        this.name = name;
        this.type = ParseType(type);
        this.etc = etc;
    }

    InteractionType ParseType(string type){
        InteractionType tmpType = 0;
        switch(type){
            case "get_only":
                tmpType = InteractionType.GET_ONLY;
                break;
            case "change":
                tmpType = InteractionType.CHANGE;
                break;
            case "addwith_item":
                tmpType = InteractionType.ADDWITH_ITEM;
                break;
            case "addwith_map":
                tmpType = InteractionType.ADDWITH_MAP;
                break;
            case "spsight":
                tmpType = InteractionType.SPSIGHT;
                break;
            case "conv":
                tmpType = InteractionType.CONV;
                break;
            case "read":
                tmpType = InteractionType.READ;
                break;
            case "direct use":
            default:
                tmpType = InteractionType.DIRECT_USE;
                break;
        }

        return tmpType;
    }

    public int getId(){
        return id;
    }

    public string getName(){
        return name;
    }

    public InteractionType getItemType(){
        return type;
    }

    public string getEtc(){
        return etc;
    }
}
