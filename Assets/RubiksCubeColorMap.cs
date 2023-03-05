using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StandardRK;

public class RubiksCubeColorMap
{
    Colors[,] XPSideColor = new Colors[5,5];
    Colors[,] XNSideColor = new Colors[5,5];
    Colors[,] YPSideColor = new Colors[5,5];
    Colors[,] YNSideColor = new Colors[5,5];
    Colors[,] ZPSideColor = new Colors[5,5];
    Colors[,] ZNSideColor = new Colors[5,5];

    // Start is called before the first frame update
    public RubiksCubeColorMap()
    {
        for (int Y = 0; Y < 5; Y++)
        {
            for (int X = 0; X < 5; X++)
            {
                XPSideColor[Y, X] = Colors.Green;
                YPSideColor[Y, X] = Colors.White;
                ZPSideColor[Y, X] = Colors.Red;
                XNSideColor[Y, X] = Colors.Blue;
                YNSideColor[Y, X] = Colors.Yellow;
                ZNSideColor[Y, X] = Colors.Orange;
            }
        }
    }

    public void  SetRK(GameObject[,,] RK)
    {
        // Positive X
        int X, Y, Z;
        X = 4;
        for (Y = 0; Y < 5; Y++)
        {
            for (Z = 0; Z < 5; Z++)
            {
                XPSideColor[4-Y, Z] = RK[X, Y, Z].GetComponent<CubeRotation>().color.forward_X;
            }
        }

        // Negative X
        X = 0;
        for (Y = 0; Y < 5; Y++)
        {
            for (Z = 0; Z < 5; Z++)
            {
                XNSideColor[4-Y, 4-Z] = RK[X, Y, Z].GetComponent<CubeRotation>().color.backward_X;
            }
        }

        // Positive Y
        Y = 4;
        for (X = 0; X < 5; X++)
        {
            for (Z = 0; Z < 5; Z++)
            {
                YPSideColor[X, Z] = RK[X, Y, Z].GetComponent<CubeRotation>().color.forward_Y;
            }
        }

        // Negative Y
        Y = 0;
        for (X = 0; X < 5; X++)
        {
            for (Z = 0; Z < 5; Z++)
            {
                YNSideColor[4-X, Z] = RK[X, Y, Z].GetComponent<CubeRotation>().color.backward_Y;
            }
        }

        // Positive Z
        Z = 4;
        for (X = 0; X < 5; X++)
        {
            for (Y = 0; Y < 5; Y++)
            {
                ZPSideColor[4-Y, 4-X] = RK[X, Y, Z].GetComponent<CubeRotation>().color.forward_Z;
            }
        }

        // Negative Z
        Z = 0;
        for (X = 0; X < 5; X++)
        {
            for (Y = 0; Y < 5; Y++)
            {
                ZNSideColor[4-Y, X] = RK[X, Y, Z].GetComponent<CubeRotation>().color.backward_Z;
            }
        }
    }

    public Colors[,] GetSideColor(string xyz)
    {
        switch( xyz )
        {
            case "-X":
                return XNSideColor;
            case "+X":
                return XPSideColor;
                
            case "-Y":
                return YNSideColor;
                
            case "+Y":
                return YPSideColor;
                
            case "-Z":
                return ZNSideColor;
                
            case "+Z":
                return ZPSideColor;
                
            default:
                Debug.Log("EXCEPTION: ERORR GET SIDE COLOR");
                return null;
                
        }
    } 

    public Colors GetCellColor(string xyz, int ind1, int ind2)
    {
        switch( xyz )
        {
            case "-X":
                return XNSideColor[ind1, ind2];
                
            case "+X":
                return XPSideColor[ind1, ind2];
                
            case "-Y":
                return YNSideColor[ind1, ind2];
                
            case "+Y":
                return YPSideColor[ind1, ind2];
                
            case "-Z":
                return ZNSideColor[ind1, ind2];
                
            case "+Z":
                return ZPSideColor[ind1, ind2];

            default:
                Debug.Log("EXCEPTION: ERORR GET SIDE COLOR");
                return Colors.ErrorCol;
        }
    }

    public string GetCellColorStr(string xyz, int ind1, int ind2)
    {
        Colors cout;
        switch( xyz )
        {
            case "-X":
                cout = XNSideColor[ind1, ind2];
                break;
            case "+X":
                cout = XPSideColor[ind1, ind2];
                break;
            case "-Y":
                cout = YNSideColor[ind1, ind2];
                break;
            case "+Y":
                cout = YPSideColor[ind1, ind2];
                break;
            case "-Z":
                cout = ZNSideColor[ind1, ind2];
                break;
            case "+Z":
                cout = ZPSideColor[ind1, ind2];
                break;
            default:
                Debug.Log("EXCEPTION: ERORR GET SIDE COLOR");
                cout = Colors.ErrorCol;
                break;
        }

        switch (cout)
        {
            case (Colors.Red):
                return "Red";

            case (Colors.Green):
                return "Green";

            case (Colors.Blue):
                return "Blue";

            case (Colors.Yellow):
                return "Yellow";

            case (Colors.White):
                return "White";

            case (Colors.Orange):
                return "Orange";

            case (Colors.Black):
                return "Black";

            default:
                return "ErrorCol";
            
        }
    }

}
