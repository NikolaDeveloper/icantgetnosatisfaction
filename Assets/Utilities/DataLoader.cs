using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;



public class DataLoader : MonoBehaviour
{
	private static JSONObject data;
	private static StreamWriter writer;


	public static void removeFromData(int index, string fileName){

		string[] bString = readData (File.OpenText (Application.dataPath + "/Data/" + fileName + ".json"));
		string rewrite = "";


		for (int i = 0; i < bString.Length-1; i++) {
			if (i != index) {
				rewrite += bString [i] + ";";
			}
		}

		//Overwrite the old data and store it;
		writer = new StreamWriter (Application.dataPath + "/Data/" + fileName + ".json");
		writer.Write (rewrite);
		writer.Close ();


	}
		
	public static JSONObject read(	string fileName){

		string[] dString = readData(File.OpenText (Application.dataPath + "/Data/" + fileName + ".json"));

		//Create a JSON Object to hold them;
		JSONObject datas = new JSONObject();

		for (int i = 0; i < dString.Length - 1; i++) {
            //Push to a new JSONObject
            JSONObject dataObj = new JSONObject (dString[i]);
			datas.Add (dataObj);
		}

		return datas;
	}
		
	private static string[] readData(StreamReader data){
		string dataString = data.ReadToEnd ();
		data.Close ();
		return dataString.Split (';');
	}

	public static void writeData(string fileName, JSONObject data){
	
	
		FileInfo t = new FileInfo(Application.dataPath + "/Data/" + fileName + ".json");

        if (!t.Exists)
        {
            if (!t.Directory.Exists)
            {
                t.Directory.Create();
            }

            t.Create().Close();

        }

        

        writer = new StreamWriter(Application.dataPath + "/Data/" + fileName + ".json", true);
		writer.Write (data.Print() + ";");
		writer.Close ();
	
	}

		
}

