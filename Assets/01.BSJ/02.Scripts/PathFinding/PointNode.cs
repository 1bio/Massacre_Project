using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PointNode
{
    public Vector3 Position { get => position; }
    public PointNode Parent { get => parent; set => parent = value; }
    public float GCost { get => gCost; set => gCost = value; }
    public float HCost { get => hCost; set => hCost = value; }
    public float FCost { get => fCost; set => fCost = value; }
    public bool IsObstacle { get => isObstacle; }
    public bool IsGround { get => isGround; }

    [SerializeField] private Vector3 position;
    private PointNode parent;
    private float gCost, hCost;
    private float fCost;
    private bool isObstacle;
    private bool isGround;

    public PointNode(Vector3 positions, bool isObstacle, bool isGround)
    {
        this.position = positions;
        this.isObstacle = isObstacle;
        this.isGround = isGround;

        gCost = 0;
        hCost = 0;
        fCost = 0;
    }

    public void Initialize()
    {
        gCost = 0;
        hCost = 0;
        fCost = 0;

        parent = null;
    }
}
