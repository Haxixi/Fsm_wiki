using System.Collections;
using System.Collections.Generic;
using DLFramework;
using RootMotion.FinalIK;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    private FSMSystem fsm;
    public Animator m_animator { get; set; }
    public CharacterController m_CharacterController { get; set; }
    public CapsuleCollider m_CapsuleCollider { get; set; }
    public AimIK m_AimIk { get; set; }

    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_CapsuleCollider = GetComponent<CapsuleCollider>();
        m_CharacterController = GetComponent<CharacterController>();
        m_AimIk = GetComponent<AimIK>();
    }

    // Use this for initialization
    void Start()
    {
        MakeFSM();
    }


    void OnEnable()
    {
        m_animator.SetBool(AnimatorConfig.ANI_IDLE, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (APPMain.gameIsPause)
        {
            return;
        }

        if (fsm.CurrentState != null)
        {
            fsm.CurrentState.OtherLayerUpdate();
            fsm.CurrentState.StateUpdate();
            if (fsm.isTransition || m_animator.IsInTransition(0))
                return;
            fsm.CurrentState.BaseLayerUpdate();
        }
    }

    public void SetTransition(Transition t)
    {
        fsm.PerformTransition(t);
    }

    private void MakeFSM()
    {

        NormalIdleState normalIdle = new NormalIdleState(this.transform);
        normalIdle.AddTransition(Transition.NormalIdleTSTurnR, StateID.TurnR);//
        normalIdle.AddTransition(Transition.NormalIdleTSTurnL, StateID.TurnL);//
        normalIdle.AddTransition(Transition.NormalIdleTSNormalMove, StateID.NormalMove);//
        normalIdle.AddTransition(Transition.NormalIdleTSFightIdle, StateID.FightIdle);//
        normalIdle.AddTransition(Transition.NormalIdleTSSprint, StateID.Sprint);//
        normalIdle.AddTransition(Transition.NormalIdleTSCoverR, StateID.CoverR);//
        normalIdle.AddTransition(Transition.NormalIdleTSCoverL, StateID.CoverL);//
        normalIdle.AddTransition(Transition.NormalIdleTSCoverHiR, StateID.CoverHiR);
        normalIdle.AddTransition(Transition.NormalIdleTSCoverHiL, StateID.CoverHiL);
        normalIdle.AddTransition(Transition.NormalIdleTSPickUp, StateID.PickUp);//
        normalIdle.AddTransition(Transition.NormalIdleTSOpenDoor, StateID.OpenDoor);//
        normalIdle.AddTransition(Transition.NormalIdleTSUsePad, StateID.UsePad);//

        TurnRState turnR = new TurnRState(this.transform);
        turnR.AddTransition(Transition.TurnRTSTurnL, StateID.TurnL);
        turnR.AddTransition(Transition.TurnRTSNormalIdle, StateID.NormalIdle);
        turnR.AddTransition(Transition.TurnRTSNormalMove, StateID.NormalMove);

        TurnLState turnL = new TurnLState(this.transform);
        turnL.AddTransition(Transition.TurnLTSNormalIdle, StateID.NormalIdle);
        turnL.AddTransition(Transition.TurnLTSNormalMove, StateID.NormalMove);

        FightIdleState fightIdle = new FightIdleState(this.transform);
        fightIdle.AddTransition(Transition.FightIdleTSFightMove, StateID.FightMove);//
        fightIdle.AddTransition(Transition.FightIdleTSNormalMove, StateID.NormalMove);//
        fightIdle.AddTransition(Transition.FightIdleTSNormalIdle, StateID.NormalIdle);//

        NormalMoveState normalMove = new NormalMoveState(this.transform);
        normalMove.AddTransition(Transition.NormalMoveTSFightMove, StateID.FightMove);//
        normalMove.AddTransition(Transition.NormalMoveTSNormalIdle, StateID.NormalIdle);//
        normalMove.AddTransition(Transition.NormalMoveTSSprint, StateID.Sprint);//
        normalMove.AddTransition(Transition.NormalMoveTSRoll, StateID.Roll);//
        normalMove.AddTransition(Transition.NormalMoveTSCoverR, StateID.CoverR);//
        normalMove.AddTransition(Transition.NormalMoveTSCoverL, StateID.CoverL);//
        normalMove.AddTransition(Transition.NormalMoveTSCoverHiR, StateID.CoverHiR);
        normalMove.AddTransition(Transition.NormalMoveTSCoverHiL, StateID.CoverHiL);
        normalMove.AddTransition(Transition.NormalMoveTSOpenDoor, StateID.OpenDoor);//
        normalMove.AddTransition(Transition.NormalMoveTSUsePad, StateID.UsePad);//

        FightMoveState fightMove = new FightMoveState(this.transform);
        fightMove.AddTransition(Transition.FightMoveTSFightIdle, StateID.FightIdle);//
        fightMove.AddTransition(Transition.FightMoveTSNormalMove, StateID.NormalMove);//

        RollState roll = new RollState(this.transform);
        roll.AddTransition(Transition.RollTSNormalIdle, StateID.NormalIdle);//
        roll.AddTransition(Transition.RollTSNormalMove, StateID.NormalMove);//

        SprintState sprint = new SprintState(this.transform);
        sprint.AddTransition(Transition.SprintTSNormalIdle, StateID.NormalIdle);//
        sprint.AddTransition(Transition.SprintTSNormalMove, StateID.NormalMove);//
        sprint.AddTransition(Transition.SprintTSCoverR, StateID.CoverR);//
        sprint.AddTransition(Transition.SprintTSCoverL, StateID.CoverL);//
        sprint.AddTransition(Transition.SprintTSRoll, StateID.Roll);//
        sprint.AddTransition(Transition.SprintTSCoverHiR, StateID.CoverHiR);
        sprint.AddTransition(Transition.SprintTSCoverHiL, StateID.CoverHiL);
        sprint.AddTransition(Transition.SprintTSOpenDoor, StateID.OpenDoor);//
        sprint.AddTransition(Transition.SprintTSUsePad, StateID.UsePad);//

        CoverJumpState coverJump = new CoverJumpState(this.transform);
        coverJump.AddTransition(Transition.CoverJumpTSNormalIdle, StateID.NormalIdle);//

        CoverRState coverR = new CoverRState(this.transform);
        coverR.AddTransition(Transition.CoverRTSCoverL, StateID.CoverL);//
        coverR.AddTransition(Transition.CoverRTSRoll, StateID.Roll);//
        coverR.AddTransition(Transition.CoverRTSNormalMove, StateID.NormalMove);//
        coverR.AddTransition(Transition.CoverRTSCoverJump, StateID.CoverJump);//
        coverR.AddTransition(Transition.CoverRTSCoverLoRShootIdle, StateID.CoverLoRShootIdle);//
        coverR.AddTransition(Transition.CoverRTSCoverRReload, StateID.CoverRReload);//
        coverR.AddTransition(Transition.CoverRTSNormalIdle, StateID.NormalIdle);//

        CoverLoRShootIdleState coverLoRShootIdle = new CoverLoRShootIdleState(this.transform);
        coverLoRShootIdle.AddTransition(Transition.CoverLoRShootIdleTSCoverLoRShootBeforeDown, StateID.CoverLoRShootBeforeDown);//
        coverLoRShootIdle.AddTransition(Transition.CoverLoRShootIdleTSFightIdle, StateID.FightIdle);//
        coverLoRShootIdle.AddTransition(Transition.CoverLoRShootIdleTSCoverR, StateID.CoverR);//

        CoverLoRShootBeforeDownState coverLoRShootBeforeDown = new CoverLoRShootBeforeDownState(this.transform);
        coverLoRShootBeforeDown.AddTransition(Transition.CoverLoRShootBeforeDownTSCoverR, StateID.CoverR);//
        coverLoRShootBeforeDown.AddTransition(Transition.CoverLoRShootBeforeDownTSCoverLoRShootIdle, StateID.CoverLoRShootIdle);//
        coverLoRShootBeforeDown.AddTransition(Transition.CoverLoRShootBeforeDownTSNormalIdle, StateID.NormalIdle);//

        CoverLState coverL = new CoverLState(this.transform);
        coverL.AddTransition(Transition.CoverLTSCoverR, StateID.CoverR);//
        coverL.AddTransition(Transition.CoverLTSRoll, StateID.Roll);//
        coverL.AddTransition(Transition.CoverLTSNormalMove, StateID.NormalMove);//
        coverL.AddTransition(Transition.CoverLTSCoverJump, StateID.CoverJump);//
        coverL.AddTransition(Transition.CoverLTSCoverLoLShootIdle, StateID.CoverLoLShootIdle);//
        coverL.AddTransition(Transition.CoverLTSCoverLReload, StateID.CoverLReload);//
        coverL.AddTransition(Transition.CoverLTSNormalIdle, StateID.NormalIdle);//

        CoverLoLShootIdleState coverLoLShootIdle = new CoverLoLShootIdleState(this.transform);
        coverLoLShootIdle.AddTransition(Transition.CoverLoLShootIdleTSCoverLoLShootBeforeDown, StateID.CoverLoLShootBeforeDown);//
        coverLoLShootIdle.AddTransition(Transition.CoverLoLShootIdleTSFightIdle, StateID.FightIdle);//
        coverLoLShootIdle.AddTransition(Transition.CoverLoLShootIdleTSCoverL, StateID.CoverL);//

        CoverLoLShootBeforeDownState coverLoLShootBeforeDown = new CoverLoLShootBeforeDownState(this.transform);
        coverLoLShootBeforeDown.AddTransition(Transition.CoverLoLShootBeforeDownTSCoverL, StateID.CoverL);//
        coverLoLShootBeforeDown.AddTransition(Transition.CoverLoLShootBeforeDownTSCoverLoLShootIdle, StateID.CoverLoLShootIdle);//
        coverLoLShootBeforeDown.AddTransition(Transition.CoverLoLShootBeforeDownTSNormalIdle, StateID.NormalIdle);//

        CoverHiRState coverHiR = new CoverHiRState(this.transform);
        coverHiR.AddTransition(Transition.CoverHiRTSCoverHiL, StateID.CoverHiL);
        coverHiR.AddTransition(Transition.CoverHiRTSNormalMove, StateID.NormalMove);
        coverHiR.AddTransition(Transition.CoverHiRTSCoverHiRightIdle, StateID.CoverHiRightIdle);
        coverHiR.AddTransition(Transition.CoverHiRTSNormalIdle, StateID.NormalIdle);

        CoverHiRightIdleState coverHiRightIdle = new CoverHiRightIdleState(this.transform);
        coverHiRightIdle.AddTransition(Transition.CoverHiRightIdleTSCoverHiR, StateID.CoverHiR);
        coverHiRightIdle.AddTransition(Transition.CoverHiRightIdleTSRoll, StateID.Roll);
        coverHiRightIdle.AddTransition(Transition.CoverHiRightIdleTSCoverHiRShootIdle, StateID.CoverHiRShootIdle);
        coverHiRightIdle.AddTransition(Transition.CoverHiRightIdleTSNormalIdle, StateID.NormalIdle);

        CoverHiRShootIdleState coverHiRShootIdle = new CoverHiRShootIdleState(this.transform);
        coverHiRShootIdle.AddTransition(Transition.CoverHiRShootIdleTSCoverHiRShootBeforeDown, StateID.CoverHiRShootBeforeDown);
        coverHiRShootIdle.AddTransition(Transition.CoverHiRShootIdleTSCoverHiRightIdle, StateID.CoverHiRightIdle);
        coverHiRShootIdle.AddTransition(Transition.CoverHiRShootIdleTSFightIdle, StateID.FightIdle);

        CoverHiRShootBeforeDownState coverHiRShootBeforeDown = new CoverHiRShootBeforeDownState(this.transform);
        coverHiRShootBeforeDown.AddTransition(Transition.CoverHiRShootBeforeDownTSCoverHiRightIdle, StateID.CoverHiRightIdle);
        coverHiRShootBeforeDown.AddTransition(Transition.CoverHiRShootBeforeDownTSCoverHiRShootIdle, StateID.CoverHiRShootIdle);
        coverHiRShootBeforeDown.AddTransition(Transition.CoverHiRShootBeforeDownTSNormalIdle, StateID.NormalIdle);

        CoverHiLState coverHiL = new CoverHiLState(this.transform);
        coverHiL.AddTransition(Transition.CoverHiLTSCoverHiR, StateID.CoverHiR);
        coverHiL.AddTransition(Transition.CoverHiLTSNormalMove, StateID.NormalMove);
        coverHiL.AddTransition(Transition.CoverHiLTSCoverHiLeftIdle, StateID.CoverHiLeftIdle);
        coverHiL.AddTransition(Transition.CoverHiLTSNormalIdle, StateID.NormalIdle);

        CoverHiLeftIdleState coverHiLeftIdle = new CoverHiLeftIdleState(this.transform);
        coverHiLeftIdle.AddTransition(Transition.CoverHiLeftIdleTSCoverHiL, StateID.CoverHiL);
        coverHiLeftIdle.AddTransition(Transition.CoverHiLeftIdleTSRoll, StateID.Roll);
        coverHiLeftIdle.AddTransition(Transition.CoverHiLeftIdleTSCoverHiLShootIdle, StateID.CoverHiLShootIdle);
        coverHiLeftIdle.AddTransition(Transition.CoverHiLeftIdleTSNormalIdle, StateID.NormalIdle);

        CoverHiLShootIdleState coverHiLShootIdle = new CoverHiLShootIdleState(this.transform);
        coverHiLShootIdle.AddTransition(Transition.CoverHiLShootIdleTSCoverHiLShootBeforeDown, StateID.CoverHiLShootBeforeDown);
        coverHiLShootIdle.AddTransition(Transition.CoverHiLShootIdleTSCoverHiLeftIdle, StateID.CoverHiLeftIdle);
        coverHiLShootIdle.AddTransition(Transition.CoverHiRShootIdleTSFightIdle, StateID.FightIdle);


        CoverHiLShootBeforeDownState coverHiLShootBeforeDown = new CoverHiLShootBeforeDownState(this.transform);
        coverHiLShootBeforeDown.AddTransition(Transition.CoverHiLShootBeforeDownTSCoverHiLeftIdle, StateID.CoverHiLeftIdle);
        coverHiLShootBeforeDown.AddTransition(Transition.CoverHiLShootBeforeDownTSCoverHiLShootIdle, StateID.CoverHiLShootIdle);
        coverHiLShootBeforeDown.AddTransition(Transition.CoverHiLShootBeforeDownTSNormalIdle, StateID.NormalIdle);

        CoverRReloadState coverRReloadState = new CoverRReloadState(this.transform);
        coverRReloadState.AddTransition(Transition.CoverRReloadTSCoverR, StateID.CoverR);//

        CoverLReloadState coverLReloadState = new CoverLReloadState(this.transform);
        coverLReloadState.AddTransition(Transition.CoverLReloadTSCoverL, StateID.CoverL);//

        PickUpState pickUpState = new PickUpState(this.transform);
        pickUpState.AddTransition(Transition.PickUpTSNormalIdle, StateID.NormalIdle);//

        OpenDoorState openDoorState = new OpenDoorState(this.transform);
        openDoorState.AddTransition(Transition.OpenDoorTSNormalIdle, StateID.NormalIdle);//

        UsePadState usePadState = new UsePadState(this.transform);
        usePadState.AddTransition(Transition.UsePadTSNormalIdle, StateID.NormalIdle);//


        fsm = new FSMSystem();

        fsm.AddState(normalIdle);
        fsm.AddState(normalMove);
        fsm.AddState(roll);
        fsm.AddState(sprint);
        fsm.AddState(fightIdle);
        fsm.AddState(fightMove);
        fsm.AddState(coverR);
        fsm.AddState(coverL);
        fsm.AddState(coverJump);
        fsm.AddState(turnR);
        fsm.AddState(turnL);
        fsm.AddState(coverHiR);
        fsm.AddState(coverHiL);
        fsm.AddState(coverHiRightIdle);
        fsm.AddState(coverHiLeftIdle);
        fsm.AddState(coverHiRShootIdle);
        fsm.AddState(coverHiRShootBeforeDown);
        fsm.AddState(coverHiLShootIdle);
        fsm.AddState(coverHiLShootBeforeDown);
        fsm.AddState(coverLoRShootIdle);
        fsm.AddState(coverLoRShootBeforeDown);
        fsm.AddState(coverLoLShootIdle);
        fsm.AddState(coverLoLShootBeforeDown);
        fsm.AddState(coverRReloadState);
        fsm.AddState(coverLReloadState);
        fsm.AddState(pickUpState);
        fsm.AddState(openDoorState);
        fsm.AddState(usePadState);
    }
}
