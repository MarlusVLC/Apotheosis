using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformations : MonoBehaviour
{
    [SerializeField] bool translate;
    [SerializeField] float moveX, moveY, moveZ;

    [SerializeField] bool rotateLocal;
    [SerializeField] float rotateX, rotateY, rotateZ;

    [SerializeField] bool rotateAround;
    [SerializeField] GameObject centerPoint;
    [SerializeField] float rotAroundX, rotAroundY, rotAroundZ;
    [SerializeField] float axisPercentX, axisPercentY, axisPercentZ;
    [SerializeField] float angle;

    [SerializeField] bool scale;
    [SerializeField] float scaleX, scaleY, scaleZ;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (translate == true)
        {
            transform.Translate(new Vector3(moveX, moveY, moveZ), Space.World);

            if (transform.position.x > 11)
            {
                transform.position = new Vector3(-11f, 0, 0);
            }
        }

        if (rotateLocal == true)
        {
            transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime, Space.World);
        }

        if (rotateAround == true)
        {
            if (centerPoint != null)
            {
                transform.RotateAround(centerPoint.transform.position,
                                        new Vector3(axisPercentX, axisPercentY, axisPercentZ),
                                        angle);
            }
            else
            {
                transform.RotateAround( new Vector3(rotAroundX, rotAroundY, rotAroundZ),
                                        new Vector3(axisPercentX, axisPercentY, axisPercentZ),
                                        angle);
            }
        }

        if (scale == true)
        {
            transform.localScale += new Vector3(scaleX, scaleY, scaleZ);
        }
    }
}
