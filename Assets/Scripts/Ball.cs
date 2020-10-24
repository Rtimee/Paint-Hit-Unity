using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Veriables

    Rigidbody rb;
    RaycastHit hit;
    MeshRenderer renderer;
    Color currentColor;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        CalculateCollision();
    }

    #endregion

    #region Private Methods

    public void LoadBall(float speed)
    {
        rb.velocity = Vector3.forward * speed;
        currentColor = ColorManager.Instance.GetColor();
        renderer.material.color = currentColor;
        GetComponent<TrailRenderer>().material.color = currentColor;
        UIBalls.Instance.DestroyUIBall();
    }

    void ResetBall()
    {
        rb.velocity = Vector3.zero;
        GetComponent<TrailRenderer>().Clear();
        Shooting.Instance.GetPool().ReturnObjToPool(gameObject);
    }

    void Paint(TorusPart part,Color color,bool isTruePart)
    {
        part.Paint(color, isTruePart);
        if (isTruePart)
        {
            ResetBall();
        }
        else
            rb.velocity = Shooting.Instance.Bounce();
    }

    void CalculateCollision()
    {
        float distance;
        if(Physics.Raycast(transform.position,transform.forward,out hit,100))
        {
            if (hit.transform.CompareTag("Target"))
            {
                TorusPart hittedTorus = hit.transform.GetComponent<TorusPart>();
                distance = Vector3.Distance(transform.position, hit.point);
                if (distance < 0.1f)
                {
                    if (!hittedTorus.painted)
                    {
                        Paint(hittedTorus, currentColor, true);
                        Shooting.Instance.SpawnFx(hit.point);
                    }
                    else
                    {
                        GetComponent<SphereCollider>().isTrigger = false;
                        Paint(hittedTorus, Color.black,false);
                        hittedTorus.StopTorus();
                        StartCoroutine(UIManager.Instance.LostLevel());
                    }
                }
            }
            else if (hit.transform.CompareTag("Wall"))
            {
                distance = Vector3.Distance(transform.position, hit.point);
                if(distance < 0.1f)
                    ResetBall();
            }
        }
    }

    #endregion
}
