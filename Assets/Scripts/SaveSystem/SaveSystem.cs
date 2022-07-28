using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void saveGame(GameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter(); //Create new file
        string path = Application.persistentDataPath + "/game.fun"; //Get path to save new file
        FileStream stream = new FileStream(path, FileMode.Create); //new FileStream

        PlayerData data = new PlayerData(gameData); //Pass info gamedata

        formatter.Serialize(stream,data); //Serialize file
        stream.Close(); //Close file
    }

    public static PlayerData loadGame()
    {
        string path = Application.persistentDataPath + "/game.fun";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file dont found in" + path);
            return null;
        }
    }
}
