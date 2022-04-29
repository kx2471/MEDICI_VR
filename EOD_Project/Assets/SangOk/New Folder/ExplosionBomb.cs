using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionBomb : MonoBehaviour
{

    public Image image;
    List<GameObject> explosionObject = new List<GameObject>();
    public float explodeDamage = 50;
    public float explodeRadius = 3;
    public GameObject explosion;
    public GameObject flame;
    public Vector3 randomVector;
    public float randomX;
    public float randomY;
    public float randomZ;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {

            StartCoroutine(Explosion());
            ObjectCollect(10);
            foreach (GameObject obj in explosionObject)
            {
                if (obj.GetComponent<Rigidbody>() == true)
                {
                    if (obj.transform.name == "Player")
                    {
                        obj.GetComponent<CharacterController>().enabled = false;
                    }
                    obj.GetComponent<Rigidbody>().isKinematic = false;
                    obj.GetComponent<Rigidbody>().AddExplosionForce(explodeDamage, transform.position, 5, explodeRadius, ForceMode.Impulse);
                    obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }
            StartCoroutine(FadeOutOver());
            
            
        }
    }


    IEnumerator Explosion()
    {
        randomX = Random.Range(-5f, 5f);
        randomY = Random.Range(-5f, 5f);
        randomZ = Random.Range(-5f, 5f);
        randomVector = new Vector3(randomX, randomY, randomZ);
        Vector3 localposition = transform.position;
        Instantiate(flame, localposition, transform.rotation);
        Instantiate(explosion, transform.position, transform.rotation);

        yield return new WaitForSeconds(1.5f);

        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(flame, localposition + randomVector, transform.rotation);

        yield return new WaitForSeconds(1.5f);

        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(flame, localposition - randomVector, transform.rotation);
    }

    void ObjectCollect(float distance)
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, distance);

        foreach(Collider coll in colls)
        {
            explosionObject.Add(coll.gameObject);
        }
        for(int i = 0; i < colls.Length; i++)
        print(colls[i].transform.name);
    }
    IEnumerator FadeOutOver()
    {
        image.gameObject.SetActive(true);
        float alphaCount = 0;
        while (alphaCount < 1.0f)
        {
            alphaCount += 0.005f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, alphaCount);

        }
    }

}
