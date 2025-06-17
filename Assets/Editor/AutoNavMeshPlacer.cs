using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class AutoNavMeshPlacer : EditorWindow
{
    [MenuItem("Tools/Auto NavMesh Placement")]
    public static void ShowWindow()
    {
        GetWindow<AutoNavMeshPlacer>("NavMesh Auto Placer");
    }

    private void OnGUI()
    {
        GUILayout.Label("���õ� ������Ʈ�� NavMesh ���� �̵�", EditorStyles.boldLabel);

        if (GUILayout.Button("NavMesh�� �ڵ� ����"))
        {
            PlaceSelectedObjectsOnNavMesh();
        }
    }

    private void PlaceSelectedObjectsOnNavMesh()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(obj.transform.position, out hit, 5.0f, NavMesh.AllAreas))
            {
                Undo.RecordObject(obj.transform, "Auto Place on NavMesh");
                obj.transform.position = hit.position;
            }
            else
            {
                Debug.LogWarning($"'{obj.name}' �� ��ó�� NavMesh�� ã�� �� �����ϴ�.");
            }
        }
    }
}
