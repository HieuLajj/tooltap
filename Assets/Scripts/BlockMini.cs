using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DirectionBlock
{
    Down,
    Up,
    Back,
    Forward,
    Left,
    Right
}
public enum StatusBlock
{
    Die,
    Normal,
}

public class BlockMini : MonoBehaviour
{
    private StatusBlock statusBlock = StatusBlock.Normal;


    private Vector3 dir2;
    private Vector3 dir;
    private RaycastHit hit;
    private DirectionBlock Direction;
    public GameObject ModelBlock;
    public bool checkTrigger = false;
    private void OnEnable()
    {
        GetDirectionBlock(Random.Range(1,6));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            checkTrigger = true;
        }        
    }

    public void GetDirectionBlock(int input)
    {
        switch (input)
        {
            case 1:
                Direction = DirectionBlock.Down;
                dir2 = Vector3.down;
                ModelBlock.transform.localEulerAngles = new Vector3(90, 0, 0);
                break;
            case 2:
                Direction = DirectionBlock.Up;
                dir2 = Vector3.up;
                ModelBlock.transform.localEulerAngles = new Vector3(-90, 0, 0);
                break;
            case 3:
                Direction = DirectionBlock.Back;
                dir2 = Vector3.back;
                ModelBlock.transform.localEulerAngles = new Vector3(180, 0, 0);
                break;
            case 4:
                Direction = DirectionBlock.Forward;
                ModelBlock.transform.localEulerAngles = new Vector3(0, 0, 0);
                dir2 = Vector3.forward;
                break;
            case 5:
                Direction = DirectionBlock.Left;
                dir2 = Vector3.left;
                ModelBlock.transform.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 6:
                Direction = DirectionBlock.Right;
                dir2 = Vector3.right;
                ModelBlock.transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            default:
                Direction = DirectionBlock.Right;
                dir2 = Vector3.right;
                ModelBlock.transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
        }
    }

    public void checkRay()
    {
        checkDirection();
        if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, (1 << 6)))
        {
            BlockMini blockcheck = hit.collider.GetComponent<BlockMini>();
            if (blockcheck.statusBlock == StatusBlock.Die)
            {
                RunBlock();
                return;
            }
        }
        else
        {
            RunBlock();
        }
    }

    public void checkDirection()
    {
        switch (Direction)
        {
            case DirectionBlock.Left:
                dir = -transform.right;
                break;
            case DirectionBlock.Right:
                dir = transform.right;
                break;
            case DirectionBlock.Up:
                dir = transform.up;
                break;
            case DirectionBlock.Down:
                dir = -transform.up;
                break;
            case DirectionBlock.Forward:
                dir = transform.forward;
                break;
            case DirectionBlock.Back:
                dir = -transform.forward;
                break;
            default:
                dir = -transform.forward;
                break;
        }
    }
    public void RunBlock()
    {    
        statusBlock = StatusBlock.Die;
        Vector3 dir3 = transform.TransformDirection(dir2);      
        StartCoroutine(Run(dir3));
    }
    IEnumerator Run(Vector3 direction)
    {
       
        float alpha = 12;
        while (alpha >= 0)
        {
            transform.Translate(direction * 20 * Time.deltaTime, Space.World);
            yield return null;
            alpha -= 0.1f;
        }
        gameObject.SetActive(false);
    }
}
