using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(Transform))] // Can attach this to any inspector or make a manager
public class Editable1 : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10);

        // === SELECT ALL SPHERES ===
        if (GUILayout.Button("Select All Spheres", GUILayout.Height(30)))
        {
            var allSpheres = GameObject.FindObjectsOfType<GameObject>(true)
                .Where(obj => obj.name.ToLower().Contains("sphere"))
                .ToArray();

            if (allSpheres.Length > 0)
                Selection.objects = allSpheres;
            else
                Debug.LogWarning("No spheres found in the scene.");
        }

        // === SELECT ALL CUBES ===
        if (GUILayout.Button("Select All Cubes", GUILayout.Height(30)))
        {
            var allCubes = GameObject.FindObjectsOfType<GameObject>(true)
                .Where(obj => obj.name.ToLower().Contains("cube"))
                .ToArray();

            if (allCubes.Length > 0)
                Selection.objects = allCubes;
            else
                Debug.LogWarning("No cubes found in the scene.");
        }

        GUILayout.Space(10);

        // === TOGGLE SPHERES ON/OFF ===
        if (GUILayout.Button("Enable/Disable All Spheres", GUILayout.Height(35)))
        {
            var allSpheres = GameObject.FindObjectsOfType<GameObject>(true)
                .Where(obj => obj.name.ToLower().Contains("sphere"))
                .ToArray();

            if (allSpheres.Length == 0)
            {
                Debug.LogWarning("No spheres found in the scene.");
                return;
            }

            bool anyEnabled = allSpheres.Any(obj => obj.activeSelf);
            bool setActive = !anyEnabled;

            foreach (var sphere in allSpheres)
                sphere.SetActive(setActive);

            Debug.Log($"{allSpheres.Length} spheres {(setActive ? "enabled" : "disabled")}.");
        }

        // === TOGGLE CUBES ON/OFF ===
        if (GUILayout.Button("Enable/Disable All Cubes", GUILayout.Height(35)))
        {
            var allCubes = GameObject.FindObjectsOfType<GameObject>(true)
                .Where(obj => obj.name.ToLower().Contains("cube"))
                .ToArray();

            if (allCubes.Length == 0)
            {
                Debug.LogWarning("No cubes found in the scene.");
                return;
            }

            bool anyEnabled = allCubes.Any(obj => obj.activeSelf);
            bool setActive = !anyEnabled;

            foreach (var cube in allCubes)
                cube.SetActive(setActive);

            Debug.Log($"{allCubes.Length} cubes {(setActive ? "enabled" : "disabled")}.");
        }
    }
}
