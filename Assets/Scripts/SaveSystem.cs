using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(SaveAndLoad saveAndLoad)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData();
        //Debug.Log("Spremanje podataka");
        //Debug.Log(playerData.concentration);
        //Debug.Log(playerData.stress);
        //Debug.Log(playerData.hints);
        //Debug.Log(playerData.steps);
        //Debug.Log(playerData.position[0]);
        //Debug.Log(playerData.position[1]);
        //Debug.Log(playerData.position[2]);
        Debug.Log(playerData.numberOfMistakes);
        formatter.Serialize(stream, playerData);
        stream.Close();

    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            //Debug.Log("Ucitavanje podataka");
            //Debug.Log(data.concentration);
            //Debug.Log(data.stress);
            //Debug.Log(data.hints);
            //Debug.Log(data.steps);
            //Debug.Log(data.position[0]);
            //Debug.Log(data.position[1]);
            //Debug.Log(data.position[2]);
            Debug.Log(data.numberOfMistakes);
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
