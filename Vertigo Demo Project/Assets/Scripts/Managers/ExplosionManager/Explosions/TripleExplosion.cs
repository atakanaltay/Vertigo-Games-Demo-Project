using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TripleExplosion : Explosion {

    #region Variables
    Dictionary<int, List<int>> columnsAndIndexesOfExplodingNodes;
    
    #endregion
    #region Unity Methods
    public override void Awake()
    {
        Effects = new GameObject[1];
        Effects[0] = Resources.Load("Prefabs/TripleEffect") as GameObject;
    } 
    #endregion
    #region Public Methods
    /// <summary>
    /// Seperates exploding nodes by their y positions.
    /// Creates& plays effect,adds score
    /// </summary>
    /// <param name="explodingNodes"></param>
    public override void startExplodingTheGivenNodes(List<List<int>> explodingNodes)
    {
        columnsAndIndexesOfExplodingNodes = new Dictionary<int, List<int>>();
        for (int i = 0; i < explodingNodes.Count; i++)
        {
            List<Vector2> positionsOfExplodingNodes = new List<Vector2>();
            int tempColorInd = NodeManager.mapNodes[explodingNodes[i][0]].GetComponent<HexaNode>().myColorIndex;

            for (int k = 0; k < explodingNodes[i].Count; k++)
            {
                Vector3 positionOfThisNode = NodeManager.mapNodes[explodingNodes[i][k]].transform.position;
                createAndPlayEffect(positionOfThisNode, tempColorInd);
                positionsOfExplodingNodes.Add(positionOfThisNode);
                NodeManager.mapNodes[explodingNodes[i][k]].gameObject.SetActive(false);
                int columnIndex = explodingNodes[i][k].GetColumnOfIndex(GameManager.instance.gridSys.height);
                fillingTheCollection(columnIndex, explodingNodes[i][k]);
            }
            GameManager.instance.scoreSys.incrementScore(positionsOfExplodingNodes.Count * 5, positionsOfExplodingNodes.giveMiddlePoint());
        }
        StartCoroutine(explode(columnsAndIndexesOfExplodingNodes));
    }
    /// <summary>
    /// Ends the explosion process, starts exploding, waits for moving nodes and check for gameover
    /// </summary>
    /// <param name="columnsAndIndexesOfExplodingNodes"></param>
    /// <returns></returns>
    public override IEnumerator explode(Dictionary<int, List<int>> columnsAndIndexesOfExplodingNodes)
    {
        MoveRoutines = new List<Coroutine>();
        foreach (var item in columnsAndIndexesOfExplodingNodes)
        {
            addAndRemoveNewNodestoTheGame(item.Value, item.Key);
        }
        yield return StartCoroutine(waitForMoveRoutines());
        yield return new WaitForSeconds(0.15f);
        GameManager.instance.gameOverCheck();
    }
    public override IEnumerator startMovingOfNodes(List<int> allMovingIndexesInAColumn)
    {
        for (int i = 0; i < allMovingIndexesInAColumn.Count; i++)
        {
            Vector3 position = GameManager.instance.gridSys.getPositionOfGivenNodeIndex(allMovingIndexesInAColumn[i]);
            if (i == allMovingIndexesInAColumn.Count - 1)
            {
                yield return StartCoroutine(allMovingIndexesInAColumn[i].giveNodeByIndex().Move(position));
            }
            else
            {
                StartCoroutine(allMovingIndexesInAColumn[i].giveNodeByIndex().Move(position));
            }
            yield return new WaitForSeconds(0.065f);
        }
        yield break;
    }
    public override void createAndPlayEffect(Vector3 pos, int colorIndex)
    {
        Instantiate(Effects[0], pos, Quaternion.identity).GetComponent<TripleEffect>().ShowEffect(pos - zOffsetOfEffect, colorIndex);
    } 
    #endregion
    #region Private Methods
    /// <summary>
    /// Fills the collection with new column or adds to created column by columindex
    /// </summary>
    /// <param name="columnIndex"></param>
    /// <param name="nodeIndex"></param>
    private void fillingTheCollection(int columnIndex, int nodeIndex)
    {
        if (columnsAndIndexesOfExplodingNodes.ContainsKey(columnIndex))
        {
            columnsAndIndexesOfExplodingNodes[columnIndex].Add(nodeIndex);
        }
        else
        {
            columnsAndIndexesOfExplodingNodes.Add(columnIndex, new List<int> { nodeIndex });
        }
    }
    /// <summary>
    /// Adding new nodes and removing exloding nodes in giving column 
    /// </summary>
    /// <param name="nodesInColumn"></param>
    /// <param name="columIndex"></param>
    private void addAndRemoveNewNodestoTheGame(List<int> nodesInColumn, int columIndex)
    {
        nodesInColumn.Sort();
        int insertIndex = (columIndex + 1) * GameManager.instance.gridSys.height;//column to insert
        int explodingNodeCountInAColumn = nodesInColumn.Count;
        for (int i = explodingNodeCountInAColumn - 1; i >= 0; i--)
        {
            HexaNode tempNode = GameManager.instance.gridSys.giveMeAnHexaNode(giveNodesUpperPosition(i, explodingNodeCountInAColumn, insertIndex));
            NodeManager.mapNodes.Insert(insertIndex, tempNode);
            Destroy(NodeManager.mapNodes[nodesInColumn[i]].gameObject);
            NodeManager.mapNodes.RemoveAt(nodesInColumn[i]);
        }
        StartCoroutine(startRoutinesOfNodesInSameColumn(insertIndex, nodesInColumn));
    }

    /// <summary>
    /// Start moving routine by columns nodes
    /// </summary>
    /// <param name="insertIndex"></param>
    /// <param name="item"></param>
    private IEnumerator startRoutinesOfNodesInSameColumn(int insertIndex, List<int> item)
    {
        List<int> allMovingIndexesInAColumn = new List<int>();
        for (int i = item[0]; i < insertIndex; i++)
        {
            allMovingIndexesInAColumn.Add(i);
        }
        for (int i = 0; i < allMovingIndexesInAColumn.Count; i++)
        {
            MoveRoutines.Add(StartCoroutine(startMovingOfNodes(allMovingIndexesInAColumn)));
            yield return new WaitForSeconds(0.15f);
        }
    }
    private IEnumerator waitForMoveRoutines()
    {
        for (int i = 0; i < MoveRoutines.Count; i++)
        {
            yield return MoveRoutines[i];
        }
    }
    /// <summary>
    /// Return a position for new node to spawn
    /// </summary>
    /// <param name="index"></param>
    /// <param name="explodingNodeCountInAColumn"></param>
    /// <param name="insertIndex"></param>
    /// <returns></returns>
    private Vector3 giveNodesUpperPosition(int index, int explodingNodeCountInAColumn, int insertIndex)
    {
        Vector3 upperIndexPos = NodeManager.mapNodes[insertIndex - 1].transform.position;
        return (new Vector3(upperIndexPos.x, 6f + ((explodingNodeCountInAColumn - 1 - index) * GameManager.instance.gridSys.yPlusPos)));
    }

    #endregion
  
	
}
