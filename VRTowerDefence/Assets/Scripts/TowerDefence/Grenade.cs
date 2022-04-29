using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Grenade : MonoBehaviour
{
    // ��򰡿� �ε����� �ı��ǰ�ʹ�.
    // ���߰��忡�� ����ȿ���� �����ؼ� ����ġ�� ��ġ�ϰ�ʹ�.
    // �ݰ� 5M ���������� ����Ű��ʹ�. 
    // ������ ����� Enemy��
    public GameObject explosionFactory;
    public float radius = 5;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Grenade"))
        {
            return;
        }

        GameObject exp = Instantiate(explosionFactory);
        exp.transform.position = transform.position;

        // �ݰ� 5M ���������� ����Ű��ʹ�. 
        // ������ ����� Enemy��
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].gameObject.name.Contains("Enemy"))
            {
                cols[i].gameObject.GetComponent<NavMeshAgent>().enabled = false;
                Rigidbody rb = cols[i].gameObject.AddComponent<Rigidbody>();
                rb.AddExplosionForce(50, transform.position, radius, 10, ForceMode.Impulse);
                Destroy(cols[i].gameObject, 3);
            }
        }


        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
