using UnityEngine;
using System.Collections.Generic;

public static class ExplosionExtensions{
    /// <summary>
    /// Checks if there two same colored node near by near if it is return false
    /// else all same colored triple will be processed in IsThereAnyPossibleExplosionByGivenGroups
    /// </summary>
    /// <param name="uniqueTriples"></param>
    /// <returns></returns>
    public static bool IsThereAnyPossibleMoveInGame(this List<List<int>> uniqueTriples)
    {
        if (uniqueTriples.checkGroupTriplesHaveSameCountOfColorByGivenCountOfColor(2))
        {
            return uniqueTriples.getGroupTriplesByHasTwoSameColor().IsThereAnyPossibleExplosionByGivenGroups();
        }
        return false;
    }
    /// <summary>
    /// For every triple group with two same color;
    /// Finds same color and different color indexes for every triple group with two same color
    /// With different node's index in one group, finds all(closest) nodes around different colored node,
    /// if one of this closest nodes has same color with a triple group's same color index,
    /// that means there is a move in the game.
    /// </summary>
    /// <param name="tripleGroupsWithTwoSameColors"></param>
    /// <returns></returns>
    public static bool IsThereAnyPossibleExplosionByGivenGroups(this List<List<int>> tripleGroupsWithTwoSameColors)
    {
        for (int i = 0; i < tripleGroupsWithTwoSameColors.Count; i++)
        {
            int tempColorIndex = tripleGroupsWithTwoSameColors[i].getSameColorIndexByTripleGroup();
            int indexOfDifferentNode = tripleGroupsWithTwoSameColors[i].findDifferentNodeIndexBySameColorIndexInTripleGroup(tempColorIndex);
            List<int> allNodesAroundThisNode = GridSystem.getGroupofTriples(indexOfDifferentNode.giveNodeByIndex()).mergeLists();
            int counter = 0;
            for (int v = 0; v < allNodesAroundThisNode.Count; v++)
            {
                if (allNodesAroundThisNode[v].giveColorByNodeIndex() == tempColorIndex)
                {
                    counter++;
                    if (counter > 2)
                    {
                        return true;
                    }
                }

            }
        }
        return false;
    }
    /// <summary>
    /// Finds triple node groups with same color and returns
    /// Checks every group has same nodes if it exist than merges them into one group
    /// Returns exploding triple node groups
    /// </summary>
    /// <param name="triangleGroups"></param>
    /// <param name="colorCount"></param>
    /// <returns></returns>
    public static List<List<int>> giveEveryGroupThatWillExplode(this List<List<int>> tripleGroups, int colorCount)
    {
        List<List<int>> explodingTriples = new List<List<int>>();
        for (int i = 0; i < tripleGroups.Count; i++)
        {
            if (tripleGroups[i].checkGroupTriplesHaveSameCountOfColorByGivenCountOfColor(colorCount))
            {
                if (explodingTriples.Exists(x => x.IsTriplesHaveCommonNodes(tripleGroups[i])))
                {
                    int index = explodingTriples.FindIndex(x => x.IsTriplesHaveCommonNodes(tripleGroups[i]));
                    explodingTriples[index] = explodingTriples[index].mergeLists(tripleGroups[i]);
                }
                else
                {
                    explodingTriples.Add(tripleGroups[i]);
                }
            }
        }
        return explodingTriples;
    }
  /// <summary>
    /// Returns the color index that a triple group's node's same color.
  /// </summary>
  /// <param name="tripleGroup"></param>
  /// <returns></returns>
    public static int getSameColorIndexByTripleGroup(this List<int> tripleGroup)
    {
        List<int> colorsIndexesToCheck = new List<int>();
        for (int k = 0; k < tripleGroup.Count; k++)
        {
            int tempColorIndex = tripleGroup[k].giveColorByNodeIndex();
            if (!colorsIndexesToCheck.Contains(tempColorIndex))
            {
                colorsIndexesToCheck.Add(tempColorIndex);
            }
            else
            {
                return tempColorIndex;
            }
        }
        return -1;
    }
    /// <summary>
    /// Returns triple groups that have two same color indexed node
    /// </summary>
    /// <param name="tripleGroups"></param>
    /// <returns></returns>
    public static List<List<int>> getGroupTriplesByHasTwoSameColor(this List<List<int>> tripleGroups)
    {
        List<List<int>> tripleGroupsWithTwoSameColors = new List<List<int>>();
        for (int i = 0; i < tripleGroups.Count; i++)
        {
            if (tripleGroups[i].checkGroupTriplesHaveSameCountOfColorByGivenCountOfColor(2))
            {
                tripleGroupsWithTwoSameColors.Add(tripleGroups[i]);
            }
        }
        return tripleGroupsWithTwoSameColors;
    }
    /// <summary>
    /// Finds the different colored node index in triple group that has same two colors.
    /// </summary>
    /// <param name="tripleGroup"></param>
    /// <param name="colorIndex"></param>
    /// <returns></returns>
    public static int findDifferentNodeIndexBySameColorIndexInTripleGroup(this List<int> tripleGroup, int colorIndex)
    {

        for (int i = 0; i < tripleGroup.Count; i++)
        {
            if (tripleGroup[i].giveColorByNodeIndex() != colorIndex)
            {
                return tripleGroup[i];
            }
        }
        return -1;
    } /// <summary>
    /// Checks if this group has same node with given triple node group
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    public static bool IsTriplesHaveCommonNodes(this List<int> list1, List<int> list2)
    {
        for (int i = 0; i < list1.Count; i++)
        {
            if (list2.Contains(list1[i]))
            {
                return true;
            }
        }
        return false;
    }
}
