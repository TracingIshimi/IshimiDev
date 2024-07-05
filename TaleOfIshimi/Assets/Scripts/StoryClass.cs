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
        CHOICE,
        ILLUST_FULL,
        ILLUST_MODAL
    }
    class SingleScript{
        ScriptType type;
        int charNum;
        int spriteNum;
        int bgimgNum;   // 스크립트 타입이 ILLUST_FULL일 경우 일러스트 ID로 사용
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
        int choiceMax;
        int timer;
        string[] choice = new string[4];
        string[] mouseover = new string[4];
        int[] choiceGoto = new int[5];

        public ChoiceScript(int choiceMax, int timer, string[] choice, string[] mouseover, int[] choiceGoto){
            this.choiceMax = choiceMax;
            this.timer = timer;
            this.choice = choice;
            this.mouseover = mouseover;
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
        public string GetMouseover(int idx){
            return mouseover[idx];
        }
        public int GetChoiceGoto(int idx){
            return choiceGoto[idx];
        }

    }