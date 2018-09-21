using UnityEngine;
using System.Collections.Generic;
public static class GridSystemExtensions {
    /// <summary>
    /// Checks a triple group contain given triple groups
    /// </summary>
    /// <param name="tripleGroup"></param>
    /// <param name="allTripleGroups"></param>
    /// <returns></returns>
    public static bool checkTripleGroupHasThisTripleGroupElements(this List<int> tripleGroup, List<List<int>> allTripleGroups)
    {
        for (int k = 0; k < allTripleGroups.Count; k++)
        {
            if (tripleGroup.checkListTripleHaveASameElement(allTripleGroups[k]))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Checks a triple group contains an other triple group
    /// </summary>
    /// <param name="firstTriple"></param>
    /// <param name="secondTriple"></param>
    /// <returns></returns>
    public static bool checkListTripleHaveASameElement(this List<int> firstTriple, List<int> secondTriple)
    {
        for (int i = 0; i < firstTriple.Count; i++)
        {
            if (!secondTriple.Contains(firstTriple[i]))
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Column of the node index.
    /// </summary>
    /// <returns>The index.</returns>
    /// <param name="index">�ndex.</param>
    /// <param name="yLength">Y length.</param>
    public static int GetColumnOfIndex(this int index, int yLength)
    {
        return (index / yLength);
    }
    /// <summary>
    /// Row of the node index.
    /// </summary>
    /// <returns>The of the index.</returns>
    /// <param name="index">�ndex.</param>
    /// <param name="yLength">Y length.</param>
    public static int GetRowOfIndex(this int index, int yLength)
    {
        return (index % yLength);
    }   

	
}
