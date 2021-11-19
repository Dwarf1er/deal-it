#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(TilemapNavMesh)), CanEditMultipleObjects]
public class TilemapNavMeshEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        TilemapNavMesh tilemapNavMesh = (TilemapNavMesh)target;
        if(GUILayout.Button("Bake")) {
            Mesh mesh = tilemapNavMesh.BuildMesh();
            AssetDatabase.CreateAsset(mesh, "Assets/Pathfinding/" + SceneManager.GetActiveScene().name.Replace(" ", "_") + ".asset");
            AssetDatabase.SaveAssets();
            tilemapNavMesh.BuildComponents(mesh);
        }
    }
}
#endif