using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Editable))]
public class EditableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Select all Cubes")) //swapped enimes to the shapes
        {
            SelectTag("Cube"); //added tag calling instead of a variable
        }

        if (GUILayout.Button("Select all Spheres")) //same vibes
        {
            SelectTag("Sphere"); //different font
        }

        if (GUILayout.Button("Clear Selection")) //removes the selection
        {
            Selection.objects = new Object[0];
        }

        DrawToggle("Cube");
        DrawToggle("Sphere");
    }

    void SelectTag(string tag) //adds the tagged objects in an array
    {
        var objs = GameObject.FindGameObjectsWithTag(tag);
        Selection.objects = objs.Cast<Object>().ToArray();
    }

    void DrawToggle(string tag) // this function is just saying that if this tag exists in the scene then this button will exists
    {
        var objs = GameObject.FindGameObjectsWithTag(tag).ToList();

        if (objs.Count == 0)
            return; //exit method if 0

        bool allActive = objs.All(o => o.activeSelf);

        string buttonText = allActive ? $"Hide all {tag}s" : $"Show all {tag}s"; //this button specifically

        GUI.color = allActive ? Color.green : Color.yellow;

        if (GUILayout.Button(buttonText))
        {
            foreach (var obj in objs)
            {
                obj.SetActive(!allActive);
            }
        }

        GUI.color = Color.white;
    }

}
