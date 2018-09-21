using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
public class GridSystem : MonoBehaviour
{

    #region Variables

    public int width = 8;
    public int height = 9;
    
    internal float yPlusPos;
    internal float xPlusPos;
    internal float xOffSet;
    internal float yOffSet;
    GameObject Hex,Bomb;

    public static List<List<int>> uniqueTriples;
    #endregion
    #region Unity Methods
    void Awake()
    {
        Hex = Resources.Load("Prefabs/Hexagon") as GameObject;
        Bomb = Resources.Load("Prefabs/Bomb") as GameObject;
    }
    
    #endregion
    #region Private Methods
    private void createGrid()
    {
        //scale calculation for placing hexs correct positions according to x and y 
        float oneSideScale = Mathf.Clamp(height, 7, 100) < (Mathf.Clamp(width, 7, 100)) + 2f ? 5f / ((Mathf.Clamp(width, 7, 100)) * 3.2f * 1f / 4f + 1f / 4f) : 5f / (Mathf.Clamp(height, 7, 100) * 3.2f * 1f / 4f + 1f / 4f);
        correctScales(oneSideScale);
        xPlusPos = oneSideScale / 2f + oneSideScale / 4f;       //extra offsets for correctly placing to screen size
        yPlusPos = 2f * oneSideScale / 4f * Mathf.Sqrt(3f);     //
        xOffSet = ((width * (3f * oneSideScale / 4f) + oneSideScale / 4f) - oneSideScale) / 2f;
        yOffSet = (height * yPlusPos + (yPlusPos / 2f)) * oneSideScale / 2f;//ekran yerleşimi için
        Vector3 position = Vector3.zero, rotation = Hex.transform.rotation.eulerAngles;
        NodeManager.mapNodes = new List<Node>(width * height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                position = getPositionOfGivenNodeIndex(NodeManager.mapNodes.Count);
                HexaNode temp = giveMeANewNode(position).GetComponent<HexaNode>();
                NodeManager.mapNodes.Add(temp);
                giveAColorToTheCreatedHexNode(temp, false);
            }
        }
    }
    private void correctScales(float oneSideScale)
    {
        Hex.transform.localScale = new Vector3(oneSideScale, oneSideScale, 1f);
        Bomb.transform.localScale = new Vector3(oneSideScale * 5f / 6f, oneSideScale * 5f / 6f, 1f);
        GameManager.instance.coverObjManager.correctScale(oneSideScale);
    }
    private bool giveColorFromStart(bool random, HexaNode tempNode)
    {
        int tempNodeCount = NodeManager.mapNodes.Count;
        int columnIndex = (tempNodeCount - 1).GetColumnOfIndex(height);
        int rowIndex = (tempNodeCount - 1).GetRowOfIndex(height);
        //Hexagons colors in first column and lowest y axised row's first columns on map can be selected randomly. 
        if (random || (columnIndex == 0) || ((columnIndex % 2 != 0) && (rowIndex == 0)))
        {
            tempNode.changeColor(UnityEngine.Random.Range(0, ColorSystem.colorCount));
            return true;
        }
        return false;
    }
    #endregion
    #region Public Methods
    public void startGame()
    {
        uniqueTriples = new List<List<int>>();
        createGrid();
    }
    /// <summary>
    /// Divides an hexagon to six triple visually 
    /// Finding the visual triple centers with calculated angle
    /// Adding triple centers to nodes positions for correcting the point of triples
    /// Getting all indexes for all points according to closestindex algorithm and gets the group of hex indexes
    /// Finds unique triple groups by checking if group golder contains the found holder
    /// Returns the group of all triples
    /// </summary>
    /// <param name="tempNode"></param>
    /// <returns></returns>
    public static List<List<int>> getGroupofTriples(Node tempNode)
    {

        List<List<int>> allTripleGroups = new List<List<int>>();
        for (int i = 0; i < 6; i++)
        {
            float angle = (45f + 60f * i);                                                                //finds correct angle for every part of hexagon according to 60 degree
            int xMultiplierForSecondAndThirdParts = ((angle / 90f > 1) && (angle / 90f < 4)) ? -1 : +1;  //checks for finding where is angle in coordinate system
            int yMultiplierForThirdAndForthParts = (angle / 90f > 2) ? -1 : +1;
            float xOfVector = Mathf.Sin(angle) * xMultiplierForSecondAndThirdParts / 2f;                 //x axis of this direction is the cos of this angle 
            float yOfVector = Mathf.Cos(angle) * yMultiplierForThirdAndForthParts / 2f;                  //y axis of this direction is the sin of this angle 


            List<int> group = GameManager.instance.nodeManager.giveClosestIndexes(tempNode, tempNode.transform.position + new Vector3(xOfVector, yOfVector, 0f), 2);
            if (!group.checkTripleGroupHasThisTripleGroupElements(allTripleGroups))
            {
                allTripleGroups.Add(group);
                if (!group.checkTripleGroupHasThisTripleGroupElements(uniqueTriples))
                {
                    uniqueTriples.Add(group);
                }
            }

        }
        return allTripleGroups;
    }

    /// <summary>
    /// Gives a color to hexnode,
    /// checks if an triple group have same color, 
    /// if it has corrects the colors of hexnode
    /// </summary>
    /// <param name="tempNode"></param>
    /// <param name="random"></param>
    public void giveAColorToTheCreatedHexNode(HexaNode tempNode, bool random)
    {
        if (giveColorFromStart(random, tempNode))
        {
            return;
        }
        List<List<int>> allTripleGroups = getGroupofTriples(tempNode);
        int randomColorIndex = UnityEngine.Random.Range(0, ColorSystem.colorCount);
        tempNode.myColorIndex = randomColorIndex;
        if (allTripleGroups.checkGroupTriplesHaveSameCountOfColorByGivenCountOfColor(1) == true)
        {
            for (int i = 0; i < ColorSystem.colorCount - 1; i++)
            {
                randomColorIndex++;
                if (randomColorIndex >= ColorSystem.colorCount)
                {
                    randomColorIndex -= ColorSystem.colorCount;
                }
                tempNode.myColorIndex = randomColorIndex;
                if (allTripleGroups.checkGroupTriplesHaveSameCountOfColorByGivenCountOfColor(1) == false)
                {
                    break;
                }
            }
        }
        tempNode.changeColor(randomColorIndex);
        return;
    }

    public Vector3 getPositionOfGivenNodeIndex(int index)
    {
        int y = index.GetRowOfIndex(height);
        int x = index.GetColumnOfIndex(height);
        Vector3 position = Vector3.zero;
        position.x = x * xPlusPos - xOffSet;
        if (x % 2 == 0)
        {
            position.y = y * yPlusPos;
        }
        else
        {
            position.y = y * yPlusPos - yPlusPos / 2f;
        }
        position.y -= yOffSet;
        return position;
    }
    public Node giveMeANewNode(Vector3 position)
    {
        Transform temp;
        if (GameManager.instance.scoreSys.currentScore >= 1000 * (GameManager.instance.bombSys.givenBombCount + 1))
        {
            temp = Instantiate(Bomb, transform).GetComponent<Transform>();
            GameManager.instance.bombSys.initializeBomb(temp.GetComponent<BombNode>());
        }
        else
        {
            temp = Instantiate(Hex, transform).GetComponent<Transform>();
        }
        temp.localPosition = position;
        temp.rotation = Quaternion.Euler(0f, 0f, 90f);
        return temp.GetComponent<Node>();
    }
    public HexaNode giveMeAnHexaNode(Vector3 position)
    {
        Node temp = giveMeANewNode(position);
        giveAColorToTheCreatedHexNode(temp.GetComponent<HexaNode>(), true);
        return temp.GetComponent<HexaNode>();
    }

    
    #endregion
  
}
