using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;

public class TaskEditor : EditorWindow
{
    private string taskName;//タスク名
    private string fileName = "Assets/Data/task.xlsx";//ファイル名
    private int importance;//重要度
    private int menberName;//担当者名

    [UnityEditor.MenuItem("Window/TaskEditor")]
    static void ShowTestMainWindow()
    {
        EditorWindow.GetWindow(typeof(TaskEditor));
    }

    /// <summary>
    /// レイアウト
    /// </summary>
    void OnGUI()
    {
        //タスクを担当する人を設定
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        GUILayout.Label("担当者 : ", GUILayout.Width(110));
        string[] menberNameStrings = new string[] { "大木", "西澤", "山下" };
        menberName = GUILayout.Toolbar(menberName, menberNameStrings);
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        //タスク名を設定
        GUILayout.BeginHorizontal();
        GUILayout.Label("タスク名 : ", GUILayout.Width(110));
        taskName = (string)EditorGUILayout.TextField(taskName);
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        //タスクの重要度を設定
        GUILayout.BeginHorizontal();
        GUILayout.Label("重要度 : ", GUILayout.Width(110));
        string[] importanceStrings = new string[] { "最重要", "重要", "普通" };
        importance = GUILayout.Toolbar(importance, importanceStrings);
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        //ファイルを保存
        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("保存", GUILayout.Width(100), GUILayout.Height(25)))
        {
            SaveFile();
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// ファイルを保存
    /// </summary>
    private void SaveFile()
    {
        IWorkbook readBook = null;
        using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            readBook = new XSSFWorkbook(fs);
        }
        ISheet sheet = readBook.GetSheetAt(menberName);
        int rowIdx = sheet.LastRowNum;

        IRow row = sheet.CreateRow(rowIdx + 1);
        int cellIdx = 1;

        ICell taskNameCell = row.CreateCell(cellIdx++);
        taskNameCell.SetCellValue(taskName);

        ICell importanceCell = row.CreateCell(cellIdx++);
        importanceCell.SetCellValue(Impotance(importance));

        IWorkbook book = new XSSFWorkbook();
        

        using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
        {
            book.Write(fs);
        }
    }

    /// <summary>
    /// 重要度
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private string Impotance(int i)
    {
        string im = "";

        if (i == 0)
        {
            im = "最重要";
        }
        if (i == 1)
        {
            im = "重要";
        }
        if (i == 2)
        {
            im = "普通";
        }
        return im;
    }
}
