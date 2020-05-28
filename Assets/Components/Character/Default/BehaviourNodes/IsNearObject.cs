using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;
using System;

[Serializable]
public class IsNearObject : NodeCondition
{
    [NodeParam]
    private string objectKey = null;
    [NodeParam]
    private float minimalRange = 1f;
    [NodeParam]
    private bool autoStop = true;
    [NodeParam]
    private bool faceObject = true;

    public override void Cleanup()
    {
    }

    protected override bool CheckCondition(float deltaTime)
    {
        Character character = behaviorTree.blackBoard["self"] as Character;

        if (character != null)
        {
            MonoBehaviour gameObject = behaviorTree.blackBoard[objectKey] as MonoBehaviour;
            if (gameObject != null)
            {
                Vector2 characterPos = new Vector2(character.transform.position.x, character.transform.position.z);
                Vector2 objectPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

                float distance = Vector2.Distance(characterPos, objectPos);

                if (distance <= minimalRange)
                {
                    if (autoStop)
                        character.Stop();

                    if (faceObject)
                    {

                        /*
                        Debug.Log("NearObject FACE");
                        Vector3 direction = objectPos - characterPos;
                        Quaternion rotation = Quaternion.LookRotation(direction);

                        character.transform.rotation = Quaternion.Lerp(character.transform.rotation, rotation, 1f * Time.deltaTime);
                        */

                        character.transform.LookAt(new Vector3(gameObject.transform.position.x, character.transform.position.y, gameObject.transform.position.z));
                    }


                    return true;
                }
            }
        }

        return false;
    }
}
