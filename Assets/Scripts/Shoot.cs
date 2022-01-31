using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Shoot : MonoBehaviour
{
    public float power = 2.0f;
    public float life = 1.0f;
    public float dead_sense = 10f;
    public int dots = 30;
    private Vector2 startPosition;
    public bool shoot = false, aiming = false, hit_ground = false;
    private GameObject Dots;
    private List<GameObject> projectilesPath;
    private Rigidbody2D rigidbody2D;
    private Collider2D collider2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }
    private void Start()
    {
        Dots = GameObject.Find("dots");
        rigidbody2D.isKinematic = true;
        collider2D.enabled = false;
        startPosition = transform.position;
        projectilesPath = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        for(int i = 0; i < projectilesPath.Count; i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled = false;
        }
    }
    private void Update()
    {
        Aim();
        if (hit_ground)
        {
            life -= Time.deltaTime;
            Color c = GetComponent<Renderer>().material.GetColor("_Color");
            GetComponent<Renderer>().material.SetColor("_Color", new Color(c.r, c.g, c.b, life));
            if (life < 0)
            {
                if(GameManager.instance!= null)
                {
                    GameManager.instance.CreateBall();
                }
                Destroy(gameObject);
            }
        }
    }
    void Aim()
    {
        if (shoot)
        {
            return;
        }
        if (Input.GetAxis("Fire1") == 1)
        {
            if (!aiming)
            {
                aiming = true;
                startPosition = Input.mousePosition;
                CalculatePath();
                ShowPath();
            }
            else
            {
                CalculatePath();
            }
        }
        else if(aiming && !shoot)
        {
            if (inDeadZone(Input.mousePosition) || inReleaseZone(Input.mousePosition))
            {
                aiming = false;
                HidePath();
                return;
            }
            rigidbody2D.isKinematic = false;
            collider2D.enabled = true;
            shoot = true;
            aiming = false;
            rigidbody2D.AddForce(GetForce(Input.mousePosition));
            HidePath();
            GameManager.instance.DecrementBalls();
        }
    }
    Vector2 GetForce(Vector3 mouse)
    {
        return (new Vector2(startPosition.x, startPosition.y) - new Vector2(mouse.x, mouse.y)) * power;
    }
    bool inDeadZone(Vector2 mouse)
    {
        if (Mathf.Abs(startPosition.x - mouse.x) <= dead_sense && Mathf.Abs(startPosition.y-mouse.y)<= dead_sense)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool inReleaseZone(Vector2 mouse)
    {
        if(mouse.x<= 70)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void CalculatePath()
    {
        Vector2 vel = GetForce(Input.mousePosition) * Time.fixedDeltaTime / rigidbody2D.mass;
        for(int i = 0; i < projectilesPath.Count; i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled = true;
            float t = i / 30f;
            Vector3 point = PathPoint(transform.position, vel, t);
            point.z = 1.0f;
            projectilesPath[i].transform.position = point;
        }
    }
    Vector2 PathPoint(Vector2 startP,Vector2 startVel,float t)
    {
        return startP + startVel * t + 0.5f * Physics2D.gravity * t * t;
    }
    void HidePath()
    {
        for (int i = 0; i < projectilesPath.Count; i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled = false;
        }
    }
    void ShowPath()
    {
        for (int i = 0; i < projectilesPath.Count; i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            hit_ground = true;
        }
    }
}
