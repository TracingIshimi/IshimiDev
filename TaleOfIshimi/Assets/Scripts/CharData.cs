using System.Collections.Generic;

public class Character{
    string name;
    string spriteAddress;   //베이스 주소
    public Character(string name, string spriteAddress){
        this.name = name;
        this.spriteAddress = spriteAddress;
    }

    public string GetName(){
        return name;
    }

    public string GetSpriteAddress(){
        return spriteAddress;
    }
}
public class CharData{
    public Character[] characterArray = new Character[Const.CHARACTER_MAX_IDX];

    public CharData(){
        characterArray[0] = new Character("주인공", Const.CHARACTER_PATH_BASE+"PCtemp/");
        characterArray[1] = new Character("NPC1", Const.CHARACTER_PATH_BASE+"NPCtemp1/");
        characterArray[1] = new Character("NPC2", Const.CHARACTER_PATH_BASE+"NPCtemp2/");
    }

}