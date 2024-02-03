using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Color[] Colors;
    public int Life;
    public TextMeshPro txt;

    private void Start()
    {
        txt = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        txt.text = Life + "";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            if (Life > 1)
            {
                Life--;
                Points.instance.slider.value++;
                txt.text = Life + "";


                gameObject.transform.GetComponent<SpriteRenderer>().color = Colors[Random.Range(0, Colors.Length)];
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
    }
}
