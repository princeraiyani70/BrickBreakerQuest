using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;


public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance;

    public RaycastHit2D ray;
    public LayerMask layermask;
    public float angle;
    public Vector2 minMaxAngle;
    public bool useRay;
    public bool useLine;
    public bool useDots;
    public LineRenderer Line;
    public GameObject ballPrefab;
    public float force;
    public int ballCount;
    public GameObject firstBall, ResetBallObj;
    public bool IsMove = true;
    public bool DotTarget = true;
    public List<GameObject> AllBollObj = new List<GameObject>();
    public List<GameObject> Temp = new List<GameObject>();
    public GameObject TargetObj;
    public TextMeshProUGUI ballsTxt;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Line = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        BottomWall.Instance.G = firstBall.gameObject;

    }
    public void FixedUpdate()
    {
        if (DotTarget == false) return;

        if (Input.GetMouseButton(0))
        {
            DotTarget = true;
            ray = Physics2D.Raycast(transform.position, transform.up, 15f, layermask);

            Vector2 reflectPos = Vector2.Reflect(new Vector3(ray.point.x, ray.point.y) - transform.position, ray.normal);
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;

            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

            if (angle >= minMaxAngle.x && angle <= minMaxAngle.y)
            {
                if (useRay)
                {
                    Debug.DrawRay(transform.position, transform.up * ray.distance, Color.red);
                    Debug.DrawRay(ray.point, reflectPos.normalized * 2f, color: Color.green);
                }

                if (useLine)
                {
                    Line.SetPosition(0, transform.position);
                    Line.SetPosition(1, ray.point);
                    Line.SetPosition(2, ray.point + reflectPos.normalized * 2f);
                }

                if (useDots)
                {
                    Dots.instance.DrawDottedLine(transform.position, ray.point);
                    Dots.instance.DrawDottedLine(ray.point, ray.point + reflectPos.normalized * 2f);
                }
            }
            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        }

    }


    private void Update()
    {
        ballsTxt.text = ballCount.ToString() +" X";

        if (DotTarget == true)
        {
            ResetBallObj.SetActive(false);
            ballsTxt.gameObject.SetActive(true);
        }
        else
        {
            ResetBallObj.SetActive(true);
            ballsTxt.gameObject.SetActive(false);

        }
        if (DotTarget == false) return;

        if (Input.GetMouseButtonUp(0))
        {
            DotTarget = false;
            firstBall.SetActive(false);
            StartCoroutine(ShootBalls());
        }
    }
    bool IsStop = false;

    public IEnumerator ShootBalls()
    {
        if (IsMove == true)
        {
            AllBollObj.Clear();


            for (int i = 0; i < ballCount; i++)
            {
                yield return new WaitForSeconds(0.08f);
                GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
                AllBollObj.Add(ball);


                ball.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
                if (IsStop == true)
                {
                    break;
                }
            }
            IsMove = false;
        }
        else
        {
            BottomWall.Instance.IsMove = true;
            for (int i = 0; i < AllBollObj.Count; i++)
            {
                yield return new WaitForSeconds(0.08f);
                AllBollObj[i].GetComponent<Rigidbody2D>().AddForce(transform.up * force);
            }
        }
    }

    public void ResetBtn()
    {
        IsStop = true;
        Invoke("Demo", 0.5f);

    }

    public void Demo()
    {
        for (int i = 0; i < AllBollObj.Count; i++)
        {
            AllBollObj[i].gameObject.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            AllBollObj[i].gameObject.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            AllBollObj[i].gameObject.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            AllBollObj[i].transform.DOMove(BottomWall.Instance.G.transform.position, 0.5f);
            //Destroy(AllBollObj[i].gameObject);  
        }

        DotTarget = true;
        IsMove = true;
        Temp.Clear();
        IsStop = false;
        TargetObj.transform.position = new Vector2(TargetObj.transform.position.x, TargetObj.transform.position.y -1f);

    }
}
