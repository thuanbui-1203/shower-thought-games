using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class GameObjectEntry
{
    public GameObject prefab;
    [Min(0)]
    public int spawnCount = 1;
}

public class RandomSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public List<GameObjectEntry> spawnEntries = new List<GameObjectEntry>();

    [Header("Spawn Bounds")]
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -4f;
    public float maxY = 4f;

    [Header("Options")]
    public bool spawnOnStart = true;
    public bool clearOnRespawn = true;

    private List<GameObject> _spawnedObjects = new List<GameObject>();

    void Start()
    {
        if (spawnOnStart)
            SpawnAll();
    }

    public void SpawnAll()
    {
        if (clearOnRespawn)
            ClearSpawned();

        foreach (var entry in spawnEntries)
        {
            if (entry.prefab == null) continue;

            for (int i = 0; i < entry.spawnCount; i++)
            {
                Vector3 randomPos = new Vector3(
                    Mathf.Clamp(Random.Range(minX, maxX), -10f, 10f),
                    Mathf.Clamp(Random.Range(minY, maxY), -4f, 4f),
                    0f
                );

                GameObject spawned = Instantiate(entry.prefab, randomPos, Quaternion.identity, transform);
                _spawnedObjects.Add(spawned);
            }
        }

        Debug.Log($"[RandomSpawner] Spawned {_spawnedObjects.Count} objects.");
    }

    public void ClearSpawned()
    {
        foreach (var obj in _spawnedObjects)
        {
            if (obj != null)
#if UNITY_EDITOR
                DestroyImmediate(obj);
#else
                Destroy(obj);
#endif
        }
        _spawnedObjects.Clear();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0.5f, 0.25f);
        Vector3 center = new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0f);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, 0.1f);
        Gizmos.DrawCube(center, size);

        Gizmos.color = new Color(0f, 1f, 0.5f, 1f);
        Gizmos.DrawWireCube(center, size);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(RandomSpawner))]
public class RandomSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RandomSpawner spawner = (RandomSpawner)target;

        EditorGUILayout.Space(8);

        GUI.backgroundColor = new Color(0.4f, 0.9f, 0.5f);
        if (GUILayout.Button("Spawn Now", GUILayout.Height(32))) spawner.SpawnAll();

        GUI.backgroundColor = new Color(0.9f, 0.4f, 0.4f);
        if (GUILayout.Button("Clear Spawned", GUILayout.Height(26))) spawner.ClearSpawned();

        GUI.backgroundColor = Color.white;
    }
}
#endif