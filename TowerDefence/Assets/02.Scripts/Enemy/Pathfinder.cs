using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    // 단순히 경로탐색 알고리즘 연산용 좌표 시스템 (배열 인덱스 접근용)
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
    /// 옵션에 따라서 시작지점부터 끝지점까지 최적화된 경로를 연산하고 반환하는 함수
    /// </summary>
    /// <param name="start">시작지점</param>
    /// <param name="end">끝지점</param>
    /// <param name="optimizedPath">최적화된 경로</param>
    /// <returns>탐색 성공 여부</returns>
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
    /// 미리 정해진 경로들 중에서 시작지점과 끝지점이 일치하는 경로를 선택하는 함수
    /// </summary>
    /// <param name="start">시작지점</param>
    /// <param name="end">끝지점</param>
    /// <returns>경로 선택 성공 여부</returns>
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
