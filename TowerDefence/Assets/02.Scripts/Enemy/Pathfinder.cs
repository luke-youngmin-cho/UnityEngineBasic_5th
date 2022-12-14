using System.Collections;
using System.Collections.Generic;
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
    private static List<Transform> _path;

    /// <summary>
    /// �ɼǿ� ���� ������������ ���������� ����ȭ�� ��θ� �����ϰ� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="start">��������</param>
    /// <param name="end">������</param>
    /// <param name="optimizedPath">����ȭ�� ���</param>
    /// <returns>Ž�� ���� ����</returns>
    public bool TryFindOptimizedPath(Transform start, Transform end, out List<Transform> optimizedPath)
    {
        bool found = false;
        optimizedPath = null;

        switch (_option)
        {
            case FindingOption.FixedPoints:
                {
                    found = SetPathWithFixedPathPoints(start, end);
                    if (found)
                        optimizedPath = _path;
                }
                break;
            case FindingOption.DFS:
                break;
            case FindingOption.BFS:
                break;
            default:
                break;
        }

        return found;
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

}
