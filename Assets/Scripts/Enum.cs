using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enum : MonoBehaviour
{
    public enum DoorStatus 
    { 
        Destroyed, 
        Builted,  
    };
    public DoorStatus doorStatus;
}
