using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SceneLinks : MonoBehaviour
{
    [SerializeField] private Transform location = null;

    public Transform Location
    {
        get
        {
            return location;
        }
    }
}