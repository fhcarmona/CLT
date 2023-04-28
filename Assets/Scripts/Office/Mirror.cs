using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField]
    private Camera MirrorCam;
    [SerializeField]
    private Camera PlayerCam;
    
    // Update is called once per frame
    public void Update()
    {
        CalculateRotation();
    }

    private void CalculateRotation()
    {
        Vector3 direction = (PlayerCam.transform.position - MirrorCam.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        rotation.eulerAngles = transform.eulerAngles - rotation.eulerAngles;

        MirrorCam.transform.localRotation = rotation;
    }
}
