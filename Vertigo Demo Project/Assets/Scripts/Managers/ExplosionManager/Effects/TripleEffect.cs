using UnityEngine;
using System.Collections;

public class TripleEffect : MonoBehaviour 
{
    /// <summary>
    /// Initialize the effect
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="colorIndex"></param>
	public void ShowEffect(Vector3 pos, int colorIndex){
		transform.position = pos;
		var main = GetComponent<ParticleSystem>().main;
        Color forAlphaChange = GameManager.instance.colorSys.colorsSc.Colorz[colorIndex];
        forAlphaChange.a = 0.75f;
        main.startColor = forAlphaChange;
		StartCoroutine (waitForStop ());
	}

	IEnumerator waitForStop(){
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}
}
