using System.Collections;
using System.Collections.Generic;
using DLFramework;
using RootMotion.FinalIK;
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

        fsm.CurrentState.OtherLayerUpdate();
        fsm.CurrentState.StateUpdate();
        if (fsm.isTransition)
            return;
        fsm.CurrentState.BaseLayerUpdate();
    }

    public void SetTransition(Transition t)
    {
        fsm.PerformTransition(t);
    }

    private void MakeFSM()
    {

        NormalIdleState normalIdle = new NormalIdleState(m_animator, this.transform, m_AimIk);
        normalIdle.AddTransition(Transition.NormalIdleTSTurnR, StateID.TurnR, 0.25f);//
        normalIdle.AddTransition(Transition.NormalIdleTSTurnL, StateID.TurnL, 0.25f);//
        normalIdle.AddTransition(Transition.NormalIdleTSNormalMove, StateID.NormalMove);//
        normalIdle.AddTransition(Transition.NormalIdleTSFightIdle, StateID.FightIdle, 0.1f);//
        normalIdle.AddTransition(Transition.NormalIdleTSSprint, StateID.Sprint, 0.1f);//
        normalIdle.AddTransition(Transition.NormalIdleTSCoverR, StateID.CoverR, 0.1f);//
        normalIdle.AddTransition(Transition.NormalIdleTSCoverL, StateID.CoverL, 0.1f);//
        normalIdle.AddTransition(Transition.NormalIdleTSCoverHiR, StateID.CoverHiR, 0.1f);
        normalIdle.AddTransition(Transition.NormalIdleTSCoverHiL, StateID.CoverHiL, 0.1f);
        normalIdle.AddTransition(Transition.NormalIdleTSPickUp, StateID.PickUp, 0.25f);//
        normalIdle.AddTransition(Transition.NormalIdleTSOpenDoor, StateID.OpenDoor, 0.1f);//
        normalIdle.AddTransition(Transition.NormalIdleTSUsePad, StateID.UsePad, 0.1f);//
        //normalIdle.AddTransition(Transition.NormalIdleTSPlotMove, StateID.PlotMove);

        TurnRState turnR = new TurnRState(m_animator, this.transform);
        //turnR.AddTransition(Transition.TurnRTSFightIdle, StateID.FightIdle);
        turnR.AddTransition(Transition.TurnRTSTurnRBeforeDown, StateID.TurnRBeforeDown, 0.1f);

        TurnLState turnL = new TurnLState(m_animator, this.transform);
        //turnL.AddTransition(Transition.TurnLTSFightIdle, StateID.FightIdle);
        turnL.AddTransition(Transition.TurnLTSTurnLBeforeDown, StateID.TurnLBeforeDown, 0.1f);

        TurnRBeforeDownState turnRBeforeDown = new TurnRBeforeDownState(m_animator, this.transform);
        turnRBeforeDown.AddTransition(Transition.TurnRBeforeDownTSNormalIdle, StateID.NormalIdle, 0.25f);
        turnRBeforeDown.AddTransition(Transition.TurnRBeforeDownTSTurnR, StateID.TurnR, 0.25f);
        turnRBeforeDown.AddTransition(Transition.TurnRBeforeDownTSTurnL, StateID.TurnL, 0.25f);
        turnRBeforeDown.AddTransition(Transition.TurnRBeforeDownTSNormalMove, StateID.NormalMove, 0.1f);
        turnRBeforeDown.AddTransition(Transition.TurnRBeforeDownTSFightIdle, StateID.FightIdle, 0.1f);

        TurnLBeforeDownState turnLBeforeDown = new TurnLBeforeDownState(m_animator, this.transform);
        turnLBeforeDown.AddTransition(Transition.TurnLBeforeDownTSNormalIdle, StateID.NormalIdle, 0.25f);
        turnLBeforeDown.AddTransition(Transition.TurnLBeforeDownTSTurnL, StateID.TurnL, 0.25f);
        turnLBeforeDown.AddTransition(Transition.TurnLBeforeDownTSTurnR, StateID.TurnR, 0.25f);
        turnLBeforeDown.AddTransition(Transition.TurnLBeforeDownTSNormalMove, StateID.NormalMove, 0.1f);
        turnLBeforeDown.AddTransition(Transition.TurnLBeforeDownTSFightIdle, StateID.FightIdle, 0.1f);

        FightIdleState fightIdle = new FightIdleState(m_animator, this.transform, m_AimIk);
        fightIdle.AddTransition(Transition.FightIdleTSFightMove, StateID.FightMove);//
        fightIdle.AddTransition(Transition.FightIdleTSNormalMove, StateID.NormalMove);//
        fightIdle.AddTransition(Transition.FightIdleTSTurnR, StateID.TurnR, 0.25f);//
        fightIdle.AddTransition(Transition.FightIdleTSTurnL, StateID.TurnL, 0.25f);//
        fightIdle.AddTransition(Transition.FightIdleTSNormalIdle, StateID.NormalIdle);//
        //fightIdle.AddTransition(Transition.FightIdleTSRoll, StateID.Roll);

        NormalMoveState normalMove = new NormalMoveState(m_animator, this.transform, m_AimIk);
        normalMove.AddTransition(Transition.NormalMoveTSFightMove, StateID.FightMove, 0.1f);//
        normalMove.AddTransition(Transition.NormalMoveTSNormalIdle, StateID.NormalIdle);//
        normalMove.AddTransition(Transition.NormalMoveTSSprint, StateID.Sprint, 0.25f);//
        normalMove.AddTransition(Transition.NormalMoveTSRoll, StateID.Roll, 0.1f);//
        normalMove.AddTransition(Transition.NormalMoveTSCoverR, StateID.CoverR, 0.1f);//
        normalMove.AddTransition(Transition.NormalMoveTSCoverL, StateID.CoverL, 0.1f);//
        normalMove.AddTransition(Transition.NormalMoveTSCoverHiR, StateID.CoverHiR, 0.1f);
        normalMove.AddTransition(Transition.NormalMoveTSCoverHiL, StateID.CoverHiL, 0.1f);
        normalMove.AddTransition(Transition.NormalMoveTSOpenDoor, StateID.OpenDoor, 0.1f);//
        normalMove.AddTransition(Transition.NormalMoveTSUsePad, StateID.UsePad, 0.1f);//

        FightMoveState fightMove = new FightMoveState(m_animator, this.transform);
        fightMove.AddTransition(Transition.FightMoveTSFightIdle, StateID.FightIdle, 0.1f);//
        fightMove.AddTransition(Transition.FightMoveTSNormalMove, StateID.NormalMove);//
        //fightMove.AddTransition(Transition.FightMoveTSRoll, StateID.Roll);

        RollState roll = new RollState(m_animator, this.transform, m_AimIk);
        roll.AddTransition(Transition.RollTSNormalIdle, StateID.NormalIdle, 0.1f);//
        roll.AddTransition(Transition.RollTSNormalMove, StateID.NormalMove, 0.1f);//

        SprintState sprint = new SprintState(m_animator, this.transform, m_AimIk);
        sprint.AddTransition(Transition.SprintTSNormalIdle, StateID.NormalIdle, 0.25f);//
        sprint.AddTransition(Transition.SprintTSNormalMove, StateID.NormalMove, 0.1f);//
        sprint.AddTransition(Transition.SprintTSCoverR, StateID.CoverR, 0.1f);//
        sprint.AddTransition(Transition.SprintTSCoverL, StateID.CoverL, 0.1f);//
        sprint.AddTransition(Transition.SprintTSRoll, StateID.Roll, 0.1f);//
        sprint.AddTransition(Transition.SprintTSCoverHiR, StateID.CoverHiR, 0.1f);
        sprint.AddTransition(Transition.SprintTSCoverHiL, StateID.CoverHiL, 0.1f);
        sprint.AddTransition(Transition.SprintTSOpenDoor, StateID.OpenDoor, 0.1f);//
        sprint.AddTransition(Transition.SprintTSUsePad, StateID.UsePad, 0.1f);//
        sprint.AddTransition(Transition.SprintTSFightIdle, StateID.FightIdle, 0.1f);

        CoverJumpState coverJump = new CoverJumpState(m_animator, this.transform, m_CapsuleCollider, m_CharacterController);
        coverJump.AddTransition(Transition.CoverJumpTSNormalIdle, StateID.NormalIdle);//

        CoverRState coverR = new CoverRState(m_animator, this.transform, m_AimIk, m_CapsuleCollider, m_CharacterController);
        coverR.AddTransition(Transition.CoverRTSCoverL, StateID.CoverL);//
        coverR.AddTransition(Transition.CoverRTSRoll, StateID.Roll, 0.25f);//
        coverR.AddTransition(Transition.CoverRTSNormalMove, StateID.NormalMove, 0.1f);//
        coverR.AddTransition(Transition.CoverRTSCoverJump, StateID.CoverJump, 0.25f);//
        coverR.AddTransition(Transition.CoverRTSCoverLoRShootIdle, StateID.CoverLoRShootIdle, 0.1f);//
        coverR.AddTransition(Transition.CoverRTSCoverRReload, StateID.CoverRReload, 0.25f);//
        coverR.AddTransition(Transition.CoverRTSNormalIdle, StateID.NormalIdle, 0.25f);//

        CoverLoRShootIdleState coverLoRShootIdle = new CoverLoRShootIdleState(m_animator, this.transform, m_AimIk);
        coverLoRShootIdle.AddTransition(Transition.CoverLoRShootIdleTSCoverLoRShootBeforeDown, StateID.CoverLoRShootBeforeDown, 0.1f);//
        coverLoRShootIdle.AddTransition(Transition.CoverLoRShootIdleTSFightIdle, StateID.FightIdle, 0.25f);//
        coverLoRShootIdle.AddTransition(Transition.CoverLoRShootIdleTSCoverR, StateID.CoverR, 0.25f);//

        CoverLoRShootBeforeDownState coverLoRShootBeforeDown = new CoverLoRShootBeforeDownState(m_animator, this.transform, m_AimIk);
        coverLoRShootBeforeDown.AddTransition(Transition.CoverLoRShootBeforeDownTSCoverR, StateID.CoverR, 0.25f);//
        coverLoRShootBeforeDown.AddTransition(Transition.CoverLoRShootBeforeDownTSCoverLoRShootIdle, StateID.CoverLoRShootIdle, 0.1f);//
        coverLoRShootBeforeDown.AddTransition(Transition.CoverLoRShootBeforeDownTSNormalIdle, StateID.NormalIdle, 0.25f);//

        CoverLState coverL = new CoverLState(m_animator, this.transform, m_AimIk, m_CapsuleCollider, m_CharacterController);
        coverL.AddTransition(Transition.CoverLTSCoverR, StateID.CoverR);//
        coverL.AddTransition(Transition.CoverLTSRoll, StateID.Roll, 0.25f);//
        coverL.AddTransition(Transition.CoverLTSNormalMove, StateID.NormalMove, 0.1f);//
        coverL.AddTransition(Transition.CoverLTSCoverJump, StateID.CoverJump, 0.25f);//
        coverL.AddTransition(Transition.CoverLTSCoverLoLShootIdle, StateID.CoverLoLShootIdle, 0.1f);//
        coverL.AddTransition(Transition.CoverLTSCoverLReload, StateID.CoverLReload, 0.25f);//
        coverL.AddTransition(Transition.CoverLTSNormalIdle, StateID.NormalIdle, 0.25f);//

        CoverLoLShootIdleState coverLoLShootIdle = new CoverLoLShootIdleState(m_animator, this.transform, m_AimIk);
        coverLoLShootIdle.AddTransition(Transition.CoverLoLShootIdleTSCoverLoLShootBeforeDown, StateID.CoverLoLShootBeforeDown, 0.1f);//
        coverLoLShootIdle.AddTransition(Transition.CoverLoLShootIdleTSFightIdle, StateID.FightIdle, 0.25f);//
        coverLoLShootIdle.AddTransition(Transition.CoverLoLShootIdleTSCoverL, StateID.CoverL, 0.25f);//

        CoverLoLShootBeforeDownState coverLoLShootBeforeDown = new CoverLoLShootBeforeDownState(m_animator, this.transform, m_AimIk);
        coverLoLShootBeforeDown.AddTransition(Transition.CoverLoLShootBeforeDownTSCoverL, StateID.CoverL, 0.25f);//
        coverLoLShootBeforeDown.AddTransition(Transition.CoverLoLShootBeforeDownTSCoverLoLShootIdle, StateID.CoverLoLShootIdle, 0.1f);//
        coverLoLShootBeforeDown.AddTransition(Transition.CoverLoLShootBeforeDownTSNormalIdle, StateID.NormalIdle, 0.25f);//

        CoverHiRState coverHiR = new CoverHiRState(m_animator, this.transform, m_AimIk);
        coverHiR.AddTransition(Transition.CoverHiRTSCoverHiL, StateID.CoverHiL);
        coverHiR.AddTransition(Transition.CoverHiRTSNormalMove, StateID.NormalMove, 0.1f);
        coverHiR.AddTransition(Transition.CoverHiRTSCoverHiRightIdle, StateID.CoverHiRightIdle, 0.1f);
        coverHiR.AddTransition(Transition.CoverHiRTSNormalIdle, StateID.NormalIdle, 0.25f);

        CoverHiRightIdleState coverHiRightIdle = new CoverHiRightIdleState(m_animator, this.transform, m_AimIk);
        coverHiRightIdle.AddTransition(Transition.CoverHiRightIdleTSCoverHiR, StateID.CoverHiR, 0.1f);
        coverHiRightIdle.AddTransition(Transition.CoverHiRightIdleTSRoll, StateID.Roll, 0.25f);
        coverHiRightIdle.AddTransition(Transition.CoverHiRightIdleTSCoverHiRShootIdle, StateID.CoverHiRShootIdle, 0.1f);
        coverHiRightIdle.AddTransition(Transition.CoverHiRightIdleTSNormalIdle, StateID.NormalIdle, 0.25f);

        CoverHiRShootIdleState coverHiRShootIdle = new CoverHiRShootIdleState(m_animator, this.transform, m_AimIk);
        coverHiRShootIdle.AddTransition(Transition.CoverHiRShootIdleTSCoverHiRShootBeforeDown, StateID.CoverHiRShootBeforeDown);
        coverHiRShootIdle.AddTransition(Transition.CoverHiRShootIdleTSCoverHiRightIdle, StateID.CoverHiRightIdle, 0.1f);
        coverHiRShootIdle.AddTransition(Transition.CoverHiRShootIdleTSFightIdle, StateID.FightIdle, 0.1f);

        CoverHiRShootBeforeDownState coverHiRShootBeforeDown = new CoverHiRShootBeforeDownState(m_animator, this.transform, m_AimIk);
        coverHiRShootBeforeDown.AddTransition(Transition.CoverHiRShootBeforeDownTSCoverHiRightIdle, StateID.CoverHiRightIdle);
        coverHiRShootBeforeDown.AddTransition(Transition.CoverHiRShootBeforeDownTSCoverHiRShootIdle, StateID.CoverHiRShootIdle, 0.1f);
        coverHiRShootBeforeDown.AddTransition(Transition.CoverHiRShootBeforeDownTSNormalIdle, StateID.NormalIdle, 0.25f);

        CoverHiLState coverHiL = new CoverHiLState(m_animator, this.transform, m_AimIk);
        coverHiL.AddTransition(Transition.CoverHiLTSCoverHiR, StateID.CoverHiR);
        coverHiL.AddTransition(Transition.CoverHiLTSNormalMove, StateID.NormalMove, 0.1f);
        coverHiL.AddTransition(Transition.CoverHiLTSCoverHiLeftIdle, StateID.CoverHiLeftIdle, 0.1f);
        coverHiL.AddTransition(Transition.CoverHiLTSNormalIdle, StateID.NormalIdle, 0.25f);

        CoverHiLeftIdleState coverHiLeftIdle = new CoverHiLeftIdleState(m_animator, this.transform, m_AimIk);
        coverHiLeftIdle.AddTransition(Transition.CoverHiLeftIdleTSCoverHiL, StateID.CoverHiL, 0.1f);
        coverHiLeftIdle.AddTransition(Transition.CoverHiLeftIdleTSRoll, StateID.Roll, 0.25f);
        coverHiLeftIdle.AddTransition(Transition.CoverHiLeftIdleTSCoverHiLShootIdle, StateID.CoverHiLShootIdle, 0.1f);
        coverHiLeftIdle.AddTransition(Transition.CoverHiLeftIdleTSNormalIdle, StateID.NormalIdle, 0.25f);

        CoverHiLShootIdleState coverHiLShootIdle = new CoverHiLShootIdleState(m_animator, this.transform, m_AimIk);
        coverHiLShootIdle.AddTransition(Transition.CoverHiLShootIdleTSCoverHiLShootBeforeDown, StateID.CoverHiLShootBeforeDown);
        coverHiLShootIdle.AddTransition(Transition.CoverHiLShootIdleTSCoverHiLeftIdle, StateID.CoverHiLeftIdle, 0.1f);
        coverHiLShootIdle.AddTransition(Transition.CoverHiRShootIdleTSFightIdle, StateID.FightIdle, 0.1f);


        CoverHiLShootBeforeDownState coverHiLShootBeforeDown = new CoverHiLShootBeforeDownState(m_animator, this.transform, m_AimIk);
        coverHiLShootBeforeDown.AddTransition(Transition.CoverHiLShootBeforeDownTSCoverHiLeftIdle, StateID.CoverHiLeftIdle);
        coverHiLShootBeforeDown.AddTransition(Transition.CoverHiLShootBeforeDownTSCoverHiLShootIdle, StateID.CoverHiLShootIdle, 0.1f);
        coverHiLShootBeforeDown.AddTransition(Transition.CoverHiLShootBeforeDownTSNormalIdle, StateID.NormalIdle, 0.25f);

        CoverRReloadState coverRReloadState = new CoverRReloadState(m_animator, this.transform);
        coverRReloadState.AddTransition(Transition.CoverRReloadTSCoverR, StateID.CoverR, 0.25f);//

        CoverLReloadState coverLReloadState = new CoverLReloadState(m_animator, this.transform);
        coverLReloadState.AddTransition(Transition.CoverLReloadTSCoverL, StateID.CoverL, 0.25f);//

        PickUpState pickUpState = new PickUpState(m_animator, this.transform);
        pickUpState.AddTransition(Transition.PickUpTSNormalIdle, StateID.NormalIdle, 0.25f);//

        OpenDoorState openDoorState = new OpenDoorState(m_animator, this.transform);
        openDoorState.AddTransition(Transition.OpenDoorTSNormalIdle, StateID.NormalIdle, 0.25f);//

        UsePadState usePadState = new UsePadState(m_animator, this.transform);
        usePadState.AddTransition(Transition.UsePadTSNormalIdle, StateID.NormalIdle, 0.25f);//

        PlotMoveState plotMoveState = new PlotMoveState(m_animator, this.transform);
        plotMoveState.AddTransition(Transition.PlotMoveTSNormalIdle, StateID.NormalIdle);
        plotMoveState.AddTransition(Transition.PlotMoveTSPutGun, StateID.PutGun);


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
        fsm.AddState(turnRBeforeDown);
        fsm.AddState(turnLBeforeDown);
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
        fsm.AddState(plotMoveState);
    }
}
