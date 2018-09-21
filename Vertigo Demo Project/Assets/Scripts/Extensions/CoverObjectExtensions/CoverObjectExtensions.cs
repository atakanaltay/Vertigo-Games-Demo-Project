using UnityEngine;
using System.Collections.Generic;
public static class CoverObjectExtensions  {
    public static Vector2 giveLowestPositionYByIndexes(this List<int> closestIndexes)
    {
        //finding the node which has the smallest Y axis 
        int lowerYaxisIndex = closestIndexes.findTheNodeThatHasLowerYaxis();
        Vector2 downVectorPoint = NodeManager.mapNodes[closestIndexes[lowerYaxisIndex]].transform.position;
        return downVectorPoint;
                
    }
    public static Vector2 giveMiddlePointByIndexes(this List<int> closestIndexes)
    {
        List<Vector2> nodeVectors = new List<Vector2>(closestIndexes.Count);
        //getting Vector2 position values of selected nodes
        for (int i = 0; i < closestIndexes.Count; i++)
        {
            nodeVectors.Add(NodeManager.mapNodes[closestIndexes[i]].transform.position);
        }
        return nodeVectors.giveMiddlePoint();
    }

    /// <summary>
    /// Turns given indexes according to direction
    /// </summary>
    /// <param name="closestIndexes"></param>
    /// <param name="dir"></param>
    public static void turnIndexesByGivenDirection(this List<int> closestIndexes, int dir)
    {
        List<Node> tempNodes = new List<Node>();
        for (int c = 0; c < closestIndexes.Count; c++)
        {
            tempNodes.Add(closestIndexes[c].giveNodeByIndex());

        }
        if (dir == 1)
        {
            NodeManager.mapNodes[closestIndexes[0]] = tempNodes[1];
            NodeManager.mapNodes[closestIndexes[1]] = tempNodes[2];
            NodeManager.mapNodes[closestIndexes[2]] = tempNodes[0];
        }
        else
        {
            NodeManager.mapNodes[closestIndexes[0]] = tempNodes[2];
            NodeManager.mapNodes[closestIndexes[1]] = tempNodes[0];
            NodeManager.mapNodes[closestIndexes[2]] = tempNodes[1];
        }
    }
    /// <summary>
    /// Given transform parents the given nodes by indexs
    /// </summary>
    /// <param name="indexs"></param>
    /// <param name="t"></param>
    public static void setParent(this List<int> indexs, Transform t)
    {
        for (int i = 0; i < indexs.Count; i++)
        {
            indexs[i].giveNodeByIndex().transform.SetParent(t);
        }
    }
    /// <summary>
    /// Finds the node that has lower position.y
    /// </summary>
    /// <param name="closestIndexes"></param>
    /// <returns></returns>
    public static int findTheNodeThatHasLowerYaxis(this List<int> closestIndexes)
    {
        float tempY = closestIndexes[0].giveNodeByIndex().transform.position.y;
        int lowerYaxisIndex = 0;
        for (int i = 0; i < closestIndexes.Count; i++)
        {
            if (closestIndexes[i].giveNodeByIndex().transform.position.y < tempY)
            {
                tempY = closestIndexes[i].giveNodeByIndex().transform.position.y;
                lowerYaxisIndex = i;
            }
        }
        return lowerYaxisIndex;
    }
    public static void changeSortingOrder(this List<int> indexs, int order)
    {
        for (int i = 0; i < indexs.Count; i++)
        {
            indexs[i].giveNodeByIndex().GetComponent<SpriteRenderer>().sortingOrder = order;
        }

    }
}
