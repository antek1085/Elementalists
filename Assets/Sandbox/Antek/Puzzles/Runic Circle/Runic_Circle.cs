using System;
using System.Collections;
using UnityEngine;

public class Runic_Circle : MonoBehaviour
{
    Quaternion rotation;
    float distance;
    float powerOfForce = 9000f;
    [SerializeField] GameObject middleObjectOfCircle;
    [SerializeField] GameObject flowerObj;
    MeshCollider collider;
    
    static bool didPlayerMakeMistake = false;
    void Awake()
    {
        collider = GetComponent<MeshCollider>();
        rotation = middleObjectOfCircle.transform.rotation;
    }


    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.transform.tag == "Player")
        {
            middleObjectOfCircle.transform.LookAt(other.transform.position, Vector3.up);
            rotation = middleObjectOfCircle.transform.rotation;
            rotation.x = 0f;
            rotation.z = 0f;
            middleObjectOfCircle.transform.rotation = rotation;

            distance = Vector3.Distance(middleObjectOfCircle.transform.position, other.transform.position);


            switch (distance)
            {
                case < 3:
                    powerOfForce = 9000f;
                    break;
                case > 10:
                    powerOfForce = 6000f;
                    break;
            }
            
            didPlayerMakeMistake = true;
            other.transform.GetComponent<Rigidbody>().AddForce(middleObjectOfCircle.transform.forward * powerOfForce);
        }
    }

    void Update()
    {
        if (didPlayerMakeMistake)
        {
            didPlayerMakeMistake = false;
            StartCoroutine(DisableTrigger());
        }
        if (flowerObj == null)
        {
            collider.enabled = false;
            gameObject.transform.GetComponent<MeshRenderer>().enabled = false;
            this.enabled = false;
        }
    }

    IEnumerator DisableTrigger()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(middleObjectOfCircle.transform.position, 10);
        Gizmos.DrawWireSphere(middleObjectOfCircle.transform.position, 6);
    }
}
