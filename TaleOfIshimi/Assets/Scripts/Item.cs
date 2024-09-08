using System;

public enum InteractionType {
    DIRECT_USE,
    GET_ONLY,
    CHANGE,
    ADDWITH
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
            case "get only":
                tmpType = InteractionType.GET_ONLY;
                break;
            case "change":
                tmpType = InteractionType.CHANGE;
                break;
            case "addwith":
                tmpType = InteractionType.ADDWITH;
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
