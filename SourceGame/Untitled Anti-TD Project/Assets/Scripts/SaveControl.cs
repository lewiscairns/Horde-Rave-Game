using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveControl : MonoBehaviour
{
    string savePath;
    static public SaveControl instance;

    private void Awake()
    {
        //set the save file path
        savePath = Application.persistentDataPath + "/save.data";

        //check that this is the only instance to ensure singleton game design pattern is kept
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void SaveGame(PlayerSave data)
    {
        //set up the file output and convert the data to binary
        FileStream dataStream = new FileStream(savePath, FileMode.Create);
        BinaryFormatter converter = new BinaryFormatter();
        //send data to the file then close the file output stream
        converter.Serialize(dataStream, data);
        dataStream.Close();
    }

    public PlayerSave LoadGame()
    {
        //check if save file exists
        if(File.Exists(savePath)){
            //if so then start the file input and decrypt, then close the file input stream
            FileStream dataStream = new FileStream(savePath, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            PlayerSave data = converter.Deserialize(dataStream) as PlayerSave;
            dataStream.Close();
            return data;
        } else {
            //if there is no save file return the game data as null
            Debug.Log("File not found");
            return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
