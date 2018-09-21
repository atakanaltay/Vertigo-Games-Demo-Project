using UnityEngine;

public class SwipeLogger : MonoBehaviour
{
    
    internal Vector2 NodePosition;
    internal static bool lastProcessed=false;
    private void Awake()
    {
        InputSystem.OnSwipe += SwipeDetector_OnSwipe;
    }
    CoverObjectDirection cod;
    public static void checkForInput()
    {
        if (Input.touches.Length == 0)
        {
            lastProcessed = false;
        }
    }
    private void SwipeDetector_OnSwipe(SwipeData data,Node currentNode)
    {
        Vector2 EndPosition = Camera.main.ScreenToWorldPoint(data.EndPosition);
        switch (data.Direction)
        {
            case SwipeDirection.Up:
                cod = EndPosition.x < NodePosition.x ? CoverObjectDirection.Clockwise : CoverObjectDirection.CounterClockwise;
                break;
            case SwipeDirection.Down:
                cod = EndPosition.x < NodePosition.x ? CoverObjectDirection.CounterClockwise : CoverObjectDirection.Clockwise;
                break;
            case SwipeDirection.Left:
                cod = EndPosition.y > NodePosition.y ? CoverObjectDirection.CounterClockwise : CoverObjectDirection.Clockwise;
                break;
            case SwipeDirection.Right:
                cod = EndPosition.y > NodePosition.y ? CoverObjectDirection.Clockwise : CoverObjectDirection.CounterClockwise;
                break;
            default:
                break;
        }
        if (!lastProcessed)
        {
            if (GameManager.instance.coverObjManager.currentCoverObject!=null)
            {
                GameManager.instance.inputSys.enabled = false;
                GameManager.instance.coverObjManager.startRotate(cod);
                lastProcessed = true;
            }

        }
    }
  
}
