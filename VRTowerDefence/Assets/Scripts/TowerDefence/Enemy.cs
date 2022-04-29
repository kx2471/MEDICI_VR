using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// nav mesh agent����� �̿��ؼ� Ÿ���� ���� �̵��ϰ�ʹ�.
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
        // �ݰ� 1000M �ȿ� Ÿ���� �浹ü���� ã��ʹ�.
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
        // ���߿� ���� ����� Ÿ���� agent�� �������� �˷��ְ�ʹ�.
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
        // �����ߴٸ� => ���������� �Ÿ��� stoppingDistance ���϶��
        float remainDistance = Vector3.Distance(transform.position, targetPosition);
        if (remainDistance <= agent.stoppingDistance)
        {
            // ���ݻ��·� �����ϰ�ʹ�.
            agent.isStopped = true;
            state = State.Attack;
        }
    }

    private void UpdateAttack()
    {
    }
}
