using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedTrapPrefab;

    void OnCollisionEnter(Collision collision)
    {
        GameObject destroyedTrap = Instantiate(destroyedTrapPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(destroyedTrap, 3f);
    }
}
