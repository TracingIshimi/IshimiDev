using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : MonoBehaviour
{
    [SerializeField] Transform pillow1;
    [SerializeField] Transform pillow2;
    [SerializeField] float distanceP;
    private int clickNum = 0;

    [SerializeField] Transform bedding1;
    [SerializeField] Transform bedding2;
    public float distanceB;
    public bool isB1Up = false;
    public bool isB2Up = false;



    public void MovePillow() {
        if (clickNum < 5) {
            Vector3 newPos1 = pillow1.position;
            Vector3 newPos2 = pillow2.position;
            newPos1.x = pillow1.position.x - distanceP;
            newPos2.x = pillow2.position.x + distanceP;
            pillow1.position = newPos1;
            pillow2.position = newPos2;
        }
        clickNum++;
    }

    public void ClickBeddig1() {
        if(!isB1Up) {
            Up(bedding1);
            isB1Up = true;
        }

        else if(isB1Up) {
            if(!isB2Up) {
                Down(bedding1);
                isB1Up = false;
            }
            else if(isB1Up) {
                return;
            }
        }
    }

    public void ClickBeddig2() {
        if(!isB1Up) {
            return;
        }

        else if(isB1Up) {
            if(!isB2Up) {
                Up(bedding2);
                isB2Up = true;
            }
            else if(isB1Up) {
                Down(bedding2);
                isB2Up = false;
            }
        }
    }

    void Down(Transform obj) {
        Vector3 newPos = obj.position;
        newPos.y = obj.position.y - distanceB;
        obj.position = newPos;
    }

    void Up(Transform obj) {
        Vector3 newPos = obj.position;
        newPos.y = obj.position.y + distanceB;
        obj.position = newPos;
    }
}
