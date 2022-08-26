using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightCollision : MonoBehaviour
{
    private WeightView weightView;

    public void Init(WeightView weightView)
    {
        this.weightView = weightView;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            var angleWall = collision.gameObject.GetComponent<WallView>().Angle;
            var alpha = 180 - (180 - transform.rotation.eulerAngles.z - angleWall) * 2;
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180 - alpha);
            transform.DOMove(weightView.Point.IsRight ? transform.right * 500 : transform.right * -500, 100);
        }

        if (collision.gameObject.CompareTag("Mill"))
        {
            Debug.Log("Mill");
        }

        if (collision.gameObject.CompareTag("Star"))
        {
            Debug.Log("Star");
        }

        if (collision.gameObject.CompareTag("Barrier"))
        {
            Debug.Log("Barrier");
        }
        
    }
}
