using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    [SerializeField] private GameObject otherTunnelEntrance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("J'entre dans le tunnel" + other.gameObject.tag);

        if (other.gameObject.CompareTag("Player"))
        {
           
            other.transform.position = otherTunnelEntrance.transform.position;
        }
    }
}
