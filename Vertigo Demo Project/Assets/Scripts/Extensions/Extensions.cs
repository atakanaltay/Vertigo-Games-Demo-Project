using UnityEngine;
using System.Collections.Generic;
internal static class Extensions
{
    /// <summary>
    /// Check for if given triple groups contains given count of same color
    /// </summary>
    /// <param name="tripleGroups"></param>
    /// <param name="colorCount"></param>
    /// <returns></returns>
    public static bool checkGroupTriplesHaveSameCountOfColorByGivenCountOfColor(this List<List<int>> tripleGroups, int colorCount)
    {
        for (int i = 0; i < tripleGroups.Count; i++)
        {
            if (tripleGroups[i].checkGroupTriplesHaveSameCountOfColorByGivenCountOfColor(colorCount))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Checks for if given triple group contains given count of same color
    /// </summary>
    /// <param name="tripleGroup"></param>
    /// <param name="colorCount"></param>
    /// <returns></returns>
    public static bool checkGroupTriplesHaveSameCountOfColorByGivenCountOfColor(this List<int> tripleGroup, int colorCount)
    {

        List<int> colorsIndexesToCheck = new List<int>();
        for (int k = 0; k < tripleGroup.Count; k++)
        {
            if (!colorsIndexesToCheck.Contains(tripleGroup[k].giveColorByNodeIndex()))
            {
                colorsIndexesToCheck.Add(tripleGroup[k].giveColorByNodeIndex());
            }
        }
        if (colorsIndexesToCheck.Count == colorCount)
        {
            return true;
        }

        return false;
    }
    /// <summary>
    /// Merges all List<int> elements each other to one List
    /// </summary>
    /// <param name="lists"></param>
    /// <returns></returns>
    public static List<int> mergeLists(this List<List<int>> lists)
    {
        List<int> tempList = new List<int>(lists[0]);
        for (int i = 1; i < lists.Count; i++)
        {
            tempList = mergeLists(lists[i], tempList);
        }
        return tempList;
    }
    /// <summary>
    /// Merges two list by checking given list contains other list's elements;if not contains adds list1 element to list2 
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    public static List<int> mergeLists(this List<int> list1, List<int> list2)
    {
        List<int> tempList = new List<int>(list2);
        for (int i = 0; i < list1.Count; i++)
        {
            if (!tempList.Contains(list1[i]))
            {
                tempList.Add(list1[i]);
            }
        }
        return tempList;
    }
    public static Node giveNodeByIndex(this int index)
    {
        return NodeManager.mapNodes[index];

    }
    public static int giveColorByNodeIndex(this int index)
    {
        return NodeManager.mapNodes[index].GetComponent<HexaNode>().myColorIndex;

    } 
    /// <summary>
    /// Finds given vectors middle point
    /// </summary>
    /// <param name="Vectors"></param>
    /// <returns></returns>
    /// 
    public static Vector2 giveMiddlePoint(this List<Vector2> Vectors)
    {
        Vector2 temp = Vector2.zero;
        int tempCount = Vectors.Count;

        for (int i = 0; i < tempCount; i++)
        {
            temp.x += (Vectors[i].x / tempCount);
            temp.y += (Vectors[i].y / tempCount);
        }
        return temp;
    }
  
    /// <summary>
    /// Destroys all the nodes in game and creating effects
    /// </summary>
    public static void gameOverExplode()
    {
        for (int i = 0; i < NodeManager.mapNodes.Count; i++)
        {
            GameManager.instance.explosionManager.currentExplosion.createAndPlayEffect(i.giveNodeByIndex().transform.position, i.giveColorByNodeIndex());
            GameObject.Destroy(i.giveNodeByIndex().gameObject);
        }
    }
}
