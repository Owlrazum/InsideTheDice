using UnityEngine;

public static class PlayerLookUpTables
{
    // first dimension is currentSide
    // second dimension is one of four posslibe up states;
    // up States are rotated clockWise, if viewed from inside cube;
    // the starting looks at backward side
    // third dimension is switch type
    public static int[,,] SideConnections =
    {
        {
            { 3, 2, 1, 4, 5},
            { 4, 5, 1, 2, 3},
            { 2, 3, 1, 5, 4},
            { 5, 4, 1, 3, 2}
        },
        {
            { 3, 2, 0, 5, 4},
            { 5, 4, 0, 2, 3},
            { 2, 3, 0, 4, 5},
            { 4, 5, 0, 3, 2}
        },
        {
            { 0, 1, 3, 4, 5},
            { 4, 5, 3, 1, 0},
            { 1, 0, 3, 5, 4},
            { 5, 4, 3, 0, 1}
        },
        {
            { 0, 1, 2, 5, 4},
            { 5, 4, 2, 1, 0},
            { 1, 0, 2, 4, 5},
            { 4, 5, 2, 0, 1}
        },
        {
            { 0, 1, 5, 3, 2},
            { 3, 2, 5, 1, 0},
            { 1, 0, 5, 2, 3},
            { 2, 3, 5, 0, 1}
        },
        {
            { 0, 1, 4, 2, 3},
            { 2, 3, 4, 1, 0},
            { 1, 0, 4, 3, 2},
            { 3, 2, 4, 0, 1}
        }
    };

    // same as SideConnections
    public static int[,,] UpChange =
    {
        { // bot
            { 2, 0, 2, 3, 1},
            { 2, 0, 3, 3, 1},
            { 2, 0, 0, 3, 1},
            { 2, 0, 1, 3, 1} 
        },
        { // top
            { 0, 2, 2, 3, 1},
            { 0, 2, 3, 3, 1},
            { 0, 2, 0, 3, 1},
            { 0, 2, 1, 3, 1}
        },
        { // back
            { 0, 2, 2, 0, 0},
            { 1, 1, 1, 1, 3},
            { 0, 2, 0, 2, 2},
            { 3, 3, 3, 3, 1} 
        },
        { // forw
            { 2, 0, 2, 0, 0},
            { 1, 1, 1, 3, 1},
            { 2, 0, 0, 2, 2},
            { 3, 3, 3, 1, 3}
        },
        { // left
            { 3, 3, 2, 0, 0},
            { 1, 1, 1, 0, 0},
            { 1, 1, 0, 2, 2},
            { 3, 3, 3, 2, 2}
        },
        {
            { 1, 1, 2, 0, 0},
            { 1, 1, 1, 2, 2},
            { 3, 3, 0, 2, 2},
            { 3, 3, 3, 0, 0}
        }
    };

    public static Quaternion[,] Rotations =
    {
        { // bot
            Quaternion.LookRotation(Vector3.up, -Vector3.forward),
            Quaternion.LookRotation(Vector3.up, Vector3.right),
            Quaternion.LookRotation(Vector3.up, Vector3.forward),
            Quaternion.LookRotation(Vector3.up, -Vector3.right)
        },
        { // top
            Quaternion.LookRotation(-Vector3.up, -Vector3.forward),
            Quaternion.LookRotation(-Vector3.up, Vector3.right),
            Quaternion.LookRotation(-Vector3.up, Vector3.forward),
            Quaternion.LookRotation(-Vector3.up, -Vector3.right)
        },
        { // back
            Quaternion.LookRotation(-Vector3.forward, Vector3.up),
            Quaternion.LookRotation(-Vector3.forward, Vector3.right),
            Quaternion.LookRotation(-Vector3.forward, -Vector3.up),
            Quaternion.LookRotation(-Vector3.forward, -Vector3.right)
        },
        { // forw
            Quaternion.LookRotation(Vector3.forward, Vector3.up),
            Quaternion.LookRotation(Vector3.forward, -Vector3.right),
            Quaternion.LookRotation(Vector3.forward, -Vector3.up),
            Quaternion.LookRotation(Vector3.forward, Vector3.right)
        },
        { // left
            Quaternion.LookRotation(-Vector3.right, Vector3.up),
            Quaternion.LookRotation(-Vector3.right, -Vector3.forward),
            Quaternion.LookRotation(-Vector3.right, -Vector3.up),
            Quaternion.LookRotation(-Vector3.right, Vector3.forward)
        },
        { // right
            Quaternion.LookRotation(Vector3.right, Vector3.up),
            Quaternion.LookRotation(Vector3.right, Vector3.forward),
            Quaternion.LookRotation(Vector3.right, -Vector3.up),
            Quaternion.LookRotation(Vector3.right, -Vector3.forward)
        }
    };
}