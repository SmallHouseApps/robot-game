using UnityEngine;
using System.Collections;

public class DeactivateObject : MonoBehaviour {

    IEnumerator DeactivateEffect()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Method which deactivate gameobject after specific time
    /// </summary>
    public void Deactivate()
    {
        StartCoroutine(DeactivateEffect());
    }
}
