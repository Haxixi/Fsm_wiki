using UnityEngine;
using DG.Tweening;
using DLFramework;
using RootMotion.FinalIK;
using TinyTeam.UI;

/// <summary>
/// fix
/// </summary>
public class NormalMoveState : FSMState
{
    public NormalMoveState(Transform player) : base(player)
    {
        stateID = StateID.NormalMove;
    }

    public override void DoBeforeEnter()
    {
        playerController.ResetAnimatorBool();
        playerController.isSPrint = false;
        animator.applyRootMotion = false;
        playerRotateWithCamera.enabled = true;
        WeaponManager.Instance.SwitchGun(WeaponType.WeaponRight);
        playerController.CharacterCameraProxy.SwitchCameraModeType(CameraModeType.NormalMode);
        playerController.CharacterCameraMouseLook.SetEventType(MouseLookModeType.NormalMode);
        ik.enabled = false;
        animator.SetBool(AnimatorConfig.ANI_MOVE, true);
        playerController.Camera.GetComponent<CharacterCameraProxy>().isInTub = false;
    }

    public override void DoBeforeLeaving()
    {

    }

    public override void BaseLayerUpdate()
    {
        if (DLInputManager.LeftTrigger(ActionCode.L2) || DLInputManager.RightTrigger(ActionCode.R2))
        {
            PlayerFsm.SetTransition(Transition.NormalMoveTSFightMove);
            return;
        }

        else if (DLInputManager.GetButton(ActionCode.LButton))
        {
            PlayerFsm.SetTransition(Transition.NormalMoveTSSprint);
            return;
        }

        else if (playerRayCast.isCoverR && DLInputManager.GetButtonDown(ActionCode.Cross))
        {
            Debug.Log(1);
            playerRotateWithCamera.enabled = false;
            CoroutineTaskManager.Instance.WaitSecondTodo(() =>
            {
                PS4ShakeManager.Instance.ShakeTrigger(PS4ShakePriorityEnum.PS4ShakePriority.BunkerShake, 0.2f, 100, 50);
            }, 0.3f);

            //CreateAudioEffect(playerRayCast.Hit.collider.gameObject, playerRayCast.Hit.point, 0.1f);

            if (playerRayCast.isFirstSide)
            {
                BunkerChoose(playerRayCast.Hit, new Vector3(0, 0, 0), playerRayCast.Hit.transform.right, 1, playerRayCast.Hit.transform.forward);
            }
            else if (playerRayCast.isSecondSide)
            {
                BunkerChoose(playerRayCast.Hit, new Vector3(0, 90, 0), playerRayCast.Hit.transform.forward, 1, playerRayCast.Hit.transform.right);
            }
            else if (playerRayCast.isThirdSide)
            {
                BunkerChoose(playerRayCast.Hit, new Vector3(0, 180, 0), playerRayCast.Hit.transform.right, -1, playerRayCast.Hit.transform.forward);
            }
            else
            {
                BunkerChoose(playerRayCast.Hit, new Vector3(0, 270, 0), playerRayCast.Hit.transform.forward, -1, playerRayCast.Hit.transform.right);
            }
            PlayerFsm.SetTransition(Transition.NormalMoveTSCoverR);
            return;
        }
        else if (playerRayCast.isCoverL && DLInputManager.GetButtonDown(ActionCode.Cross))
        {
            Debug.Log(1);
            playerRotateWithCamera.enabled = false;
            CoroutineTaskManager.Instance.WaitSecondTodo(() =>
            {
                PS4ShakeManager.Instance.ShakeTrigger(PS4ShakePriorityEnum.PS4ShakePriority.BunkerShake, 0.2f, 100, 50);
            }, 0.3f);

            //CreateAudioEffect(playerRayCast.Hit.collider.gameObject, playerRayCast.Hit.point, 0.1f);

            if (playerRayCast.isFirstSide)
            {
                BunkerChoose(playerRayCast.Hit, new Vector3(0, 0, 0), playerRayCast.Hit.transform.right, 1, playerRayCast.Hit.transform.forward);
            }
            else if (playerRayCast.isSecondSide)
            {
                BunkerChoose(playerRayCast.Hit, new Vector3(0, 90, 0), playerRayCast.Hit.transform.forward, 1, playerRayCast.Hit.transform.right);
            }
            else if (playerRayCast.isThirdSide)
            {
                BunkerChoose(playerRayCast.Hit, new Vector3(0, 180, 0), playerRayCast.Hit.transform.right, -1, playerRayCast.Hit.transform.forward);
            }
            else
            {
                BunkerChoose(playerRayCast.Hit, new Vector3(0, 270, 0), playerRayCast.Hit.transform.forward, -1, playerRayCast.Hit.transform.right);
            }
            PlayerFsm.SetTransition(Transition.NormalMoveTSCoverL);
            return;
        }

        else if (playerRayCast.isCoverHiR && DLInputManager.GetButtonDown(ActionCode.Cross))
        {
            Debug.Log(1);
            playerRotateWithCamera.enabled = false;

            if (playerRayCast.isFirstSide)
            {
                BunkerChoose(playerRayCast.HitHi, new Vector3(0, 0, 0), playerRayCast.HitHi.transform.right, 1, playerRayCast.HitHi.transform.forward);
            }
            else if (playerRayCast.isSecondSide)
            {
                BunkerChoose(playerRayCast.HitHi, new Vector3(0, 90, 0), playerRayCast.HitHi.transform.forward, 1, playerRayCast.HitHi.transform.right);
            }
            else if (playerRayCast.isThirdSide)
            {
                BunkerChoose(playerRayCast.HitHi, new Vector3(0, 180, 0), playerRayCast.HitHi.transform.right, -1, playerRayCast.HitHi.transform.forward);
            }
            else
            {
                BunkerChoose(playerRayCast.HitHi, new Vector3(0, 270, 0), playerRayCast.HitHi.transform.forward, -1, playerRayCast.HitHi.transform.right);
            }
            PlayerFsm.SetTransition(Transition.NormalMoveTSCoverHiR);
            return;

        }
        else if (playerRayCast.isCoverHiL && DLInputManager.GetButtonDown(ActionCode.Cross))
        {
            Debug.Log(1);
            playerRotateWithCamera.enabled = false;

            if (playerRayCast.isFirstSide)
            {
                BunkerChoose(playerRayCast.HitHi, new Vector3(0, 0, 0), playerRayCast.HitHi.transform.right, 1, playerRayCast.HitHi.transform.forward);
            }
            else if (playerRayCast.isSecondSide)
            {
                BunkerChoose(playerRayCast.HitHi, new Vector3(0, 90, 0), playerRayCast.HitHi.transform.forward, 1, playerRayCast.HitHi.transform.right);
            }
            else if (playerRayCast.isThirdSide)
            {
                BunkerChoose(playerRayCast.HitHi, new Vector3(0, 180, 0), playerRayCast.HitHi.transform.right, -1, playerRayCast.HitHi.transform.forward);
            }
            else
            {
                BunkerChoose(playerRayCast.HitHi, new Vector3(0, 270, 0), playerRayCast.HitHi.transform.forward, -1, playerRayCast.HitHi.transform.right);
            }

            PlayerFsm.SetTransition(Transition.NormalMoveTSCoverHiL);
            return;
        }

        else if (CanRoll())
        {
            PlayerFsm.SetTransition(Transition.NormalMoveTSRoll);
            return;
        }

        else if (playerController.isOpenDoor)
        {
            playerController.CharacterCameraProxy.SwitchCameraModeType(CameraModeType.OpenDoorMode);
            PlayerFsm.SetTransition(Transition.NormalMoveTSOpenDoor);
            return;
        }

        else if (playerController.isUsePad)
        {
            playerController.CharacterCameraProxy.SwitchCameraModeType(CameraModeType.UsePadMode);
            PlayerFsm.SetTransition(Transition.NormalMoveTSUsePad);
            return;
        }

        else
        {
            if (!playerRayCast.isHitObj)
            {
                if (Mathf.Abs(playerController.HorizontalL) < 0.19f && Mathf.Abs(playerController.VerticalL) < 0.19f)
                {
                    ik.enabled = false;
                    PlayerFsm.SetTransition(Transition.NormalMoveTSNormalIdle);
                    return;
                }
            }
            else
            {
                if (Mathf.Abs(playerController.VerticalL) < 0.19f && (playerController.HorizontalL >= 0.75f || Mathf.Abs(playerController.HorizontalL) < 0.19f))
                {
                    ik.enabled = false;
                    PlayerFsm.SetTransition(Transition.NormalMoveTSNormalIdle);
                    return;
                }
            }
        }
    }



