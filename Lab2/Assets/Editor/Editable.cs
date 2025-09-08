using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Editable))]
[CanEditMultipleObjects] // enables multi-object editing
public class EditableEditor : Editor
{
    SerializedProperty cubeSize;
    SerializedProperty sphereRadius;

    private void OnEnable()
    {
        // Links the Serialized properties from Editable Ediotr Script
        cubeSize = serializedObject.FindProperty("cubeSize");
        sphereRadius = serializedObject.FindProperty("sphereRadius");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw default inspector values first
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Shape Size Settings", EditorStyles.boldLabel);

        // Cube size field + warnings
        EditorGUILayout.PropertyField(cubeSize, new GUIContent("Cube Size"));
        if (cubeSize.floatValue > 2f)
        {
            EditorGUILayout.HelpBox(" The cubes' sizes cannot be bigger than 2!", MessageType.Warning);
        }
        else if (cubeSize.floatValue <= 0f)
        {
            EditorGUILayout.HelpBox(" Cube has to be > 0 for sure!!!!", MessageType.Error);
        }

        // Sphere radius field + warnings
        EditorGUILayout.PropertyField(sphereRadius, new GUIContent("Sphere Radius"));
        if (sphereRadius.floatValue < 1f)
        {
            EditorGUILayout.HelpBox(" The spheres' radius cannot be smaller than 1!", MessageType.Warning);
        }
        else if (sphereRadius.floatValue > 5f)
        {
            EditorGUILayout.HelpBox(" Sphere too big?!!", MessageType.Info);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Shape Selection", EditorStyles.boldLabel);

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

        serializedObject.ApplyModifiedProperties();
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

        bool allVisible = objs.All(o => o.activeSelf && !SceneVisibilityManager.instance.IsHidden(o));
        string buttonText = allVisible ? $"Hide all {tag}s" : $"Show all {tag}s"; //this button specifically

        GUI.color = allVisible ? Color.green : Color.yellow;

        if (GUILayout.Button(buttonText))
        {
            Undo.SetCurrentGroupName(buttonText);
            int group = Undo.GetCurrentGroup();

            foreach (var obj in objs)
            {
                UndoHiddenObjs(obj, buttonText);

                // Toggle active state
                obj.SetActive(!allVisible);

                // Toggle Scene visibility in the Editor
                if (allVisible)
                    SceneVisibilityManager.instance.Hide(obj, false);
                else
                    SceneVisibilityManager.instance.Show(obj, false);

                EditorUtility.SetDirty(obj);
            }

            Undo.CollapseUndoOperations(group);
        }

        GUI.color = Color.white;
    }

    void UndoHiddenObjs(GameObject obj, string actionName)
    {
        Undo.RegisterFullObjectHierarchyUndo(obj, actionName);
    }
}
