using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StandardRK;


public class SingleCubeColor
{
    public Colors forward_X;
    public Colors forward_Y;
    public Colors forward_Z;
    public Colors backward_X;
    public Colors backward_Y;
    public Colors backward_Z;

    public SingleCubeColor()
    {
        forward_X = Colors.Green;
        forward_Y = Colors.White;
        forward_Z = Colors.Red;
        backward_X = Colors.Blue;
        backward_Y = Colors.Yellow;
        backward_Z = Colors.Orange;
    }
    
    public void Rotate(string xyz)
    {
        Colors tmp;
        switch( xyz )
        {
            case "-X":
                tmp = forward_Z;
                forward_Z = backward_Y;
                backward_Y = backward_Z;
                backward_Z = forward_Y;
                forward_Y = tmp;
                break;
            case "+X":
                tmp = forward_Z;
                forward_Z = forward_Y;
                forward_Y = backward_Z;
                backward_Z = backward_Y;
                backward_Y = tmp;
                break;
            case "-Y":
                tmp = forward_X;
                forward_X = backward_Z;
                backward_Z = backward_X;
                backward_X = forward_Z;
                forward_Z = tmp;
                break;
            case "+Y":
                tmp = forward_X;
                forward_X = forward_Z;
                forward_Z = backward_X;
                backward_X = backward_Z;
                backward_Z = tmp;
                break;
            case "+Z":
                tmp = forward_X;
                forward_X = backward_Y;
                backward_Y = backward_X;
                backward_X = forward_Y;
                forward_Y = tmp;
                break;
            case "-Z":
                tmp = forward_X;
                forward_X = forward_Y;
                forward_Y = backward_X;
                backward_X = backward_Y;
                backward_Y = tmp;
                break;
            default:
                Debug.Log("EXCEPTION: ERORR SINGLE CUBE ROTATION");
                break;
        }
    }
}
