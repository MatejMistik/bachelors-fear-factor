using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField] private Transform[] coverspots;
    
    public Transform[] GetCoverSpots()
    {
        return coverspots;
    }
}
