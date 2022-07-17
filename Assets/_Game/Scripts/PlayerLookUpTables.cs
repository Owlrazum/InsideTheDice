using UnityEngine;

public static class PlayerLookUpTables
{
    // side up switchType
    // first dimension is currentSide
    // second dimension is one of four posslibe up states;
    // up States are rotated clockWise, if viewed from inside cube;
    // the starting looks at backward side
    // third dimension is switch type
    public static int[,,] SideConnections =
    {
        { // bot
            { 3, 2, 1, 4, 5},
            { 4, 5, 1, 2, 3},
            { 2, 3, 1, 5, 4},
            { 5, 4, 1, 3, 2}
        },
        { // top
            { 3, 2, 0, 5, 4},
            { 5, 4, 0, 2, 3},
            { 2, 3, 0, 4, 5},
            { 4, 5, 0, 3, 2}
        },
        { // back
            { 0, 1, 3, 4, 5},
            { 4, 5, 3, 1, 0},
            { 1, 0, 3, 5, 4},
            { 5, 4, 3, 0, 1}
        },
        { // forw
            { 0, 1, 2, 5, 4},
            { 5, 4, 2, 1, 0},
            { 1, 0, 2, 4, 5},
            { 4, 5, 2, 0, 1}
        },
        { // left
            { 0, 1, 5, 3, 2},
            { 3, 2, 5, 1, 0},
            { 1, 0, 5, 2, 3},
            { 2, 3, 5, 0, 1}
        },
        { // right
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
            { 2, 0, 2, 1, 3},
            { 2, 0, 3, 1, 3},
            { 2, 0, 0, 1, 3},
            { 2, 0, 1, 1, 3}
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

    public static int[,,] BeizerSegments =
    {
        { // bot
            { 0, 1, 2, 3, 4},
            { 3, 4, 2, 1, 0},
            { 1, 0, 2, 4, 3},
            { 4, 3, 2, 0, 1}
        },
        { // top
            { 5, 6, 7, 8, 9},
            { 8, 9, 7, 6, 5},
            { 5, 6, 7, 9, 8},
            { 9, 8, 7, 5, 6}
        },
        { // back
            { 10, 11, 12, 13, 14},
            { 13, 14, 12, 11, 10},
            { 11, 10, 12, 14, 13},
            { 14, 13, 12, 10, 11}
        },
        { // forw
            { 15, 16, 17, 18, 19},
            { 18, 19, 17, 16, 15},
            { 16, 15, 17, 19, 18},
            { 19, 18, 17, 15, 16}
        },
        { // left
            { 20, 21, 22, 23, 24},
            { 23, 24, 22, 21, 20},
            { 21, 20, 22, 24, 23},
            { 24, 23, 22, 20, 21}
        },
        {
            { 25, 26, 27, 28, 29},
            { 28, 29, 27, 26, 25},
            { 26, 25, 27, 29, 28},
            { 29, 28, 27, 25, 26}
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
            Quaternion.LookRotation(Vector3.forward, Vector3.up),
            Quaternion.LookRotation(Vector3.forward, Vector3.right),
            Quaternion.LookRotation(Vector3.forward, -Vector3.up),
            Quaternion.LookRotation(Vector3.forward, -Vector3.right)
        },
        { // forw
            Quaternion.LookRotation(-Vector3.forward, Vector3.up),
            Quaternion.LookRotation(-Vector3.forward, -Vector3.right),
            Quaternion.LookRotation(-Vector3.forward, -Vector3.up),
            Quaternion.LookRotation(-Vector3.forward, Vector3.right)
        },
        { // left
            Quaternion.LookRotation(Vector3.right, Vector3.up),
            Quaternion.LookRotation(Vector3.right, -Vector3.forward),
            Quaternion.LookRotation(Vector3.right, -Vector3.up),
            Quaternion.LookRotation(Vector3.right, Vector3.forward)
        },
        { // right
            Quaternion.LookRotation(-Vector3.right, Vector3.up),
            Quaternion.LookRotation(-Vector3.right, Vector3.forward),
            Quaternion.LookRotation(-Vector3.right, -Vector3.up),
            Quaternion.LookRotation(-Vector3.right, -Vector3.forward)
        }
    };

    public static Vector3[,] UpDirections =
    {
        { // bot
            -Vector3.forward,
            Vector3.right,
            Vector3.forward,
            -Vector3.right
        },
        { // top
             -Vector3.forward,
             Vector3.right,
             Vector3.forward,
             -Vector3.right
        },
        { // back
             Vector3.up,
             Vector3.right,
             -Vector3.up,
             -Vector3.right
        },
        { // forw
             Vector3.up,
             -Vector3.right,
             -Vector3.up,
             Vector3.right
        },
        { // left
             Vector3.up,
             -Vector3.forward,
             -Vector3.up,
             Vector3.forward
        },
        { // right
             Vector3.up,
             Vector3.forward,
             -Vector3.up,
             -Vector3.forward
        }
    };

    public static Vector3[] SidesLocalPos =
    {
        new Vector3(0, -0.5f, 0),
        new Vector3(0, 0.5f, 0),
        new Vector3(0, 0, -0.5f),
        new Vector3(0, 0, 0.5f),
        new Vector3(-0.5f, 0, 0),
        new Vector3(0.5f, 0, 0)
    };
}