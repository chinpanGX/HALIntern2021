using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree<T> where T : IComparable<T>
{
    private Node _root = null;

    public void Insert(T value)
    {
        if (_root == null)
        {
            _root = new Node(value, null);
            return;
        }

        Node node = _root;
        while (true)
        {
            if (node.Value.CompareTo(value) > 0)
            {
                if (node.Left == null)
                {
                    node.Left = new Node(value, node);
                    return;
                }
                node = node.Left;
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new Node(value, node);
                    return;
                }
                node = node.Right;
            }
        }
    }

    public class Node
    {
        public T Value { get; set; }
        public Node Parent { get; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(T value, Node parent)
        {
            Value = value;
            Parent = parent;
        }

        /// <summary>
        /// このノード以下の部分木中で最大の要素
        /// </summary>
        public Node Max
        {
            get
            {
                Node node = this;
                while (node.Right != null)
                    node = node.Right;
                return node;
            }
        }
    }
}