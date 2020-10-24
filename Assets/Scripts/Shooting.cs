using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    #region Veriables

    public static Shooting Instance;

    public PlayerState.States currentState;

    [SerializeField] PoolManager ballPool;
    [SerializeField] PoolManager fxPool;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float shootingSpeed;

    int ballCount;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Instance = this;
        currentState = PlayerState.States.Wait;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentState == PlayerState.States.canShoot && ballCount > 0)
        {
            Shoot();
        }
    }

    #endregion

    #region Private Methods

    void Shoot()
    {
        ballCount--;
        GameObject _ball = ballPool.GetObjFromPool(transform.position, Quaternion.identity);
        _ball.GetComponent<Ball>().LoadBall(shootingSpeed);
    }

    public PoolManager GetPool()
    {
        return ballPool;
    }

    public Vector3 Bounce()
    {
        float xPos = Random.Range(-1f,1f);
        Vector3 bounceVector = new Vector3(xPos, 0, -1) * shootingSpeed;
        UIBalls.Instance.ClearList();

        return bounceVector;
    }

    public void SetBallCount(int count)
    {
        ballCount = count;
    }

    public void SpawnFx(Vector3 point)
    {
        GameObject fx = fxPool.GetObjFromPool(point, Quaternion.identity);
        fx.GetComponent<ParticleSystem>().startColor = ColorManager.Instance.GetColor();
        StartCoroutine(RemoveFx(fx));
    }

    IEnumerator RemoveFx (GameObject fx)
    {
        yield return new WaitForSeconds(fx.GetComponent<ParticleSystem>().duration);
        fxPool.ReturnObjToPool(fx);
    }

    public void Move()
    {
        transform.position = new Vector3(0, 0, -5);
        transform.DOMoveZ(0, .45f);
    }

    #endregion
}
