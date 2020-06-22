using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(WaveConfig))]
public class WaveEditor : UnityEditor.Editor
{
    List<ReorderableList> batchLists;
    SerializedProperty waitTime;
    SerializedProperty multiplierDuration;

    private void OnEnable()
    {
        batchLists = new List<ReorderableList>();
        waitTime = serializedObject.FindProperty("waveEndWaitTime");
        multiplierDuration = serializedObject.FindProperty("multiplierDuration");

        Rect rect = new Rect();
        for (int i = 1; i <= 4; i++)
        {
            SerializedProperty batches;
            SerializedProperty spawn;
            spawn = serializedObject.FindProperty("spawn"+i);
            batches = FindProperty(spawn, "batches");

            ReorderableList list = new ReorderableList(serializedObject, batches, true, true, true, true);
            list.drawElementCallback = getDrawCallBack(list);
            list.drawHeaderCallback = getHeaderCallback(i);

            int x = Task.Run(() => { return 1; }).Result;
            batchLists.Add(list);
        }

    }

    private ReorderableList.HeaderCallbackDelegate getHeaderCallback(int x)
    {
        return (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Spawn " + x + "    |wait s |between |enemy amount");
        };
    }

    private ReorderableList.ElementCallbackDelegate getDrawCallBack(ReorderableList batchList)
    {
        ReorderableList.ElementCallbackDelegate drawElementCallBack =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                SerializedProperty element = batchList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty waitTime = element.FindPropertyRelative("waitTime");
                SerializedProperty spawnTime = element.FindPropertyRelative("spawnTime");
                SerializedProperty enemyCount = element.FindPropertyRelative("enemyCount");
                SerializedProperty enemyType = element.FindPropertyRelative("enemyType");

                EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Batch " + index);
                waitTime.floatValue = EditorGUI.FloatField(new Rect(rect.x + 50, rect.y, 40, EditorGUIUtility.singleLineHeight), waitTime.floatValue);
                spawnTime.floatValue = EditorGUI.FloatField(new Rect(rect.x + 100, rect.y, 40, EditorGUIUtility.singleLineHeight), spawnTime.floatValue);
                enemyCount.intValue = EditorGUI.IntField(new Rect(rect.x + 150, rect.y, 40, EditorGUIUtility.singleLineHeight), enemyCount.intValue);
                enemyType.enumValueIndex = (int)(EnemyType)EditorGUI.EnumPopup(new Rect(rect.x + 200, rect.y, 100, EditorGUIUtility.singleLineHeight), (EnemyType)Enum.GetValues(typeof(EnemyType)).GetValue(enemyType.enumValueIndex));
            };

        return drawElementCallBack;
    }

    private SerializedProperty FindProperty(SerializedProperty parent, string name)
    {
        IEnumerator e = parent.GetEnumerator();
        while(e.MoveNext())
        {
            SerializedProperty prop = e.Current as SerializedProperty;
            if(prop.name == name)
            {
                return prop;
            }
        }

        return null;
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        //base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.LabelField("Every level has 4 spawns, leave empty if not needed.");
        EditorGUILayout.LabelField("1: Seconds to wait before spawnin starts");
        EditorGUILayout.LabelField("2: Seconds between two spawning enemies");
        EditorGUILayout.LabelField("3: Amount of enemies");
        EditorGUILayout.LabelField("4: Type of enemy");

        foreach (ReorderableList list in batchLists)
        {
            list.DoLayoutList();
        }

        waitTime.floatValue = EditorGUILayout.FloatField("End of wave wait time (s)", waitTime.floatValue);
        multiplierDuration.floatValue = EditorGUILayout.FloatField("Wave skipping multiplier lasts (s)", multiplierDuration.floatValue);
        serializedObject.ApplyModifiedProperties();
    }
}