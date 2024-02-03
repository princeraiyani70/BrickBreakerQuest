using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BottomWall : MonoBehaviour
{
    public static BottomWall Instance;

    public bool IsMove = true;
    public GameObject G;

    private void Awake()
    {
        Instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsMove == true)
        {

            collision.gameObject.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            collision.gameObject.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            collision.gameObject.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            BallSpawner.Instance.gameObject.transform.position = collision.gameObject.transform.position;
            G = collision.gameObject;
            BallSpawner.Instance.Temp.Add(collision.gameObject);

            IsMove = false;
        }
        else
        {
            collision.gameObject.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            collision.gameObject.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            collision.gameObject.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            collision.gameObject.transform.DOMove(G.transform.position, 0.5f);
            BallSpawner.Instance.Temp.Add(collision.gameObject);

            if (BallSpawner.Instance.AllBollObj.Count == BallSpawner.Instance.Temp.Count)
            {
                BallSpawner.Instance.TargetObj.transform.position = new Vector2(BallSpawner.Instance.TargetObj.transform.position.x, BallSpawner.Instance.TargetObj.transform.position.y -1f);
                BallSpawner.Instance.DotTarget = true;
                BallSpawner.Instance.Temp.Clear();
            }
        }
    }

}
