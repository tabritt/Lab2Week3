using UnityEngine;

public class Editable : MonoBehaviour
{
    [Header("Shape Sizes")]
    [Tooltip("Controls the size of all cubes with this tag.")]
    public float cubeSize = 1f;

    [Tooltip("Controls the radius of all spheres with this tag.")]
    public float sphereRadius = 1f;
}
