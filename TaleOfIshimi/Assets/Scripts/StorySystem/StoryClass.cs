using System.Collections.Generic;

class StoryObject{
        Dictionary<int,SingleScript> scripts;
        public StoryObject(){
            scripts = new Dictionary<int, SingleScript>();
        }
        public void SetScript(int n, SingleScript singleObj){
            scripts.Add(n,singleObj);
        }
        public SingleScript GetScript(int n){
            return scripts[n];
        }
    }
    
    enum ScriptType{
        NARR, 
        CHAR,
        CHOICE,
        ILLUST_FULL,
        ILLUST_MODAL, 
        END_CONV
    }
    class SingleScript{
        ScriptType type;
        int charNum;
        int spriteNum;
        int illustNum;
        int bgimgNum;
        string content;
        int nextGoto;

        // 생성자
        public SingleScript(ScriptType type, int bgimgNum, int illustNum, int charNum, int charImgNum, string content, int nextGoto){
            this.type = type;
            this.bgimgNum = bgimgNum;
            this.illustNum = illustNum;
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
        public int GetIllustNum(){
            return illustNum;
        }
    }

    class ChoiceScript{
        int choiceMax;
        int timer;
        string[] choice = new string[4];
        int[] choiceGoto = new int[5];

        public ChoiceScript(int choiceMax, int timer, string[] choice, int[] choiceGoto){
            this.choiceMax = choiceMax;
            this.timer = timer;
            this.choice = choice;
            this.choiceGoto = choiceGoto;
        }

        public int GetChoiceMax(){
            return choiceMax;
        }
        public int GetTimer(){
            return timer;
        }
        public string GetChoice(int idx){
            return choice[idx];
        }
        public int GetChoiceGoto(int idx){
            return choiceGoto[idx];
        }

    }