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

    private void Start(){
        string dbname = "/TaleOfIshimi.db";
        string connectionString = "URI=file:"+Application.streamingAssetsPath+dbname;
        IDbConnection dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();

        string tablename = "SingleScript";

        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM " + tablename;
        IDataReader dataReader = dbCommand.ExecuteReader();

        while(dataReader.Read()){
            int scriptId = dataReader.GetInt32(1);
            string content = dataReader.GetString(6);
            Debug.Log("id "+scriptId+": "+content);
        }
    }
}
