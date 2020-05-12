using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    private class CharacterRain
    {
        [SerializeField]
        public Character character = null;

        [SerializeField]
        [Range(0f,1f)]
        public float rainThresholdTrigger = 0f;

        [HideInInspector]
        public bool triggered = false;
    }

    [SerializeField]
    private List<CharacterRain> characters = new List<CharacterRain>();

    [Header("End Game")]
    [SerializeField]
    private RainScript rainManager = null;
    [SerializeField]
    private float secondsBeforeRain = 20f;
    [SerializeField]
    private float secondsForRainToReachMax = 5f;

    private bool isGameStarted = false;
    
    void Start()
    {
        // init Rain Manager
        rainManager.RainIntensity = 0;

        foreach(CharacterRain cr in characters)
        {
            cr.character.GetStateMachine().enabled = false;
        }
    }
    
    void Update()
    {
        if(!isGameStarted)
        {
            if(Input.GetMouseButtonUp(0))
            {
                Debug.Log("Game Manager: START");

                isGameStarted = true;

                StartCoroutine(TimeBeforeRain());

                foreach (CharacterRain cr in characters)
                {
                    cr.character.GetStateMachine().enabled = true;
                }
            }
        } else
        {
            if(rainManager.RainIntensity > 0)
            {
                foreach(CharacterRain cr in characters)
                {
                    if(!cr.triggered && rainManager.RainIntensity >= cr.rainThresholdTrigger)
                    {
                        cr.triggered = true;

                        cr.character.NoticeRain();
                    }
                }
            }
        }
    }

    IEnumerator TimeBeforeRain()
    {
        yield return new WaitForSeconds(secondsBeforeRain);
        
        StartCoroutine(RainLerp());
    }

    IEnumerator RainLerp()
    {
        Debug.Log("Game Manager: Start Raining");

        float timer = 0f;
        while(timer < secondsForRainToReachMax)
        {
            timer += Time.deltaTime;
            rainManager.RainIntensity = Mathf.Lerp(0f, 1f, timer / secondsForRainToReachMax);
            yield return null;
        }
    }


}
