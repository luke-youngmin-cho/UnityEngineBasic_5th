using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    // �ܼ��� ���Ž�� �˰��� ����� ��ǥ �ý��� (�迭 �ε��� ���ٿ�)
    struct Coord
    {
        public static Coord Zero = new Coord(0, 0);
        public static Coord Error = new Coord(-1, -1);
        public int Y, X;

        public Coord(int y, int x)
        {
            Y = y;
            X = x;
        }

        public static bool operator ==(Coord op1, Coord op2)
            => (op1.X == op2.X) && (op1.Y == op2.Y);

        public static bool operator !=(Coord op1, Coord op2)
            => !(op1 == op2);
    }

    enum NodeType
    {
        None,
        Path,
        Obstacle
    }

    public enum FindingOption
    {
        FixedPoints,
        DFS,
        BFS,
    }
    [SerializeField] private FindingOption _option;

    struct MapNode
    {
        public Transform Point;
        public Coord Coord;
        public NodeType Type;
    }
    private static MapNode[,] _map;
    private static bool[,] _visited;
    private static int[,] _direction = new int[2, 4]
    {
        {-1, 0, 1, 0 }, // Y
        { 0,-1, 0, 1 }  // X
    };

    private static List<Transform> _path;
    private static List<Transform> _path_DFS;

    private static Transform _leftBottom;
    private static Transform _rightTop;
    private static float _nodeDistance = 1.0f; // ��� ����
    private static float _width => _rightTop.position.x - _leftBottom.position.x;
    private static float _height => _rightTop.position.z - _leftBottom.position.z;

    /// <summary>
    /// �ɼǿ� ���� ������������ ���������� ����ȭ�� ��θ� �����ϰ� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="start">��������</param>
    /// <param name="end">������</param>
    /// <param name="optimizedPath">����ȭ�� ���</param>
    /// <returns>Ž�� ���� ����</returns>
    public bool TryFindOptimizedPath(Transform start, Transform end, out List<Transform> optimizedPath)
    {
        bool success = false;
        optimizedPath = null;

        switch (_option)
        {
            case FindingOption.FixedPoints:
                {
                    success = SetPathWithFixedPathPoints(start, end);
                    if (success)
                        optimizedPath = _path;
                }
                break;
            case FindingOption.DFS:
                {
                    success = DFS(GetCoord(start), GetCoord(end));
                    if (success)
                        optimizedPath = _path_DFS;
                }
                break;
            case FindingOption.BFS:
                break;
            default:
                break;
        }

        return success;
    }

    /// <summary>
    /// �� ������ ����
    /// </summary>
    /// <param name="pathPoints">�������� �ִ� �� ���� ���</param>
    /// <param name="obstacles">�������� ���� ���� ���</param>
    private static void SetUp(List<Transform> pathPoints, List<Transform> obstacles)
    {
        List<Transform> nodes = new List<Transform>();
        nodes.AddRange(pathPoints);
        nodes.AddRange(obstacles);

        IOrderedEnumerable<Transform> nodesFilted = nodes.OrderBy(node => node.position.x + node.position.z);

        _leftBottom = nodesFilted.First();
        _rightTop = nodesFilted.Last();

        _map = new MapNode[(int)(_height / _nodeDistance) + 1, (int)(_width / _nodeDistance) + 1];
        _visited = new bool[_map.GetLength(0), _map.GetLength(1)];

        Coord tmpCoord;
        foreach (Transform point in pathPoints)
        {
            tmpCoord = GetCoord(point);
            _map[tmpCoord.Y, tmpCoord.X] = new MapNode()
            {
                Point = point,
                Coord = tmpCoord,
                Type = NodeType.Path
            };
        }

        foreach (Transform obstacle in obstacles)
        {
            tmpCoord = GetCoord(obstacle);
            _map[tmpCoord.Y, tmpCoord.X] = new MapNode()
            {
                Point = obstacle,
                Coord = tmpCoord,
                Type = NodeType.Obstacle
            };
        }
    }



    /// <summary>
    /// �̸� ������ ��ε� �߿��� ���������� �������� ��ġ�ϴ� ��θ� �����ϴ� �Լ�
    /// </summary>
    /// <param name="start">��������</param>
    /// <param name="end">������</param>
    /// <returns>��� ���� ���� ����</returns>
    private static bool SetPathWithFixedPathPoints(Transform start, Transform end)
    {
        List<Transform> tmp;
        foreach (Paths.Path path in Paths.Instance.GetPaths())
        {
            tmp = path.GetPath();
            if (tmp[0] == start &&
                tmp[tmp.Count - 1] == end)
            {
                _path = tmp;
                return true;
            }
        }
        return false;
    }

    private static bool DFS(Coord start, Coord end)
    {
        _path_DFS = new List<Transform>();
        bool success = DFSLoop(start, end);
        if (success)
        {
            _path_DFS.Add(GetPoint(start));
            _path_DFS.Reverse();
            _path_DFS.Add(GetPoint(end));
        }
        return success;
    }

    private static bool DFSLoop(Coord start, Coord end)
    {
        bool success = false;
        _visited[start.Y, start.X] = true;

        Coord next = Coord.Error;
        for (int i = 0; i < _direction.GetLength(1); i++)
        {
            next.Y = start.Y + _direction[0, i];
            next.X = start.X + _direction[1, i];

            // Ž����ġ�� ���� ������� 
            if ((next.Y < 0 || next.Y >= _map.GetLength(0)) ||
                (next.X < 0 || next.X >= _map.GetLength(1)))
                continue;

            // Ž�� ��ġ�� ��ֹ�����
            if (_map[next.Y, next.X].Type == NodeType.Obstacle)
                continue;

            // �湮 �ߴ���
            if (_visited[next.Y, next.X])
                continue;

            // ���� ����
            if (next == end)
            {
                return true;
            }
            else
            {
                success = DFSLoop(next, end);
                if (success)
                {
                    _path_DFS.Add(GetPoint(next));
                    break;
                }
            }
        }

        return success;
    }

    private static Transform GetPoint(Coord coord) => _map[coord.Y, coord.X].Point;

    private static Coord GetCoord(Transform point)
    {
        return new Coord(Mathf.RoundToInt((point.position.z - _leftBottom.position.z) / _nodeDistance),
                         Mathf.RoundToInt((point.position.x - _leftBottom.position.x) / _nodeDistance));
    }
}
