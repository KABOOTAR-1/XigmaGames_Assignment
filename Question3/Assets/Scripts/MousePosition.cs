using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MousePosition : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 startPos;
    private Vector2 endPos;
    private Rigidbody2D rb;
    float force;
    private float intialposition_y;
    private LineRenderer lr;
    bool canthrow;
    int count = 0;
    bool done = false;
    float waitTime;
    public Reposition repos;
    [SerializeField]
    AudioSource ballAudio;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        force = 3f;
        intialposition_y = transform.position.y;
        lr = GetComponent<LineRenderer>();
        canthrow = true;
        done = false;
        waitTime = 5f;
    }

    // Update is called once per frame
   public void Update()
    {
        if (Time.timeScale == 0)
        {
            canthrow = false;
        }

        if(Time.timeScale==1 && done==false)
        Invoke("change", 0.2f);

        if (canthrow && !done)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0))
            {
               DrawTrajectory();
            }
            else
            {
                lr.enabled = false;
            }
            if (Input.GetMouseButtonUp(0))
            {
                endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                rb.isKinematic = false;
                Vector2 dir = startPos - endPos;
                rb.velocity = dir * force;
                canthrow = false;
                done = true;
                StartCoroutine(Timeout());
            }
        }

    }
    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(waitTime);
        float x = Random.Range(-1.5f, 6f);
        repos.Repos(rb, new Vector2(x, intialposition_y));
        canthrow = true;
        done = false;

    }
    void DrawTrajectory()
    {
        lr.enabled = true;

        Vector2 DragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 _velocity = -1 * (DragPos - startPos) * force;

        Vector2[] trajectory = Projection.Plot(rb, (Vector2)transform.position, _velocity, 500);

        lr.positionCount = trajectory.Length;

        Vector3[] positions = new Vector3[trajectory.Length];

        for (int i = 0; i < trajectory.Length; i++)
        {
            positions[i] = trajectory[i];
        }

        lr.SetPositions(positions);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(done==true && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            ballAudio.Play();
        }
    }
    void change()
    {
        canthrow = true;
    }
}
