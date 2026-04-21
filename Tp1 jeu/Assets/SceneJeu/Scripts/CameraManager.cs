using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cameraOverview;
    public Camera cameraJaune;


    private Camera[] cameras;
    private int idx = 0;

    void Start()
    {
        cameras = new Camera[] { cameraOverview, cameraJaune };
        Activate(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            idx = (idx + 1) % cameras.Length;
            Activate(idx);
        }
    }

    void Activate(int i)
    {
        for (int j = 0; j < cameras.Length; j++)
            cameras[j].gameObject.SetActive(j == i);
    }
}