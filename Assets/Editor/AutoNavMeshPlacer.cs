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
        GUILayout.Label("선택된 오브젝트를 NavMesh 위로 이동", EditorStyles.boldLabel);

        if (GUILayout.Button("NavMesh에 자동 정렬"))
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
                Debug.LogWarning($"'{obj.name}' 는 근처에 NavMesh를 찾을 수 없습니다.");
            }
        }
    }
}
