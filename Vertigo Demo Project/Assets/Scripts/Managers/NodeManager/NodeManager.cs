using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
public class NodeManager : MonoBehaviour
{
    #region Variables
    
    public static List<Node> mapNodes;
    #endregion
    public List<int> giveClosestIndexes(Node ClickedNode, Vector3 mousePos, int countToGive)
    {
        List<int> selectedIndexex = new List<int>();
        List<Node> excludedNodes = new List<Node>();
        excludedNodes.Add(ClickedNode);

        Vector2 mPoint = new List<Vector2>(new Vector2[] { mousePos, ClickedNode.transform.position }).giveMiddlePoint();
        //
        for (int i = 0; i < countToGive; i++)
        {
            int closestIndex = 0;
            float closestPoint = 100f;
            for (int b = 0; b < mapNodes.Count; b++)
            {
                if (!excludedNodes.Contains(mapNodes[b]))
                {
                    if (Vector2.Distance(mapNodes[b].transform.position, mPoint) < closestPoint)
                    {
                        closestIndex = b;
                        closestPoint = Vector2.Distance(mapNodes[b].transform.position, mPoint);
                    }
                }
            }
            selectedIndexex.Add(closestIndex);
            excludedNodes.Add(mapNodes[closestIndex]);

            //Recorrect middle Point
            List<Vector2> tempArray = new List<Vector2>(new Vector2[] { mousePos, ClickedNode.transform.position });
            for (int c = 0; c < excludedNodes.Count; c++)
            {
                tempArray.Add(excludedNodes[c].transform.position);
            }
            mPoint = tempArray.giveMiddlePoint();
        }
        selectedIndexex.Add(mapNodes.FindIndex(x => x == ClickedNode));
        return selectedIndexex;
    }

}
