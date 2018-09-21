using UnityEngine;
using System.Collections;

public class HexaNode : Node,IColorable {
    #region Variables
    private float deltaPos;
    public int myColorIndex { get; set; } 
    #endregion
    #region Public Methods
    /// <summary>
    /// Changes nodes color and colorindex
    /// </summary>
    /// <param name="index"></param>
    public void changeColor(int index)
    {
        myColorIndex = index;
        transform.GetComponent<SpriteRenderer>().color = GameManager.instance.colorSys.colorsSc.Colorz[myColorIndex];
    }
    /// <summary>
    /// Start cover process by node type
    /// </summary>
    /// <param name="_TouchPos"></param>
    public override void select(Vector3 _TouchPos)
    {
        GameManager.instance.coverObjManager.startCover(this.GetType(), GameManager.instance.nodeManager.giveClosestIndexes(this, _TouchPos, 2));
    }
    /// <summary>
    /// Moves to given positions.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public override IEnumerator Move(Vector3 position)
    {
        Vector3 temp = transform.position;

        //if (transform.position.y - position.y >= 3f)
        //{
            deltaPos = 0.12f;
        //}
        //else
        //{
        //    deltaPos = 0.04f;
        //}
        for (int i = 0; i < 120; i++)
        {
            temp.y -= deltaPos;
            transform.position = temp;
            // transform.position = Vector2.MoveTowards(transform.position, position, deltaPos);
            if (position.y >= temp.y)
            {
                transform.position = position;
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }

        transform.position = position;
        yield break;
    }
    /// <summary>
    /// Corrects the rotation of node
    /// </summary>
    public override void correctRotation()
    {
        gameObject.transform.eulerAngles = new Vector3(0f, 0f, 90f);
    } 
    #endregion
}
      
