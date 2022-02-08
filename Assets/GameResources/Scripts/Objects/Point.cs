using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Color white = default;
    [SerializeField] private Color black = default;

    private Material material = null;
    private PointPosition pointPosition = null;

    public PointPosition PointPosition
    {
        get
        {
            return pointPosition;
        }
    }

    public void Init(int width, int height)
    {
        pointPosition = new PointPosition(width, height);
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    public void ChangeState(bool isBlack)
    {
        material.color = isBlack ? black : white;
    }
}