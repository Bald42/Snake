using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Color white = default;
    [SerializeField] private Color black = default;

    private Material material = null;

    public void Init()
    {
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    public void ChangeState(bool isBlack)
    {
        material.color = isBlack ? black : white;
    }
}