using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeleteSaveEditor : EditorWindow
{
    [MenuItem("MyMenu/DeleteSaveFiles")]
    static void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("세이브 파일 다 지움");
    }
}