// using UnityEditor;
// using UnityEngine;
// using UnityEngine.UI;

// public class InitBeizerSegments : EditorWindow
// {
//     private float _heightOffset = 0;
//     private float _lengthOffset = 100;

//     private float _centerHeightOffset = 0;
//     private float _centerLegnthOffset = 0;

//     private Transform _parent;

//     [MenuItem("Window/Custom/InitBeizerSegments")]
//     private static void Init()
//     {
//         InitBeizerSegments window = 
//             (InitBeizerSegments)EditorWindow.GetWindow(
//                 typeof(InitBeizerSegments));

//         window.Show();
//     }

//     private void OnGUI()
//     {
//         _heightOffset = EditorGUILayout.FloatField("HeightOffset", _heightOffset);
//         _lengthOffset = EditorGUILayout.FloatField("LengthOffset", _lengthOffset);
//         _centerHeightOffset = EditorGUILayout.FloatField("CenterHeightOffset", _centerHeightOffset);
//         _centerLegnthOffset = EditorGUILayout.FloatField("CenterLegnthOffset", _centerLegnthOffset);

//         _parent = (Transform)EditorGUILayout.ObjectField("Parent", _parent, typeof(Transform), true);

//         if (GUILayout.Button("Init"))
//         {
//             InitBeizer();
//         }
//     }

//     private void InitBeizer()
//     {
//         InitBottoms();
//     }

//     private void InitBottoms()
//     {
//         for (int side = 0; side < 6; side++)
//         {
//             for (int switchType = 0; switchType < 5; switchType++)
//             { 
//                 InitSegment(_parent.GetChild(side).GetChild(switchType), side, switchType);
//             }
//         }
//     }

//     private void InitSegment(Transform segment, int side, int switchType)
//     {
//         if (switchType == 2 || switchType == 3)
//         {
//             InitForwardSegment(segment, side, switchType);
//             return;
//         }
//         else
//         { 
//             InitForwardSegment(segment, side, switchType);
//         }
//     }

//     private void InitCornerSegment(Transform segment, int side, int switchType)
//     {
//         PlayerLookUpTables.SideConnections[side, 0, switchType]
//     }

//     private void InitForwardSegment(Transform segment, int side, int switchType)
//     { 

//     }

//     private RectTransform CreateTilesContainer(string name, Transform totalWindowTransform)
//     { 
//         GameObject gb = new GameObject(name, typeof(RectTransform), typeof(UIWindowCraft));
//         gb.transform.SetParent(totalWindowTransform, false);
//         gb.TryGetComponent(out RectTransform tilesParentRect);
//         tilesParentRect.anchorMin = Vector2.zero;
//         tilesParentRect.anchorMax = Vector2.one;
//         tilesParentRect.sizeDelta = -_inventoryWindowBorderWidth;

//         return gb.GetComponent<RectTransform>();
//     }

//     private RectTransform CreateItemsContainer(string name, Transform totalWindowTransform)
//     { 
//         GameObject gb = new GameObject(name, typeof(RectTransform));
//         gb.transform.SetParent(totalWindowTransform, false);
//         gb.TryGetComponent(out RectTransform tilesParentRect);
//         tilesParentRect.anchorMin = Vector2.zero;
//         tilesParentRect.anchorMax = Vector2.one;
//         tilesParentRect.sizeDelta = -_inventoryWindowBorderWidth;

//         return gb.GetComponent<RectTransform>();
//     }
// }