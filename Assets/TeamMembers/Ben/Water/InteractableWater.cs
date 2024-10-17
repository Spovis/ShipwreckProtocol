using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
[RequireComponent(typeof(WaterTriggerHandler))]
public class InteractableWater : MonoBehaviour
{
    [Header("Mesh Generation")]
    [Range(2, 500)] public int numXVertices = 70;
    public float width = 10f;
    public float height = 4f;
    public Material waterMaterial;
    private const int numYVertices = 2;

    [Header("Gizmo")]
    public Color gizmoColor = Color.white;

    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Vector3[] _vertices;
    private int[] _topVerticiesIndex;

    private EdgeCollider2D _coll;

    private void Start()
    {
        GenerateMesh();
    }

    private void Reset()
    {
        _coll = GetComponent<EdgeCollider2D>();
        _coll.isTrigger = true;
    }

    public void ResetEdgeCollider()
    {
        _coll = GetComponent<EdgeCollider2D>();

        Vector2[] newPoints = new Vector2[2];
        Vector2 firstPoint = new Vector2(_vertices[_topVerticiesIndex[0]].x, _vertices[_topVerticiesIndex[0]].y);
        newPoints[0] = firstPoint;

        Vector2 secondPoint = new Vector2(_vertices[_topVerticiesIndex[_topVerticiesIndex.Length - 1]].x, _vertices[_topVerticiesIndex[_topVerticiesIndex.Length - 1]].y);
        newPoints[1] = secondPoint;

        _coll.offset = Vector2.zero;
        _coll.points = newPoints;
    }

    public void GenerateMesh()
    {
        _mesh = new Mesh();

        _vertices = new Vector3[numXVertices * numYVertices];
        _topVerticiesIndex = new int[numXVertices];
        for (int y = 0; y < numYVertices; y++)
        {
            for (int x = 0; x < numXVertices; x++)
            {
                float xPos = (x / (float)(numXVertices - 1)) * width - width / 2;
                float yPos = (y / (float)(numYVertices - 1)) * height - height / 2;
                _vertices[y * numXVertices + x] = new Vector3(xPos, yPos, 0);

                if (y == numYVertices - 1)
                {
                    _topVerticiesIndex[x] = y * numXVertices + x;
                }
            }
        }

        int[] triangles = new int[(numXVertices - 1) * (numYVertices - 1) * 6];
        int index = 0;

        for (int y = 0; y < numYVertices - 1; y++)
        {
            for (int x = 0; x < numXVertices - 1; x++)
            {
                int bottomLeft = y * numXVertices + x;
                int bottomRight = bottomLeft + 1;
                int topLeft = bottomLeft + numXVertices;
                int topRight = topLeft + 1;

                triangles[index++] = bottomLeft;
                triangles[index++] = topLeft;
                triangles[index++] = bottomRight;

                triangles[index++] = bottomRight;
                triangles[index++] = topLeft;
                triangles[index++] = topRight;
            }
        }

        Vector2[] uvs = new Vector2[_vertices.Length];
        for (int i = 0; i < _vertices.Length; i++)
        {
            uvs[i] = new Vector2(_vertices[i].x + width / 2, (_vertices[i].y + height / 2) / height);
        }

        if(_meshRenderer == null)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        if(_meshFilter == null)
        {
            _meshFilter = GetComponent<MeshFilter>();
        }

        _meshRenderer.material = waterMaterial;

        _mesh.vertices = _vertices;
        _mesh.triangles = triangles;
        _mesh.uv = uvs;

        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        _meshFilter.mesh = _mesh;
    }
}

[CustomEditor(typeof(InteractableWater))]
public class InteractableWaterEditor : Editor
{
    private InteractableWater _water;

    private void OnEnable()
    {
        _water = (InteractableWater)target;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        InspectorElement.FillDefaultInspector(root, serializedObject, this);

        root.Add(new VisualElement { style = { height = 10 } });

        Button generateMeshButton = new Button(() => _water.GenerateMesh()) { text = "Generate Mesh" };
        root.Add(generateMeshButton);

        Button placeEdgeColliderButton = new Button(() => _water.ResetEdgeCollider()) { text = "Place Edge Collider" };
        root.Add(placeEdgeColliderButton);
        return root;
    }

    private void ChangeDimensions(ref float width, ref float height, float calculatedWidthMax, float calculatedHeightMax)
    {
        width = Mathf.Max(0.1f, calculatedWidthMax);
        height = Mathf.Max(0.1f, calculatedHeightMax);
    }

    private void OnSceneGUI()
    {
        Handles.color = _water.gizmoColor;
        Vector3 center = _water.transform.position;
        Vector3 size = new Vector3(_water.width, _water.height, 0.1f);
        Handles.DrawWireCube(center, size);

        float handlesSize = HandleUtility.GetHandleSize(center) * 0.1f;
        Vector3 snap = Vector3.one * 0.1f;

        Vector3[] corners = new Vector3[4];
        corners[0] = center + new Vector3(-_water.width / 2, -_water.height / 2, 0); // Bottom-left
        corners[1] = center + new Vector3(_water.width / 2, -_water.height / 2, 0); // Bottom-right
        corners[2] = center + new Vector3(-_water.width / 2, _water.height / 2, 0); // Top-left
        corners[3] = center + new Vector3(_water.width / 2, _water.height / 2, 0); // Top-right

        EditorGUI.BeginChangeCheck();
        Vector3 newBottomLeft = Handles.FreeMoveHandle(corners[0], handlesSize, snap, Handles.CubeHandleCap);
        if(EditorGUI.EndChangeCheck())
        {
            ChangeDimensions(ref _water.width, ref _water.height, corners[1].x - newBottomLeft.x, corners[3].y - newBottomLeft.y);
            _water.transform.position += new Vector3((newBottomLeft.x - corners[0].x) / 2, (newBottomLeft.y - corners[0].y) / 2, 0);
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newBottomRight = Handles.FreeMoveHandle(corners[1], handlesSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            ChangeDimensions(ref _water.width, ref _water.height, newBottomRight.x - corners[0].x, corners[3].y - newBottomRight.y);
            _water.transform.position += new Vector3((newBottomRight.x - corners[1].x) / 2, (newBottomRight.y - corners[1].y) / 2, 0);
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newTopLeft = Handles.FreeMoveHandle(corners[2], handlesSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            ChangeDimensions(ref _water.width, ref _water.height, corners[3].x - newTopLeft.x, newTopLeft.y - corners[0].y);
            _water.transform.position += new Vector3((newTopLeft.x - corners[2].x) / 2, (newTopLeft.y - corners[2].y) / 2, 0);
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newTopRight = Handles.FreeMoveHandle(corners[3], handlesSize, snap, Handles.CubeHandleCap);
        if (EditorGUI.EndChangeCheck())
        {
            ChangeDimensions(ref _water.width, ref _water.height, newTopRight.x - corners[2].x, newTopRight.y - corners[1].y);
            _water.transform.position += new Vector3((newTopRight.x - corners[3].x) / 2, (newTopRight.y - corners[3].y) / 2, 0);
        }

        if(GUI.changed)
        {
            _water.GenerateMesh();
        }
    }
}
