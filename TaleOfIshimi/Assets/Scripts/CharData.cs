using System.Collections.Generic;
public static class CharData{
    public class Character{
        string name;
        string spriteAddress;   //베이스 주소

        public string GetName(){
            return name;
        }
    }

    public static List<Character> characterList = new List<Character>();
    


}