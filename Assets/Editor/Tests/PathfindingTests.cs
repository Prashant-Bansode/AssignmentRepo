// using NUnit.Framework;
// using UnityEngine;
// using System.Collections.Generic;

// [TestFixture]
// public class PathfindingTests
// {
//     private GridManager gridManager;
//     private Pathfinding pathfinding;
//     private ObstacleData obstacleData;

//     [SetUp]
//     public void SetUp()
//     {
//         gridManager = new GameObject().AddComponent<GridManager>();
//         pathfinding = new GameObject().AddComponent<Pathfinding>();
//         obstacleData = ScriptableObject.CreateInstance<ObstacleData>();

//         gridManager.gridSize = 10;
//         obstacleData.obstacleGrid = new bool[100]; // Initialize with no obstacles
//         pathfinding.gridManager = gridManager;
//         pathfinding.obstacleData = obstacleData;

//         pathfinding.InitializeNodes();
//     }

//     [TearDown]
//     public void TearDown()
//     {
//         Object.DestroyImmediate(gridManager.gameObject);
//         Object.DestroyImmediate(pathfinding.gameObject);
//         ScriptableObject.DestroyImmediate(obstacleData);
//     }

//     [Test]
//     public void TestPathfinding_NoObstacles()
//     {
//         Vector3 startPos = new Vector3(0, 0, 0);
//         Vector3 targetPos = new Vector3(9, 0, 9);

//         List<Vector3> path = pathfinding.FindPath(startPos, targetPos);

//         Assert.IsNotNull(path);
//         Assert.AreEqual(new Vector3(0, 0, 0), path[0]);
//         Assert.AreEqual(new Vector3(9, 0, 9), path[path.Count - 1]);
//     }

//     [Test]
//     public void TestPathfinding_WithObstacles()
//     {
//         // Set some obstacles
//         obstacleData.obstacleGrid[11] = true; // Block position (1, 1)
//         obstacleData.obstacleGrid[12] = true; // Block position (1, 2)

//         pathfinding.InitializeNodes(); // Reinitialize nodes with obstacles

//         Vector3 startPos = new Vector3(0, 0, 0);
//         Vector3 targetPos = new Vector3(9, 0, 9);

//         List<Vector3> path = pathfinding.FindPath(startPos, targetPos);

//         Assert.IsNotNull(path);
//         Assert.AreEqual(new Vector3(0, 0, 0), path[0]);
//         Assert.AreEqual(new Vector3(9, 0, 9), path[path.Count - 1]);
//         Assert.IsFalse(path.Contains(new Vector3(1, 0, 1))); // Ensure obstacle position is not in the path
//         Assert.IsFalse(path.Contains(new Vector3(1, 0, 2))); // Ensure obstacle position is not in the path
//     }

//     [Test]
//     public void TestPathfinding_NoBackwardMovement()
//     {
//         Vector3 startPos = new Vector3(0, 0, 0);
//         Vector3 targetPos = new Vector3(9, 0, 9);

//         List<Vector3> path = pathfinding.FindPath(startPos, targetPos);

//         Assert.IsNotNull(path);

//         // Ensure no backward movement
//         for (int i = 1; i < path.Count; i++)
//         {
//             Vector3 previous = path[i - 1];
//             Vector3 current = path[i];
//             Assert.IsTrue((current.x >= previous.x && current.z >= previous.z) || 
//                           (current.x <= previous.x && current.z <= previous.z) ||
//                           (current.x >= previous.x && current.z <= previous.z) ||
//                           (current.x <= previous.x && current.z >= previous.z));
//         }
//     }
// }
