using System.Collections.Generic;

public class Character{
    string name;
    string spriteAddress;   //베이스 주소
    int spriteMax;
    public Character(string name, string spriteAddress, int spriteMax){
        this.name = name;
        this.spriteAddress = spriteAddress;
        this.spriteMax = spriteMax;
    }

    public string GetName(){
        return name;
    }

    public string GetSpriteAddress(){
        return spriteAddress;
    }
}
public class CharData{
    Dictionary<int, Character> characterArray ;
    public void AddCharacter(int idNum, Character tmpCharacter){
        characterArray.Add(idNum, tmpCharacter);
    }
    public Character GetCharacter(int idNum){
        return characterArray[idNum];
    }

}