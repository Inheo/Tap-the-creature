using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraNextCreature : MonoBehaviour
{
    public void MoveToCreature(Transform creature, float speedMove)
    {
        Vector3 cameraPosition = transform.position;
        Vector3 endPosition = new Vector3(creature.transform.position.x, creature.transform.position.y, cameraPosition.z);
        transform.position = Vector3.MoveTowards(cameraPosition, endPosition, Time.deltaTime * speedMove);
    }
}
