using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DBManager : MonoBehaviour
{
    public static DBManager dbManager;
    public IDbConnection dbConnection;

    private void Awake(){
        if(dbManager == null){
            dbManager = this;
            DontDestroyOnLoad(dbManager);
        }
        else if(dbManager != this){
            Destroy(this);
            return;
        }
    }

        // DB 관련 코드 리팩토링 - 코드 파일 분리 필요
    public void OpenDBConnection(){
        string connectionString = "URI=file:"+Application.streamingAssetsPath+Const.DB_NAME;
        dbConnection = new SqliteConnection(connectionString);
        dbConnection.Open();
    }

    public void CloseDBConnection(){
        dbConnection.Close();
    }
}
