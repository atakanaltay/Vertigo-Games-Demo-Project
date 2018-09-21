using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TripleCoverObject : CoverObject {

    #region Public Methods
    /// <summary>
    /// Initializing the coverobject that covers hexagons
    /// </summary>
    /// <param name="closestIndexes"></param>
    public override void cover(List<int> closestIndexes)
    {
        //positioning
        Vector2 middlePoint = closestIndexes.giveMiddlePointByIndexes();
        transform.position = middlePoint;
        //rotating the object according to angle between nodes>>

        //finding the angle for placing coverobj in every situation correctly according to nodes center points
        float angle = giveAngle(closestIndexes.giveLowestPositionYByIndexes(), middlePoint);
        transform.GetChild(0).eulerAngles = new Vector3(0f, 0f, angle);
        closestIndexes.Sort();
    }
    /// <summary>
    /// Starts rotating the coverobject
    /// </summary>
    /// <param name="cod"></param>
    /// <param name="closestIndexes"></param>
    public override void rotate(CoverObjectDirection cod, List<int> closestIndexes)
    {
        StartCoroutine(rotateRoutine(cod, closestIndexes));
    }
    /// <summary>
    /// Rotates cover object by given CoverObjectDirection 
    /// </summary>
    /// <param name="cod"></param>
    /// <param name="closestIndexes"></param>
    /// <returns></returns>
    internal override IEnumerator rotateRoutine(CoverObjectDirection cod, List<int> closestIndexes)
    {
        closestIndexes.changeSortingOrder(1);
        closestIndexes.setParent(transform);
        closestIndexes.Sort();
        for (int i = 0; i < 3; i++)
        {
            if (closestIndexes[1] - closestIndexes[0] == 1)//<<if cover object's, two 'vertical aligned' nodes, on the left side
            {
                if (cod != CoverObjectDirection.Clockwise)
                {
                    yield return StartCoroutine(turnAnim(1f, 0.20f));
                    closestIndexes.turnIndexesByGivenDirection(1);

                }
                else
                {
                    yield return StartCoroutine(turnAnim(-1f, 0.20f));
                    closestIndexes.turnIndexesByGivenDirection(-1);
                }
            }
            else
            {

                if (cod != CoverObjectDirection.Clockwise)
                {
                    yield return StartCoroutine(turnAnim(1f, 0.20f));
                    closestIndexes.turnIndexesByGivenDirection(-1);
                }
                else
                {
                    yield return StartCoroutine(turnAnim(-1f, 0.20f));
                    closestIndexes.turnIndexesByGivenDirection(1);

                }
            }

            yield return new WaitForSeconds(0.05f);//smoothing
            if (GridSystem.uniqueTriples.checkGroupTriplesHaveSameCountOfColorByGivenCountOfColor(1))//explosion detected
            {
                resetNodesByIndexes(closestIndexes);
                gameObject.SetActive(false);
                GameManager.instance.explosionManager.startExplode(GetType(), GridSystem.uniqueTriples.giveEveryGroupThatWillExplode(1));
                yield break;
            }
        }
        resetNodesByIndexes(closestIndexes);
        SwipeLogger.checkForInput();
        GameManager.instance.inputSys.enabled = true;
    }
    /// <summary>
    /// Turns this object by given direction and duration
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    internal override IEnumerator turnAnim(float dir, float duration)
    {
        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation + (120f * dir);
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, zRotation);
            yield return null;
        }
    } 
    #endregion
    #region Private Methods
    /// <summary>
    /// Resets given nodes rotation,sorting order and parent
    /// </summary>
    /// <param name="closestIndexes"></param>
    private void resetNodesByIndexes(List<int> closestIndexes)
    {
        for (int i = 0; i < closestIndexes.Count; i++)
        {
            NodeManager.mapNodes[closestIndexes[i]].correctRotation();
        }
        closestIndexes.changeSortingOrder(0);
        closestIndexes.setParent(GameManager.instance.nodeManager.gameObject.transform);
    }
    /// <summary>
    /// Finds the angle between two vectors 
    /// </summary>
    /// <param name="P1"></param>
    /// <param name="P2"></param>
    /// <returns></returns>
    private float giveAngle(Vector2 P1, Vector2 P2)
    {
        float xDiff = P1.x - P2.x;
        float yDiff = P1.y - P2.y;
        float angle = Mathf.Atan2(yDiff, xDiff) * 180.0f / Mathf.PI;
        if (angle < 0) //corrects the angle
        {
            angle += 360f;
        }
        return angle;
    } 
    #endregion
}
