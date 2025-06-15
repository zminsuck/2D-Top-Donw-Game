using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public Transform player;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) player = p.transform;
            else Debug.LogWarning("EnemyFSM: Player 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent != null)
        {
            navMeshAgent.updateRotation = false;   // 2D에서는 회전 사용 안 함
            navMeshAgent.updateUpAxis = false;
        }
        else
        {
            Debug.LogError("EnemyFSM: NavMeshAgent 컴포넌트를 찾을 수 없습니다.");
        }
    }

    void Update()
    {
        if (player == null || animator == null || navMeshAgent == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("DistanceToPlayer", dist);

        navMeshAgent.SetDestination(player.position);
    }
}