    public override void OtherLayerUpdate()
    {
        if (DLInputManager.GetButton(ActionCode.R1) && !playerRayCast.isCoverR && !playerRayCast.isCoverL && !playerController.isCoverJump)
        {
            animator.SetBool(AnimatorConfig.ANI_RELOAD, true);
            WeaponManager.Instance.Reload();
            CoroutineTaskManager.Instance.WaitSecondTodo(() =>
            {
                animator.SetBool(AnimatorConfig.ANI_RELOAD, false);
            }, WeaponManager.Instance.CurrentWeaponHandle.ReloadAnimatorDuration);
        }

        else if (DLInputManager.GetButton(ActionCode.Square) && !playerRayCast.isCoverR && !playerRayCast.isCoverL && !playerController.isCoverJump)
        {
            if (playerController.UIStrength.isCanAttack)
            {
                int dir = playerController.CreateRandomDir();
                playerController.UIStrength.isCanAttack = false;
                if (dir <= 1)
                {
                    animator.SetBool(AnimatorConfig.ANI_PUNCH, true);
                }
                else
                {
                    animator.SetBool(AnimatorConfig.ANI_KICK, true);
                    WeaponManager.Instance.SwitchGun(WeaponType.WeaponLeft);
                }
                animator.applyRootMotion = true;
                TTUIPage.ShowPage<UIStrength>();
                playerController.SetSpeedZero();
                APPMain.Facade.worldData.MTblackBoard.SetValue("CanHideMeleeUI", true);
                CoroutineTaskManager.Instance.WaitSecondTodo(() =>
                {
                    playerController.ResetSpeed();
                    animator.SetBool(AnimatorConfig.ANI_PUNCH, false);
                    animator.SetBool(AnimatorConfig.ANI_KICK, false);
                    animator.applyRootMotion = false;
                    WeaponManager.Instance.SwitchGun(WeaponType.WeaponRight);
                }, 1);
                CoroutineTaskManager.Instance.WaitSecondTodo(() =>
                {
                    playerController.UIStrength.isCanAttack = true;
                }, 2);
            }
        }
    }

