using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ð����� �����忡�� ���� ���� �� ��ġ�� ��ġ�ϰ�ʹ�.
public class EnemyManager : MonoBehaviour
{
    public GameObject enemyFactory;
    float currentTime;
    public float createTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. �ð��� �帣�ٰ�
        currentTime += Time.deltaTime;
        // 2. ����ð��� �����ð��� �Ǹ�
        if (currentTime > createTime)
        {
            // 3. �����忡�� ���� ����
            GameObject enemy = Instantiate(enemyFactory);
            // 4. �� ��ġ�� ��ġ�ϰ�ʹ�.
            enemy.transform.position = transform.position;
            enemy.transform.forward = transform.forward;
            // 5. ����ð��� 0���� �ʱ�ȭ �ϰ�ʹ�.
            currentTime = 0;
        }

    }
}
