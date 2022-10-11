using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBinaryTree
{
    class MyBinaryTree<T>
    {
        // Inner class : 클래스의 멤버 클래스
        public class Node<K>
        {
            public K Value;
            public Node<K> Left;
            public Node<K> Right;
        }
        private Node<T> _root, _tmp, _tmp2;

        // O(LogN)
        public void Add(T item)
        {
            if (_root != null)
            {
                _tmp = _root;
                while (_tmp != null)
                {
                    // 삽입하려는 값이 현재 탐색중인 노드의 값보다 작을때
                    if (Comparer<T>.Default.Compare(item, _tmp.Value) < 0)
                    {
                        // 왼쪽 노드 있으면 왼쪽으로 이동
                        if (_tmp.Left != null)
                        {
                            _tmp = _tmp.Left;
                        }
                        // 여기가 내자리다 
                        else
                        {
                            _tmp.Left = new Node<T>();
                            _tmp.Left.Value = item;
                            return;
                        }
                    }
                    // 삽입하려는 값이 현재 탐색중인 노드의 값보다 클 때
                    else if(Comparer<T>.Default.Compare(item, _tmp.Value) > 0)
                    {
                        // 오른쪽 노드 있으면 오른쪽으로 이동
                        if (_tmp.Right != null)
                        {
                            _tmp = _tmp.Right;
                        }
                        // 여기가 내자리다 
                        else
                        {
                            _tmp.Right = new Node<T>();
                            _tmp.Right.Value = item;
                            return;
                        }
                    }
                    // 이미 값이 존재함
                    else
                    {
                        throw new Exception("해당 값의 노드가 이미 존재함");
                    }
                }
            }
            else
            {
                _root = new Node<T>();
                _root.Value = item;
            }
        }

        // O(LogN)
        public Node<T> Find(T item)
        {
            if (_root != null)
            {
                _tmp = _root;

                while (_tmp != null)
                {
                    // 작다
                    if (Comparer<T>.Default.Compare(item, _tmp.Value) < 0)
                        _tmp = _tmp.Left;
                    // 크다
                    else if (Comparer<T>.Default.Compare(item, _tmp.Value) < 0)
                        _tmp = _tmp.Right;
                    // 찾았다
                    else
                        return _tmp;
                }
            }
            return null;
        }

        //
        public bool Remove(T item)
        {
            if (_root != null)
            {
                _tmp = _root;

                while (_tmp != null)
                {
                    // 작다
                    if (Comparer<T>.Default.Compare(item, _tmp.Value) < 0)
                    {
                        _tmp = _tmp.Left;
                        _tmp2 = _tmp;
                    }
                    // 크다
                    else if (Comparer<T>.Default.Compare(item, _tmp.Value) < 0)
                    {
                        _tmp = _tmp.Right;
                        _tmp2 = _tmp;
                    }
                    // 찾았다
                    else
                        break;
                }
            }
            else
            {
                return false;
            }

            if (_tmp != null)
            {
                if (Comparer<T>.Default.Compare(_tmp.Value, _tmp2.Value) < 0)
                    _tmp2.Left = null;
                else if (Comparer<T>.Default.Compare(_tmp.Value, _tmp2.Value) > 0)
                    _tmp2.Right = null;

                // 자식이 둘다 없을 때
                if (_tmp.Left == null && _tmp.Right == null)
                {
                    _tmp = null;
                }
                // 오른쪽 자식만 있을 때
                else if (_tmp.Left == null && _tmp.Right != null)
                {
                    _tmp = _tmp.Right;
                }
                // 오른쪽 자식만 있을 때
                else if (_tmp.Left != null && _tmp.Right == null)
                {
                    _tmp = _tmp.Left;
                }
                // 자식이 둘 다 있을때
                else
                {
                    _tmp2 = _tmp.Right;
                    while (_tmp2.Left != null)
                    {
                        _tmp2 = _tmp2.Left;

                        if (_tmp2.Left == null &&
                            _tmp2.Right != null)
                            _tmp2 = _tmp2.Right;
                    }
                    _tmp2.Left = _tmp.Left;
                    _tmp2.Right = _tmp.Right;
                    _tmp = _tmp2;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
