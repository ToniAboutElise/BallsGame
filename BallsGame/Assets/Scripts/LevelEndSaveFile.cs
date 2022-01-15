using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LevelEndSaveFile : MonoBehaviour
{
    public static void SaveFileEndLevel(string levelName)
    {
        string savedFile = Application.persistentDataPath + "\\savedFile.txt";

        int currentLine = 0;
        foreach (string line in File.ReadAllLines(savedFile))
        {
            if(line == levelName + " False")
            {
                OverwriteLine(levelName + " True", savedFile, currentLine);
            }
            currentLine++;
        }
    }

    private static void OverwriteLine(string newText, string fileName, int line_to_edit)
    {
        string[] arrLine = File.ReadAllLines(fileName);
        arrLine[line_to_edit] = newText;
        File.WriteAllLines(fileName, arrLine);
    }
}
