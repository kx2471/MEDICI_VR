using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// nav mesh agent기능을 이용해서 타워를 향해 이동하고싶다.
public class Enemy : MonoBehaviour
{
    public enum State
    {
        Search,
        Move,
        Attack
    }
    public State state;


    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
        agent.isStopped = true;
        state = State.Search;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Search: UpdateSearch(); break;
            case State.Move: UpdateMove(); break;
            case State.Attack: UpdateAttack(); break;
        }
    }

    private void UpdateSearch()
    {
        // 반경 1000M 안에 타워를 충돌체들을 찾고싶다.
        Collider[] cols = Physics.OverlapSphere(transform.position, 1000);

        int chooseIndex = -1;
        float distance = 99999;
        for (int i = 0; i < cols.Length; i++)
        {
            if (false == cols[i].gameObject.CompareTag("Tower"))
            {
                continue;
            }
            float temp = Vector3.Distance(transform.position, cols[i].transform.position);
            if (distance > temp)
            {
                distance = temp;
                chooseIndex = i;
            }
        }
        // 그중에 가장 가까운 타워를 agent의 목적지로 알려주고싶다.
        if (chooseIndex != -1)
        {
            state = State.Move;
            agent.isStopped = false;
            targetPosition = cols[chooseIndex].transform.position;
            agent.destination = targetPosition;
        }
    }
    Vector3 targetPosition;

    private void UpdateMove()
    {
        // 도착했다면 => 목적지와의 거리가 stoppingDistance 이하라면
        float remainDistance = Vector3.Distance(transform.position, targetPosition);
        if (remainDistance <= agent.stoppingDistance)
        {
            // 공격상태로 전이하고싶다.
            agent.isStopped = true;
            state = State.Attack;
        }
    }

    private void UpdateAttack()
    {
    }
}
