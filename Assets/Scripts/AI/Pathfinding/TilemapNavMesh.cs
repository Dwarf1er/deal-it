using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapNavMesh : MonoBehaviour {
    public Tilemap tilemap;
    private MeshFilter meshFilter;
    private Queue<Vector2[]> pathHistory;
    private HashSet<TilemapNavObstacle> obstacles;
    private static readonly float STEP_SIZE = 0.08f;

    private void Awake() {
        this.pathHistory = new Queue<Vector2[]>();
        this.obstacles = new HashSet<TilemapNavObstacle>();
        Mesh mesh = MakeMesh(tilemap);
        mesh = ErodeMesh(mesh);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter = (MeshFilter)gameObject.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = mesh;
        gameObject.AddComponent(typeof(MeshCollider));
    }

    private void OnDrawGizmosSelected() {
        if(meshFilter == null) return;

        while(pathHistory.Count > 3) pathHistory.Dequeue();

        Gizmos.color = Color.black;
        foreach(Vector2[] path in pathHistory) {
            foreach(Vector2 point in path) {
                Gizmos.DrawSphere(point, 0.02f);
            }
        }

        Color color = Color.green;
        color.a = 0.5f;
        Gizmos.color = color;

        Gizmos.DrawMesh(meshFilter.mesh, transform.position, transform.rotation);
    }

    private Mesh MakeMesh(Tilemap tilemap) {
        Mesh mesh = new Mesh();

        int xSize = tilemap.cellBounds.xMax - tilemap.cellBounds.xMin;
        int ySize = tilemap.cellBounds.yMax - tilemap.cellBounds.yMin;
        int xOffset = tilemap.cellBounds.xMin;
        int yOffset = tilemap.cellBounds.yMin;

        List<Vector3> vertexList = new List<Vector3>();
        List<int> triangleIndicies = new List<int>();

        for(int y = yOffset; y < yOffset + ySize; y++) {
            for(int x = xOffset; x < xOffset + xSize; x++) {
                if(!tilemap.HasTile(new Vector3Int(x, y, 0))) continue;

                Vector3 tilePosition = new Vector3(x * tilemap.cellSize.x, y * tilemap.cellSize.y, 0);
                List<Vector3> verticies = new List<Vector3>();

                foreach(Vector2 offset in new Vector2[]{Vector2.zero, Vector2.up, Vector2.right, new Vector2(1, 1)}) {
                    verticies.Add(tilePosition + new Vector3(offset.x * tilemap.cellSize.x, offset.y * tilemap.cellSize.y, 0));
                }

                List<int> vertexIndicies = new List<int>();
                foreach(Vector3 vertex in verticies) {
                    int vertexIndex = -1;

                    for(int i = 0; i < vertexList.Count; i++) {
                        if(Vector3.Distance(vertex, vertexList[i]) < 0.08f) {
                            vertexIndex = i;
                            break;
                        }
                    }

                    if(vertexIndex < 0) {
                        vertexIndex = vertexList.Count;
                        vertexList.Add(vertex);
                    }

                    vertexIndicies.Add(vertexIndex);
                }

                triangleIndicies.AddRange(new int[]{
                    vertexIndicies[0], 
                    vertexIndicies[1], 
                    vertexIndicies[2],
                    vertexIndicies[1], 
                    vertexIndicies[3], 
                    vertexIndicies[2]
                });
            }
        }

        mesh.vertices = vertexList.ToArray();
        mesh.triangles = triangleIndicies.ToArray();

        return mesh;
    }

    private int RandomTriangleIndex() {
        return Random.Range(0, meshFilter.mesh.triangles.Length / 3) * 3;
    }

    private Vector2 RandomInTriangle(int triangleIndex) {
        int[] triangles = meshFilter.mesh.triangles;
        Vector2[] verticies = new Vector2[3];

        for(int i = 0; i < 3; i++) {
            verticies[i] = meshFilter.mesh.vertices[triangles[triangleIndex + i]];
        }

        Vector2 p1 = Vector2.Lerp(verticies[0], verticies[1], Random.Range(0.0f, 1.0f));
        Vector2 p2 = Vector2.Lerp(verticies[1], verticies[2], Random.Range(0.0f, 1.0f));
        Vector2 randomPosition = Vector2.Lerp(p1, p2, Random.Range(0.0f, 1.0f));

        return randomPosition;
    }

    public Vector2 RandomPosition() {
        return RandomInTriangle(RandomTriangleIndex());
    }

    private bool OnObstacle(Vector2 position) {
        foreach(TilemapNavObstacle obstacle in obstacles) {
            if(obstacle.GetBounds().Contains(position)) return true;
        }

        return false;
    }

    private bool OnSurface(Vector3 position) {
        if(!meshFilter.mesh.bounds.Contains(position)) return false;

        return Physics.RaycastAll(position + Vector3.back * 0.1f, Vector3.forward, 0.2f).Length > 0;
    }

    public void AddObstacle(TilemapNavObstacle obstacle) {
        obstacles.Add(obstacle);
    }

    public void RemoveObstacle(TilemapNavObstacle obstacle) {
        obstacles.Remove(obstacle);
    }

    private Dictionary<int, HashSet<int>> VertexTriangles(Mesh mesh) {
        Dictionary<int, HashSet<int>> vertexTriangles = new Dictionary<int, HashSet<int>>();

        for(int i = 0; i < mesh.triangles.Length; i += 3) {
            for(int j = 0; j < 3; j++) {
                int vertexIndex = mesh.triangles[i + j];

                if(!vertexTriangles.ContainsKey(vertexIndex)) vertexTriangles[vertexIndex] = new HashSet<int>();

                vertexTriangles[vertexIndex].Add(i);
            }
        }

        return vertexTriangles;
    }

    private List<int> EdgeVerticies(Dictionary<int, HashSet<int>> vertexTriangles) {
        List<int> edgeVerticies = new List<int>();

        foreach(int vertexIndex in vertexTriangles.Keys) {
            if(vertexTriangles[vertexIndex].Count < 6) edgeVerticies.Add(vertexIndex);
        }

        return edgeVerticies;
    }

    private Mesh ErodeMesh(Mesh mesh) {
        Dictionary<int, HashSet<int>> vertexTriangles = VertexTriangles(mesh);
        List<int> edgeVerticies = EdgeVerticies(vertexTriangles);
        Mesh newMesh = new Mesh();
        Vector3[] vertices = mesh.vertices;

        foreach(int vertexIndex in edgeVerticies) {
            Vector3 averagePosition = Vector3.zero;

            foreach(int triangleIndex in vertexTriangles[vertexIndex]) {
                for(int i = 0; i < 3; i++) {
                    averagePosition += mesh.vertices[mesh.triangles[triangleIndex + i]];
                }
            }

            averagePosition /= vertexTriangles[vertexIndex].Count * 3.0f;

            Vector3 direction = (averagePosition - mesh.vertices[vertexIndex]).normalized;
            vertices[vertexIndex] += direction * 0.08f;
        }

        newMesh.vertices = vertices;
        newMesh.triangles = mesh.triangles;

        return newMesh;
    }

    public Vector2[] GetPath(Vector2 from, Vector2 to) {
        if(OnObstacle(from) || OnObstacle(to)) {
            return new Vector2[0];
        }

        PriorityQueue<Tuple<Stack<Vector2>, HashSet<Vector2>>, float> queue = new PriorityQueue<Tuple<Stack<Vector2>, HashSet<Vector2>>, float>();
        queue.Enqueue(new Tuple<Stack<Vector2>, HashSet<Vector2>>(new Stack<Vector2>(new Vector2[]{from}), new HashSet<Vector2>()), 0);

        Vector2[] finalPath = new Vector2[0];
        while(queue.Count() > 0) {
            Tuple<Stack<Vector2>, HashSet<Vector2>> pathSeen = queue.Dequeue();
            Stack<Vector2> path = pathSeen.first;
            HashSet<Vector2> seen = pathSeen.second;
            Vector2 nextNode = path.Pop();

            if(!OnSurface(nextNode) || OnObstacle(nextNode)) continue;
            if(seen.Contains(nextNode)) continue;

            if(Vector2.Distance(nextNode, to) <= STEP_SIZE) {
                path.Push(to);
                finalPath = new Stack<Vector2>(path).ToArray();
                break;
            }

            path.Push(nextNode);
            seen.Add(nextNode);

            foreach(Vector2 delta in new Vector2[]{Vector2.left, Vector2.up, Vector2.down, Vector2.right}) {
                Vector2 offset = path.Peek() + delta * STEP_SIZE;
                Stack<Vector2> nextPath = new Stack<Vector2>(new Stack<Vector2>(path));
                nextPath.Push(offset);
                queue.Enqueue(new Tuple<Stack<Vector2>, HashSet<Vector2>>(nextPath, seen), 1.0f / (Vector2.Distance(offset, to) * nextPath.Count));
            }
        }

        if(finalPath.Length > 0) {
            pathHistory.Enqueue(finalPath);
        }

        return finalPath;
    }
}
