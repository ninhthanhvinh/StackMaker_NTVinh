using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetableBrick : MonoBehaviour
{
    [SerializeField] GameObject brick;
    bool hasBrick = true;
    private void OnTriggerEnter(Collider other)
    {
        if (hasBrick && other.gameObject.CompareTag("Player"))
        {
            //Add brick to player
            other.GetComponent<BrickControl>().AddBrick();
            hasBrick = false;
            //Destroy brick
            brick.SetActive(false);
        }
    }
}
