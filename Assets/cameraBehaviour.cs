using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehaviour : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    public bool zoomOut = false;
    public bool zoomIn = false;
    public bool zoomOutDone = false;
    public bool zoomInDone = false;

    [SerializeField]
    private float zoomDuration = 5f;
    [SerializeField]
    private float distanceThreshold = 5f;

    private GameManager gameManager;

    [SerializeField]
    private float gameTimer = 0;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameTimer = gameManager.secondsBeforeRain + gameManager.secondsForRainToReachMax;
    }


    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameStarted)
        {
            if (gameTimer <= 0 && !zoomInDone)
            {
                zoomIn = true;
            }
            else
            {
                gameTimer -= Time.deltaTime;
                //Debug.Log(gameTimer);
            }
        }
        IEnumerator zoomingOut = LerpFromTo(startPos.position, endPos.position, zoomDuration);
        IEnumerator zoomingIn = LerpFromTo(endPos.position, startPos.position, zoomDuration);

        if (zoomIn)
        {

            if (Vector3.Distance(transform.position, startPos.position) < distanceThreshold || transform.position == startPos.position)
            {
                StopCoroutine(zoomingIn);
                zoomIn = false;
                zoomInDone = true;
                Debug.Log("done in");
            }
            StartCoroutine(zoomingIn);
        }

        IEnumerator coroutine = LerpFromTo(startPos.position, endPos.position, zoomDuration);
        if (gameManager.isGameStarted && gameTimer > 0 && !zoomOutDone)
        {
            zoomOut = true;
        }

        if (zoomOut)
        {
            if (transform.position == startPos.position)
            {
                StartCoroutine(zoomingOut);
                Debug.Log("dist " + Vector3.Distance(transform.position, endPos.position));
            }
            if (Vector3.Distance(transform.position, endPos.position) <= distanceThreshold)
            {
                zoomOut = false;
                StopCoroutine(zoomingOut);
                zoomOutDone = true;
                Debug.Log("done");
            }
        }
    }

    IEnumerator LerpFromTo(Vector3 pos1, Vector3 pos2, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(pos1, pos2, t / duration);
            yield return 0;
        }
        transform.position = pos2;
    }
}
