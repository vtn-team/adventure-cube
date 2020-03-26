using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{
    [SerializeField] GameObject Target = null;
    [SerializeField] Vector3 Offset = Vector3.zero;

    bool IsBulletTime = false;
    float CamRot = 130.0f;
    float Timer = 0.0f;
    Camera GameCam;

    // Use this for initialization
    void Awake()
    {
        GameCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        var LookAt = Target.transform.position;
        GameCam.transform.position = LookAt;
        GameCam.transform.position += Quaternion.AngleAxis(CamRot, Vector3.up) * Offset;
        GameCam.transform.LookAt(LookAt);

        if (IsBulletTime)
        {
            Timer += Time.unscaledDeltaTime;
            CamRot+=0.75f;
            if (Timer < 5.0f)
            {
                if (Camera.main.fieldOfView > 15.0f)
                {
                    Camera.main.fieldOfView -= 0.05f;
                }
            }
            else
            {
                if (Camera.main.fieldOfView < 45.0f)
                {
                    Camera.main.fieldOfView += 0.02f;
                }

                CamRot += 0.5f;
                Offset.x += 0.25f;
                Offset.y += 0.125f;
                if (Time.timeScale < 1.0f)
                {
                    Time.timeScale += 0.005f;
                }
                else
                {
                    Time.timeScale = 1.0f;
                    IsBulletTime = false;
                }
            }
            //if (Time.timeScale > 0.1f) Time.timeScale -= 0.05f;
        }
    }

    public void BulletTime()
    {
        Time.timeScale = 0.0f;
        IsBulletTime = true;
    }
}
