using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickControl : MonoBehaviour
{
    [SerializeField] List<PlayerBrick> bricks;
    [SerializeField] GameObject brickPrefab;
    [SerializeField] Transform playerModel;
    [SerializeField] Transform brickContainer;

    [SerializeField] Animator animator;


    const float _BRICK_HEIGHT = 0.3f;
    public void AddBrick()
    {
#if UNITY_EDITOR
        Debug.Log("AddBrick");
#endif
        GameManager.instance.GetBrick();
        //animator.SetTrigger("AddBrick");
        //StartCoroutine(AddBrickToScene());
        AddBrickToScene();
    }

    private /*IEnumerator*/ void AddBrickToScene()
    {
        Transform newBrick = Instantiate(brickPrefab, brickContainer).transform;
        newBrick.localPosition = Vector3.zero;

        playerModel.position += _BRICK_HEIGHT * Vector3.up;
        foreach (PlayerBrick brick in bricks)
        {
            brick.transform.localPosition += _BRICK_HEIGHT * Vector3.up;
        }
        bricks.Add(newBrick.GetComponent<PlayerBrick>());
        //yield return new WaitForSeconds(0.35f);

        
    }

    public bool PlaceBrick(Transform newParent)
    {
        if (bricks.Count > 0)
        {
            PlayerBrick brick = bricks[bricks.Count - 1];
            bricks.RemoveAt(bricks.Count - 1);
            Destroy(brick.gameObject);
            playerModel.position -= _BRICK_HEIGHT * Vector3.up;
            foreach (PlayerBrick _brick in bricks)
            {
                _brick.transform.localPosition -= _BRICK_HEIGHT * Vector3.up;
            }

            Instantiate(brickPrefab, newParent).transform.localPosition = Vector3.zero;

            return true;
        }

        return false;
    }
}
