using System;
using System.Collections;
using UnityEngine;

public class Runic_Circle : MonoBehaviour
{
    Quaternion rotation;
    float distance;
    float powerOfForce = 9000f;
    [SerializeField] GameObject middleObjectOfCircle;
    BoxCollider collider;
    
    static bool didPlayerMakeMistake = false;
    void Awake()
    {
        collider = GetComponent<BoxCollider>();
        rotation = middleObjectOfCircle.transform.rotation;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            middleObjectOfCircle.transform.LookAt(other.transform.parent.position, Vector3.up);
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
            other.transform.parent.GetComponent<Rigidbody>().AddForce(middleObjectOfCircle.transform.forward * powerOfForce);
        }
    }

    void Update()
    {
        if (didPlayerMakeMistake)
        {
            didPlayerMakeMistake = false;
            StartCoroutine(DisableTrigger());
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
