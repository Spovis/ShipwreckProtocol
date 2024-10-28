using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(BoxCollider2D))]
[RequireComponent(typeof(WaterTriggerHandler))]
public class InteractableWater : MonoBehaviour
{
    [Header("Springs")]
    [SerializeField] private float _spriteConstant = 1.4f;
    [SerializeField] private float _damping = 1.1f;
    [SerializeField] private float _spread = 8f;
    [SerializeField, Range(1, 10)] private int _wavePropogationIterations = 8;
    [SerializeField, Range(0f, 20f)] private float _speedMult = 5.5f;

    [Header("Force")]
    public float forceMultiplier = 0.2f;
    [Range(1f, 50f)] public float MaxForce = 5f;

    [Header("Collision")]
    [SerializeField, Range(1f, 10f)] private float _playerCollisionRadiusMult = 4.15f;

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

    private BoxCollider2D _coll;

    private class WaterPoint
    {
        public float velocity, acceleration, pos, targetHeight;
    }
    private List<WaterPoint> _waterPoints = new List<WaterPoint>();

    private void Start()
    {
        _coll = GetComponent<BoxCollider2D>();

        GenerateMesh();
        CreateWaterPoints();
    }

    private void Reset()
    {
        _coll = GetComponent<BoxCollider2D>();
        _coll.isTrigger = true;
    }

    private void FixedUpdate()
    {
        for (int i = 1; i < _waterPoints.Count - 1; i++)
        {
            WaterPoint point = _waterPoints[i];

            float x = point.pos - point.targetHeight;
            float acceleration = -_spriteConstant * x - _damping * point.velocity;
            point.pos += point.velocity * _speedMult * Time.fixedDeltaTime;
            _vertices[_topVerticiesIndex[i]].y = point.pos;
            point.velocity += acceleration * _speedMult * Time.fixedDeltaTime;
        }

        for(int j = 0; j < _wavePropogationIterations; j++)
        {
            for (int i = 1; i < _waterPoints.Count - 1; i++)
            {
                float leftDelta = _spread * (_waterPoints[i].pos - _waterPoints[i - 1].pos) * _speedMult * Time.fixedDeltaTime;
                _waterPoints[i - 1].velocity += leftDelta;

                float rightDelta = _spread * (_waterPoints[i].pos - _waterPoints[i + 1].pos) * _speedMult * Time.fixedDeltaTime;
                _waterPoints[i + 1].velocity += rightDelta;
            }
        }

        _mesh.vertices = _vertices;
    }

    public void Splash(Collider2D collision, float force)
    {
        float radius = collision.bounds.extents.x * _playerCollisionRadiusMult;
        Vector2 center = collision.transform.position;

        for (int i = 0; i < _waterPoints.Count; i++)
        {
            Vector2 vertextWorldPos = transform.TransformPoint(_vertices[_topVerticiesIndex[i]]);

            if(IsPointInsideCircle(vertextWorldPos, center, radius))
            {
                _waterPoints[i].velocity = force;
            }
        }
    }

    private bool IsPointInsideCircle(Vector2 point, Vector2 center, float radius)
    {
        float distanceSquared = (point - center).sqrMagnitude;
        return distanceSquared <= radius * radius;
    }

    public void ResetBoxCollider()
    {
        _coll = GetComponent<BoxCollider2D>();

        _coll.size = new Vector2(width, height - 1f);
        _coll.offset = new Vector2(0, -0.5f);

        if(_coll.size.y < 0.5f)
        {
            _coll.size = new Vector2(width, height);
            _coll.offset = Vector2.zero;
        }
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
    
    private void CreateWaterPoints()
    {
        _waterPoints.Clear();

        for (int i = 0; i < _topVerticiesIndex.Length; i++)
        {
            _waterPoints.Add(new WaterPoint
            {
                pos = _vertices[_topVerticiesIndex[i]].y,
                targetHeight = _vertices[_topVerticiesIndex[i]].y,
            });
        }
    }
}

#if UNITY_EDITOR
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

        Button placeEdgeColliderButton = new Button(() => _water.ResetBoxCollider()) { text = "Place Edge Collider" };
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
            _water.ResetBoxCollider();
        }
    }
}
#endif