using UnityEngine;

public class EnemyCasting : MonoBehaviour
{
    public static float E_distanceFromTarget;
    [SerializeField] private float sightRange;

    [SerializeField] float toTarget;
    LayerMask whatIsWall;

    private EnemyAI enemyAI;
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, sightRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                enemyAI.targetPlayer = true;
            }
        }
        else
        {
            enemyAI.targetPlayer = false;
        }
    }
}
