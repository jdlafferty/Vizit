using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public GameObject laserPrefab;
    public SpriteRenderer digit;
    public string spritePath;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;
    private GameObject hitObject;
    private Sprite[] digits;

    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        digits = Resources.LoadAll<Sprite>(spritePath);
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;

            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100))
            {
                hitPoint = hit.point;
                hitObject = hit.collider.transform.gameObject;
                ShowLaser(hit);

                if (hitObject.tag == "GraphPoint")
                {
                    int index = hitObject.GetComponent<PointController>().GetIndex();
                    Debug.Log(index);
                    digit.sprite = digits[index];
                }
            }
        }
        else
        {
            laser.SetActive(false);
        }
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }
}