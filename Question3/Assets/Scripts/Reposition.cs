using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition :MonoBehaviour
{
    public UnityEngine.Events.UnityEvent ReduceLives;
    public void Repos(Rigidbody2D rb,Vector2 newpos)
    {
        if (GameManager.scored == false)
        {
            ReduceLives.Invoke();
        }
        rb.isKinematic = true;
        float x = Random.Range(-1.5f, 6f);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.transform.position = newpos;
        rb.transform.rotation = Quaternion.identity;
        GameManager.scored = false;
    }
}
