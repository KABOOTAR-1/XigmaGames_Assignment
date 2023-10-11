using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class IncreaseScore : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 ballpos;
    public UnityEvent scoreEvent;
    [SerializeField]
    AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballpos = transform.position;
        audioSource.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (ballpos.y - transform.position.y > 0 && collision.transform.position.y < ballpos.y)
        {
            scoreEvent.Invoke();
        }
    }
}
