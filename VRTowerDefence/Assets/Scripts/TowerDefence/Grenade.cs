using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Grenade : MonoBehaviour
{
    // 어딘가에 부딪히면 파괴되고싶다.
    // 폭발공장에서 폭발효과를 생성해서 내위치에 배치하고싶다.
    // 반경 5M 범위폭발을 일으키고싶다. 
    // 폭발의 대상은 Enemy만
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

        // 반경 5M 범위폭발을 일으키고싶다. 
        // 폭발의 대상은 Enemy만
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
