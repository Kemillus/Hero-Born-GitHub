using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;

    public Transform patrolRoute;
    public List<Transform> location;

    int locationIndex = 0;
    NavMeshAgent agent;
    int lives = 3;
    public int EnemyLives
    {
        get { return lives; }
        private set { lives = value;
            if (lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down!");
            }
        }
    }

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        InitialaizePatrolRoute();
        MoveToNextPatrolLocation();
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
            MoveToNextPatrolLocation();
    }

    void MoveToNextPatrolLocation()
    {
        if (location.Count == 0)
            return;

        agent.destination = location[locationIndex].position;

        locationIndex = (locationIndex + 1) % location.Count;
    }

    void InitialaizePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
            location.Add(child);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position;
            Debug.Log("Player detected - attack!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            MoveToNextPatrolLocation();
            Debug.Log("Player out of range, resume patrol.");

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Critical Hit!");
        }
    }
}
