using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableGround : MonoBehaviour
{
    [SerializeField] Transform whereToPlaceBrick;
    bool isPlaced = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlaced)
        {
            if (other.GetComponent<BrickControl>().PlaceBrick(whereToPlaceBrick))
            {
                isPlaced = true;
            }    
        }
    }
}
