using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
***************************************************************************************
*	Title: AI in Unity Turorial. Behavior Trees.
*	Author: GameDevChef
*   Date: 22.05., 2022
*	Code version: 1.0
*	Availability: https://github.com/GameDevChef/BehaviourTrees
*
***************************************************************************************/

public class Cover : MonoBehaviour
{
    [SerializeField] private Transform[] coverspots;

    public Transform[] GetCoverSpots()
    {
        return coverspots;
    }
}
