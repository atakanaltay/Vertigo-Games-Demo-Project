using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour {
    /// <summary>
    /// Moves score text and show the given point
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public IEnumerator moveToPos(int point)
    {
        GetComponent<TextMesh>().text = point.ToString();
        Vector3 position = transform.position + Vector3.up * 1f;
        for (int i = 0; i < 90; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, position + Vector3.back * 4f, 0.05f);
            if (Vector3.Distance(position,transform.position)<=0.001f)
            {
                Destroy(gameObject);
                yield break;
            }
            yield return new WaitForFixedUpdate ();
        }
        Destroy(gameObject);
    }
  
}
