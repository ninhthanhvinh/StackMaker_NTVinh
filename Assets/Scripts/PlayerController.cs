using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Direction
{
    Up = 0,
    Left = 1,
    Down = 2,
    Right = 3
}

public class PlayerController : MonoBehaviour
{
    private Direction direction;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Vector3 destination;
    private bool isMoving = false;
    [SerializeField] float speed = 2f;
    [SerializeField] float rotationSpeed = 0.5f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponentInChildren<Animator>();
       destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            startTouchPosition = Input.GetTouch(0).position;
        if (!isMoving && 
            Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
            
            if (Mathf.Abs(endTouchPosition.x - startTouchPosition.x) > Mathf.Abs(endTouchPosition.y - startTouchPosition.y))
                direction = endTouchPosition.x - startTouchPosition.x > 0 ? Direction.Right : Direction.Left;
            else
                direction = endTouchPosition.y - startTouchPosition.y > 0 ? Direction.Up : Direction.Down;

            destination = GetDestination(direction);
            isMoving = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, destination) == 0)
        {
            isMoving = false;
        }

    }

    public void Goal()
    {
        animator.SetTrigger("Goal");
        GameManager.instance.StartCoroutine(GameManager.instance.OnWin());
    }

    private Vector3 GetDestination(Direction _direction)
    {
        RaycastHit hit;
        Vector3 rayDirection = Vector3.zero;
        switch (_direction)
        {
            case Direction.Up:
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed);
                rayDirection = Vector3.forward;
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), rotationSpeed);
                rayDirection = Vector3.left;
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), rotationSpeed);
                rayDirection = Vector3.back;
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), rotationSpeed);
                rayDirection = Vector3.right;
                break;
            default:
                break;
        }

        Physics.Raycast(transform.position, rayDirection, out hit, 100f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            Vector3 sub = rayDirection / 2;
            return (hit.point - sub);
        }

        return transform.position;
            
    }
}
