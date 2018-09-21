using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InputSystem : MonoBehaviour
{

    
    #region Variables

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    [SerializeField]
    SwipeLogger swipeLogger;

    Node currentNode;

    public static event Action<SwipeData,Node> OnSwipe = delegate { };
    #endregion
    #region Unity Methods
    public void startGame()
    {
        enabled = true;
        SwipeLogger.lastProcessed = false;
    }
    private void Update()
    {
        for (int i = 0; i < Input.touches.Length; i++)
        {
            if (Input.touches[i].phase == TouchPhase.Began)
            {
                
                fingerUpPosition = Input.touches[i].position;
                fingerDownPosition = Input.touches[i].position;
            }
            if (!detectSwipeOnlyAfterRelease && Input.touches[i].phase == TouchPhase.Moved)
            {
                fingerDownPosition = Input.touches[i].position;
                DetectSwipe();
            }

            if (Input.touches[i].phase == TouchPhase.Ended)
            {
                if (SwipeLogger.lastProcessed)
                {
                    SwipeLogger.lastProcessed = false;
                }
                else
                {
                    fingerDownPosition = Input.touches[i].position;
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                     Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);

                    if (hit.transform != null)
                    {
                        swipeLogger.NodePosition = hit.transform.position;
                        currentNode = hit.transform.GetComponent<Node>();
                        currentNode.select(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    }
                }
            }
        }
    } 
    #endregion
    #region Private Methods
    private void DetectSwipe()
    {
        SwipeDirection direction;
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }

            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition
        };

        OnSwipe(swipeData, currentNode);
    }  
    #endregion

}
public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}
