using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StreetLampCycle", menuName = "StreetLampCycle", order = 1)]
public class StreetLampCycle : ScriptableObject
{
    [SerializeField] public GameObject streetLamp;
}