    public override void StateUpdate()
    {
        playerController.PlayerMove();

        if (!animator.GetBool(AnimatorConfig.ANI_BOMB))
        {
            CameraShakeManager.Instance.CameraShake(CameraShakeType.PlayerMoveShake);
        }
    }

    void CreateAudioEffect(GameObject obj, Vector3 pos, float duration)
    {
        CoroutineTaskManager.Instance.WaitSecondTodo(() =>
        {
            //PlayerAudioEffect.SpawnBunkerAudioEffect(pos, duration);
            //DLAudioManager.Instance.PlayObjectSE(obj, "bunker");
        }, 0.1f);
    }

    private bool CanRoll()
    {
        if (!animator.GetBool(AnimatorConfig.ANI_PUNCH) && !animator.GetBool(AnimatorConfig.ANI_KICK)
            && !animator.GetBool(AnimatorConfig.ANI_BOMB) && !animator.GetBool(AnimatorConfig.ANI_ISHIT)
            && !animator.GetBool(AnimatorConfig.ANI_BULLET) && DLInputManager.GetButtonDown(ActionCode.Circle))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void BunkerChoose(RaycastHit ray, Vector3 increase, Vector3 dir1, int flag, Vector3 dir2)
    {
        player.eulerAngles = ray.collider.transform.eulerAngles + increase;
        Vector3 PlayerPoint = ray.point - player.position;
        float length = Vector3.ProjectOnPlane(PlayerPoint, dir1).magnitude;
        Vector3 NewPos = player.position + flag * dir2.normalized * (length - 0.8f);
        player.DOMove(NewPos, 0.5f);
    }
}
