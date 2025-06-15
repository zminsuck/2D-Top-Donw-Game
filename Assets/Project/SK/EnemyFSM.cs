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
            else Debug.LogWarning("EnemyFSM: Player �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�.");
        }

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent != null)
        {
            navMeshAgent.updateRotation = false;   // 2D������ ȸ�� ��� �� ��
            navMeshAgent.updateUpAxis = false;
        }
        else
        {
            Debug.LogError("EnemyFSM: NavMeshAgent ������Ʈ�� ã�� �� �����ϴ�.");
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
