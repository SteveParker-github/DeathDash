using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetTran;
    [SerializeField]
    private Vector3 targetOffset;

    public float minCamY = 5;
    public float maxCamY = 50;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = targetTran.position + targetOffset;
        //min cam floor level = 5, will be a variable that can be set later.
        newPos.y = Mathf.Clamp(newPos.y, minCamY, maxCamY);
        transform.position = newPos;

    }
}
