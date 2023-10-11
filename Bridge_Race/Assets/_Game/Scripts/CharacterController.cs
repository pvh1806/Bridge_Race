using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Scriptable;
using UnityEngine;

public class CharacterController : ColorObj
{
    [SerializeField] private Animator animator;
    [SerializeField] protected List<PlayerBrick> lstPlayerBricks = new List<PlayerBrick>();
    [SerializeField] private PlayerBrick playerBrickPrefab;
    [SerializeField] private Transform brickHolder;
    private bool isJumping = true;
    public Stage stage;
    public int BrickCount => lstPlayerBricks.Count;
    public enum AnimType
    {
        Idle,
        Running,
        Dance
    }

    AnimType currentAnimName;

    public void ChangeAnimation(AnimType type)
    {
        if (currentAnimName != type)
        {
            currentAnimName = type;
            switch (type)
            {
                case AnimType.Idle:
                    animator.SetTrigger("isIdle");
                    break;
                case AnimType.Running:
                    animator.SetTrigger("isRunning");
                    break;
                case AnimType.Dance:
                    animator.SetTrigger("isDance");
                    break;
            }
        }
    }

    public void AddBrick()
    {
        PlayerBrick playerBrick = Instantiate(playerBrickPrefab, brickHolder);
        playerBrick.ChangeColor(color);
        playerBrick.transform.localPosition = Vector3.up * 0.25f * lstPlayerBricks.Count;
        lstPlayerBricks.Add(playerBrick);
    }

    public void RemoveBrick()
    {
        if (lstPlayerBricks.Count > 0)
        {
            PlayerBrick playerBrick = lstPlayerBricks[lstPlayerBricks.Count - 1];
            lstPlayerBricks.RemoveAt(lstPlayerBricks.Count - 1);
            Destroy(playerBrick.gameObject);
        }
    }

    public void ClearBrick()
    {
        for (int i = 0; i < lstPlayerBricks.Count; i++)
        {
            Destroy(lstPlayerBricks[i].gameObject);
        }
        lstPlayerBricks.Clear();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Brick brick = other.GetComponent<Brick>();
            if (brick.color == color && isJumping)
            {
                brick.OnDesSpawn();
                StartCoroutine(MoveBrick(brick));
            }
        }
    }

    private float jumpDuration = 0.3f;

    private IEnumerator MoveBrick(Brick brick)
    {
        isJumping = false;
        Transform endPos;
        if (lstPlayerBricks.Count > 0)
        {
            endPos = lstPlayerBricks[lstPlayerBricks.Count - 1].gameObject.transform;
        }
        else
        {
            endPos = brickHolder.transform;
        }

        brick.transform.DOMove(endPos.position, jumpDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            Destroy(brick.gameObject);
            AddBrick();
        });
        yield return new WaitForSeconds(jumpDuration);
        isJumping = true;
    }
    public bool isCanMove()
    {
        Ray ray = new Ray(gameObject.transform.position, Vector3.forward);
        RaycastHit hit;
        Debug.DrawRay(gameObject.transform.position, Vector3.forward, Color.green);
        if (Physics.Raycast(ray, out hit, 1f))
        {
            if (hit.collider.TryGetComponent<BrickLadder>(out BrickLadder brickLadder))
            {
                if (gameObject.transform.forward.z < 0)
                {
                    return true;
                }
                else
                {
                    if (lstPlayerBricks.Count > 0)
                    {
                        if (brickLadder.color != color)
                        {
                            brickLadder.ChangeColor(color);
                            RemoveBrick();
                        }

                        return true;
                    }
                    else
                    {
                        if (brickLadder.color == color)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}
