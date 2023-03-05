using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    static float angularSpeed = 720f;
    public bool isRotating = false;
    private float LocalTargetAngle = 0;
    private float TempAngle = 0;
    private bool isPositiveRotate = true;
    private Vector3 LocalRotationAxis, LocalRotationCenter, LocalTargetPosition;
    private Quaternion LocalTargetRotation;
    public SingleCubeColor color;
    // Start is called before the first frame update
    void Start()
    {
        color = new SingleCubeColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            MainRotation();
        }
    }

    public void Initialize()
    {
        color = new SingleCubeColor();
    }

    public void RotationStart(Vector3 RotationCenter, Vector3 RotationAxis, float TargetAngle, Vector3 TargetPosition, Quaternion TargetRotation)
    {
        if (!isRotating)
        {
            isPositiveRotate = (TargetAngle > 0);
            LocalTargetAngle = Mathf.Abs(TargetAngle);
            isRotating = true;
            LocalRotationAxis = RotationAxis;
            LocalRotationCenter = RotationCenter;
            LocalTargetPosition = TargetPosition;
            LocalTargetRotation = TargetRotation;
            TempAngle = 0;
            string pnxyz = (TargetAngle>0)?"+":"-";
            int appnum = (Mathf.Abs(TargetAngle) > 150)?2:1;
            if (Vector3.Dot(RotationAxis, Vector3.right) > 0.5)
            {
                pnxyz = pnxyz + "X";
            }
            else if (Vector3.Dot(RotationAxis, Vector3.up) > 0.5)
            {
                pnxyz = pnxyz + "Y";
            }
            else if (Vector3.Dot(RotationAxis, Vector3.forward) > 0.5)
            {
                pnxyz = pnxyz + "Z";
            }
            else
            {
                EmergencyStop("CubeRotation Cannot call color.Rotate");
            }
            for (int i = 0; i < appnum; i++)
            {
                color.Rotate(pnxyz);
            }
        }
    }

    private void EmergencyStop(string inputstr)
    {
        Debug.Log(inputstr);
        //EditorUtility.DisplayDialog("Error", inputstr, "OK");
    }

    private void MainRotation()
    {
        float deltaAngle = angularSpeed * Time.deltaTime;
        if (TempAngle + deltaAngle >= LocalTargetAngle)
        {
            isRotating = false;
            transform.position = LocalTargetPosition;
            transform.rotation = LocalTargetRotation;
        }
        else
        {
            TempAngle += deltaAngle;
            this.transform.RotateAround (LocalRotationCenter, LocalRotationAxis, (isPositiveRotate)?deltaAngle:(-1)*deltaAngle);
        }
    }
}
