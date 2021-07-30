//using System;
using System.Collections.Generic;
using UnityEngine;

public class BspTree
{
    //Basic constraints for rectangle
    public RectInt container;
    public RectInt room;
    public BspTree left;
    public BspTree right;

    //Constructor for container
    public BspTree(RectInt container)
    {
        this.container = container;
    }

    //Checks to see if leaf is leaf has empty spaces
    public bool what_is_leaf()
    {
        return left == null && right == null;
    }

    //Checks if internal has room
    public bool what_is_internal()
    {
        return left != null || right != null;
    }

    //Split function
    internal static BspTree Split(int iterations, RectInt container)
    {
        var node = new BspTree(container);
        if (iterations == 0)
            return node;

        var divContainers = SplitContainerNoRatio(container);
        node.left = Split(iterations - 1, divContainers[0]);
        node.right = Split(iterations - 1, divContainers[1]);
        return node;
    }
    static float W_RATIO = 0.45f, H_RATIO = 0.45f;
    private static RectInt[] SplitContainerNoRatio(RectInt container)
    {
        RectInt r1, r2;
        if (Random.Range(0f, 1f) > 0.5f)
        {
            // Vertical
            r1 = new RectInt(
                container.x, container.y,             // r1.x, r1.y
                Random.Range(1, container.width), container.height   // r1.w, r1.h
            );
            r2 = new RectInt(
                container.x + r1.width, container.y,      // r2.x, r2.y
                container.width - r1.width, container.height       // r2.w, r2.h
            );

            float r1_w_ratio = r1.width / (float)r1.height;
            float r2_w_ratio = r2.width / (float)r2.height;
            if (r1_w_ratio < W_RATIO || r2_w_ratio < W_RATIO)
            {
                return SplitContainerNoRatio(container);
            }

        }
        else
        {
            // Horizontal
            r1 = new RectInt(
                container.x, container.y,             // r1.x, r1.y
                container.width, Random.Range(1, container.height)   // r1.w, r1.h
            );
            r2 = new RectInt(
              container.x, container.y + r1.height,      // r2.x, r2.y
              container.width, container.height - r1.height       // r2.w, r2.h
          );

            float r1_h_ratio = r1.height / (float)r1.width;
            float r2_h_ratio = r2.height / (float)r2.width;
            if (r1_h_ratio < H_RATIO || r2_h_ratio < H_RATIO)
            {
                return SplitContainerNoRatio(container);
            }
        }
        return new RectInt[2] { r1, r2 };
    }

    //Split container
    private static RectInt[] SplitContainer(RectInt container)
    {
        RectInt r1, r2;
        if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
        {
            //Vertical
            r1 = new RectInt(container.x, container.y, container.width, (int)UnityEngine.Random.Range(container.height * 0.3f, container.height * 0.5f));
            r2 = new RectInt(container.x, container.y + r1.height, container.width, container.height - r1.height);
        }
        else
        {
            //horizontal 
            r1 = new RectInt(container.x, container.y, (int)UnityEngine.Random.Range(container.height * 0.3f, container.height * 0.5f), container.height);
            r2 = new RectInt(container.x + r1.width, container.y, container.width - r1.width, container.height);
        }
        return new RectInt[] { r1, r2 };
    }
    //Spawn point for room
    public static void GenerateNode(BspTree node)
    {
        if (node.what_is_leaf())
        {
            var randomX = UnityEngine.Random.Range(MapGen.MIN_ROOM_DELTA, node.container.width / 4);
            var randomY = UnityEngine.Random.Range(MapGen.MIN_ROOM_DELTA, node.container.height / 4);
            int roomX = node.container.x + randomX;
            int roomY = node.container.y + randomY;
            int roomW = node.container.width - (int)(randomX * UnityEngine.Random.Range(1f, 2f));
            int roomH = node.container.height - (int)(randomY * UnityEngine.Random.Range(1f, 2f));
            node.room = new RectInt(roomX, roomY, roomW, roomH);
        }
        else
        {
            if (node.left != null)
            {
                GenerateNode(node.left);
            }
            if (node.right != null)
            {
                GenerateNode(node.right);
            }
        }
    }
}
