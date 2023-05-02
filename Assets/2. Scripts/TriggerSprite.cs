using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSprite : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera cmvam1;
    Transform camTransform;

    Transform myTransform; //the transform of the gameObject

    private void Awake()
    {
        myTransform = gameObject.transform;
        camTransform = cmvam1.transform;
    }

    private void LateUpdate()
    {
        myTransform.forward = camTransform.forward;
    }
}
