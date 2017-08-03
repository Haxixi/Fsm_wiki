using UnityEngine;
using DLFramework;
using DG.Tweening;
using TinyTeam.UI;

/// <summary>
/// fix
/// </summary>
public class NormalIdleState : FSMState
{
    public NormalIdleState(Transform player) : base(player)
    {
        stateID = StateID.NormalIdle;
    }

    public override void DoBeforeEnter()
    {
        playerController.ResetAnimatorBool();
        animator.SetBool(AnimatorConfig.ANI_IDLE, true);
        playerController.isSPrint = false;
        animator.applyRootMotion = false;
        playerRotateWithCamera.enabled = true;
        playerController.CameraStartRotateForward = Vector3.ProjectOnPlane(playerController.Camera.forward, new Vector3(0, 1, 0));
        playerController.CameraStartRotateRight = Vector3.ProjectOnPlane(playerController.Camera.right, new Vector3(0, 1, 0));
        WeaponManager.Instance.SwitchGun(WeaponType.WeaponRight);
        playerController.CharacterCameraProxy.SwitchCameraModeType(CameraModeType.NormalMode);
        playerController.CharacterCameraMouseLook.SetEventType(MouseLookModeType.NormalMode);
        playerController.Camera.GetComponent<CharacterCameraProxy>().isInTub = false;
    }


    public override void DoBeforeLeaving()
    {

    }

    /// <summary>
    /// Base层状态切换
    /// </summary>
    public override void BaseLayerUpdate()
    {

        if (DLInputManager.RightTrigger(ActionCode.R2) || DLInputManager.LeftTrigger(ActionCode.L2))
        {
            PlayerFsm.SetTransition(Transition.NormalIdleTSFightIdle);
            return;
        }

        else if (DLInputManager.GetButton(ActionCode.LButton))
        {
            playerRotateWithCamera.enabled = true;
            PlayerFsm.SetTransition(Transition.NormalIdleTSSprint);
            return;
        }

        else if (DLInputManager.GetButton(ActionCode.Circle))
        {
            PlayerFsm.SetTransition(Transition.NormalIdleTSPickUp);
            return;
        }

        else if (playerRayCast.isCoverR && DLInputManager.GetButton(ActionCode.Cross))
        {
            playerRotateWithCamera.enabled = false;
            CoroutineTaskManager.Instance.WaitSecondTodo(() =>
            {
                PS4ShakeManager.Instance.ShakeTrigger(PS4ShakePriorityEnum.PS4ShakePriority.BunkerShake, 0.2f, 100, 50);
            }, 0.3f);

            //if (!playerController.PlayCoverAudio)
            //{
            //    CreateAudioEffect(playerRayCast.Hit.collider.gameObject, playerRayCast.Hit.point, 0.1f);
            //    playerController.PlayCoverAudio = true;
            //}

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
            PlayerFsm.SetTransition(Transition.NormalIdleTSCoverR);
            return;
        }
        else if (playerRayCast.isCoverL && DLInputManager.GetButton(ActionCode.Cross))
        {
            playerRotateWithCamera.enabled = false;
            CoroutineTaskManager.Instance.WaitSecondTodo(() =>
            {
                PS4ShakeManager.Instance.ShakeTrigger(PS4ShakePriorityEnum.PS4ShakePriority.BunkerShake, 0.2f, 100, 50);
            }, 0.3f);

            //if (!playerController.PlayCoverAudio)
            //{
            //    CreateAudioEffect(playerRayCast.Hit.collider.gameObject, playerRayCast.Hit.point, 0.1f);
            //    playerController.PlayCoverAudio = true;
            //}

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
            PlayerFsm.SetTransition(Transition.NormalIdleTSCoverL);
            return;
        }

        else if (playerRayCast.isCoverHiR && DLInputManager.GetButton(ActionCode.Cross))
        {
            //if (!playerController.PlayCoverAudio)
            //{
            //    CreateAudioEffect(playerRayCast.m_HitHi.collider.gameObject, playerRayCast.m_HitHi.point, 0.1f);
            //    playerController.PlayCoverAudio = true;
            //}
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
            PlayerFsm.SetTransition(Transition.NormalIdleTSCoverHiR);
            return;
        }
        else if (playerRayCast.isCoverHiL && DLInputManager.GetButton(ActionCode.Cross))
        {
            //if (!playerController.PlayCoverAudio)
            //{
            //    CreateAudioEffect(playerRayCast.m_HitHi.collider.gameObject, playerRayCast.m_HitHi.point, 0.1f);
            //    playerController.PlayCoverAudio = true;
            //}
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
            PlayerFsm.SetTransition(Transition.NormalIdleTSCoverHiL);
            return;
        }

        else if (playerController.isUsePad)
        {
            playerController.CharacterCameraProxy.SwitchCameraModeType(CameraModeType.UsePadMode);
            PlayerFsm.SetTransition(Transition.NormalIdleTSUsePad);
            return;
        }

        else if (playerController.isOpenDoor)
        {
            playerController.CharacterCameraProxy.SwitchCameraModeType(CameraModeType.OpenDoorMode);
            PlayerFsm.SetTransition(Transition.NormalIdleTSOpenDoor);
            return;
        }

        else if (playerController.isTurnR)
        {
            PlayerFsm.SetTransition(Transition.NormalIdleTSTurnR);
            return;
        }

        else if (playerController.isTurnL)
        {
            PlayerFsm.SetTransition(Transition.NormalIdleTSTurnL);
            return;
        }

        else
        {
            if (!playerRayCast.isHitObj)
            {
                if (Mathf.Abs(playerController.HorizontalL) >= 0.19f || Mathf.Abs(playerController.VerticalL) >= 0.19f)
                {
                    playerRotateWithCamera.enabled = true;
                    PlayerFsm.SetTransition(Transition.NormalIdleTSNormalMove);
                    return;
                }
            }
            else
            {
                if (Mathf.Abs(playerController.VerticalL) >= 0.19f || Mathf.Abs(playerController.VerticalL) < 0.19f && playerController.HorizontalL < -0.19f)
                {
                    playerRotateWithCamera.enabled = true;
                    PlayerFsm.SetTransition(Transition.NormalIdleTSNormalMove);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 其他层状态切换
    /// </summary>
    public override void OtherLayerUpdate()
    {
        if (DLInputManager.GetButtonDown(ActionCode.R1) && !playerRayCast.isCoverR && !playerRayCast.isCoverL && !playerController.isCoverJump)
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

    /// <summary>
    /// 当前状态的Update
    /// </summary>
    public override void StateUpdate()
    {
        playerController.PlayerNormalIdle();

        if (Mathf.Abs(playerController.VerticalR) > 0.19f || Mathf.Abs(playerController.HorizontalR) > 0.19f)
        {
            playerRotateWithCamera.enabled = false;
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

    void BunkerChoose(RaycastHit ray, Vector3 increase, Vector3 dir1, int flag, Vector3 dir2)
    {
        player.eulerAngles = ray.collider.transform.eulerAngles + increase;
        Vector3 PlayerPoint = ray.point - player.position;
        float length = Vector3.ProjectOnPlane(PlayerPoint, dir1).magnitude;
        Vector3 NewPos = player.position + flag * dir2.normalized * (length - 0.8f);
        player.DOMove(NewPos, 0.5f);
    }
}
