using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;


public enum Transition
{
    NullTransition = 0,
    NormalIdleTSNormalMove, //
    NormalIdleTSFightIdle,//
    NormalIdleTSTurnR,//
    NormalIdleTSTurnL,//
    NormalIdleTSSprint,//
    NormalIdleTSCoverR,//
    NormalIdleTSCoverL,//
    NormalIdleTSCoverHiR,//
    NormalIdleTSCoverHiL,//
    NormalIdleTSHeavyGunStart,//
    NormalIdleTSPickUp,//
    NormalIdleTSOpenDoor,
    NormalIdleTSUsePad,
    NormalIdleTSPlotMove,
    NormalIdleTSPutGun,
    NormalIdleTSRoll,
    TurnRBeforeDownTSNormalIdle,//
    TurnRBeforeDownTSFightIdle,//
    TurnRBeforeDownTSTurnR,//
    TurnRBeforeDownTSTurnL,//
    TurnRBeforeDownTSNormalMove,//
    TurnLBeforeDownTSNormalIdle,//
    TurnLBeforeDownTSFightIdle,//
    TurnLBeforeDownTSTurnR,//
    TurnLBeforeDownTSTurnL,//
    TurnLBeforeDownTSNormalMove,//
    FightIdleTSFightMove,//
    FightIdleTSNormalMove,//
    FightIdleTSTurnR,//
    FightIdleTSTurnL,//
    FightIdleTSNormalIdle,//
    FightIdleTSRoll,
    NormalMoveTSFightMove,//
    NormalMoveTSNormalIdle,//
    NormalMoveTSSprint,//
    NormalMoveTSRoll,//
    NormalMoveTSCoverR,
    NormalMoveTSCoverL,
    NormalMoveTSCoverHiR,
    NormalMoveTSCoverHiL,
    NormalMoveTSOpenDoor,
    NormalMoveTSUsePad,
    FightMoveTSFightIdle,//
    FightMoveTSNormalMove,//
    FightMoveTSRoll,
    SprintTSNormalIdle,//
    SprintTSNormalMove,//
    SprintTSCoverR,
    SprintTSCoverL,
    SprintTSRoll,
    SprintTSCoverHiL,
    SprintTSCoverHiR,
    SprintTSOpenDoor,
    SprintTSUsePad,
    SprintTSFightIdle,
    CoverRTSCoverL,//
    CoverRTSRoll,//
    CoverRTSNormalMove,//
    CoverRTSNormalIdle,//
    CoverRTSCoverJump,//
    CoverRTSCoverLoRShootIdle,//
    CoverRTSCoverRReload,//   
    CoverLoRShootIdleTSCoverLoRShootBeforeDown,//
    CoverLoRShootIdleTSFightIdle,//
    CoverLoRShootIdleTSCoverR,//
    CoverLoRShootBeforeDownTSCoverLoRShootIdle,//
    CoverLoRShootBeforeDownTSNormalIdle,//
    CoverLoRShootBeforeDownTSCoverR,//
    CoverLTSCoverR,//
    CoverLTSRoll,//
    CoverLTSNormalMove,//
    CoverLTSNormalIdle,//
    CoverLTSCoverJump,//
    CoverLTSCoverLoLShootIdle,//
    CoverLTSCoverLReload,//   
    CoverLoLShootIdleTSCoverLoLShootBeforeDown,//
    CoverLoLShootIdleTSFightIdle,//
    CoverLoLShootIdleTSCoverL,//
    CoverLoLShootBeforeDownTSCoverLoLShootIdle,//
    CoverLoLShootBeforeDownTSNormalIdle,//
    CoverLoLShootBeforeDownTSCoverL,//
    CoverHiRTSCoverHiL,//
    CoverHiRTSNormalMove,//
    CoverHiRTSCoverHiRightIdle,//
    CoverHiRTSNormalIdle,
    CoverHiRightIdleTSNormalIdle,
    CoverHiRightIdleTSCoverHiR,//
    CoverHiRightIdleTSRoll,//
    CoverHiRightIdleTSCoverHiRShootIdle,//
    CoverHiRShootIdleTSFightIdle,
    CoverHiRShootIdleTSCoverHiRightIdle,//
    CoverHiRShootIdleTSCoverHiRShootBeforeDown,//
    CoverHiRShootBeforeDownTSCoverHiRShootIdle,//
    CoverHiRShootBeforeDownTSCoverHiRightIdle,//
    CoverHiRShootBeforeDownTSNormalIdle,
    CoverHiLTSNormalIdle,
    CoverHiLTSCoverHiR,//
    CoverHiLTSNormalMove,//
    CoverHiLTSCoverHiLeftIdle,//
    CoverHiLeftIdleTSNormalIdle,
    CoverHiLeftIdleTSCoverHiL,//
    CoverHiLeftIdleTSRoll,//
    CoverHiLeftIdleTSCoverHiLShootIdle,//
    CoverHiLShootIdleTSFightIdle,
    CoverHiLShootIdleTSCoverHiLeftIdle,//
    CoverHiLShootIdleTSCoverHiLShootBeforeDown,//
    CoverHiLShootBeforeDownTSNormalIdle,
    CoverHiLShootBeforeDownTSCoverHiLShootIdle,//
    CoverHiLShootBeforeDownTSCoverHiLeftIdle,//
    CoverJumpTSNormalIdle,//
    CoverJumpTSNormalMove,//
    RollTSNormalIdle,//
    RollTSNormalMove,//
    TurnRTSFightIdle, //
    TurnRTSTurnRBeforeDown, //
    TurnRTSTurnL,
    TurnRTSNormalIdle,
    TurnRTSNormalMove,
    TurnLTSFightIdle,//
    TurnLTSTurnLBeforeDown,//
    TurnLTSTurnR,
    TurnLTSNormalIdle,
    TurnLTSNormalMove,
    HeavyGunStartTSHeavyGunIdle,//
    HeavyGunIdleTSHeavyGunShoot,//
    HeavyGunIdleTSHeavyGunEnd,//
    HeavyGunShootTSHeavyGunIdle,//
    HeavyGunShootTSHeavyGunEnd,//
    HeavyGunEndTSNormalIdle,//
    CoverRReloadTSCoverR,//
    CoverLReloadTSCoverL,//
    PickUpTSNormalIdle,//
    OpenDoorTSNormalIdle,
    UsePadTSNormalIdle,
    PlotMoveTSNormalIdle,
    PutGunTSPlotMove,
    PlotMoveTSPutGun,
    PutGunTSNormalIdle
}

public enum StateID
{
    NullStateID = 0,
    NormalIdle,//
    NormalMove,//
    Roll,//
    Sprint,//
    FightIdle,//
    FightMove,//
    CoverR,//    
    CoverL,//    
    CoverJump,//
    TurnR,//
    TurnL,//
    TurnRBeforeDown,//
    TurnLBeforeDown,//
    CoverHiR,//
    CoverHiL,//
    CoverHiRightIdle,//
    CoverHiLeftIdle,//
    CoverHiRShootIdle,//
    CoverHiRShootBeforeDown,//
    CoverHiLShootIdle,//
    CoverHiLShootBeforeDown,//
    CoverLoRShootIdle,//
    CoverLoRShootBeforeDown,//
    CoverLoLShootIdle,//
    CoverLoLShootBeforeDown,//
    HeavyGunStart,//
    HeavyGunIdle,//
    HeavyGunShoot,//
    HeavyGunEnd,//
    CoverRReload,//
    CoverLReload,//
    PickUp,//
    OpenDoor,
    UsePad,
    PlotMove,
    PutGun
}

public abstract class FSMState
{
    public Animator animator;
    public AimIK ik;
    public CapsuleCollider capsuleCollider;
    public CharacterController characterController;
    public Transform player;
    public PlayerFSM PlayerFsm;
    public PlayerController playerController;
    public PlayerRotateWithCamera playerRotateWithCamera;
    public PlayerRayCast playerRayCast;
    public PlayerAudioEffect PlayerAudioEffect;


    public FSMState(Transform player)
    {
        this.player = player;
        animator = player.GetComponent<Animator>();
        ik = player.GetComponent<AimIK>();
        playerController = player.GetComponent<PlayerController>();
        characterController = player.GetComponent<CharacterController>();
        capsuleCollider = player.GetComponent<CapsuleCollider>();
        PlayerFsm = player.GetComponent<PlayerFSM>();
        playerRotateWithCamera = player.GetComponent<PlayerRotateWithCamera>();
        playerRayCast = player.GetComponent<PlayerRayCast>();
        PlayerAudioEffect = player.GetComponent<PlayerAudioEffect>();
    }

    public Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
    public Dictionary<Transition, float> dic = new Dictionary<Transition, float>();

    protected StateID stateID;
    public StateID ID
    {
        get { return stateID; }
    }

    public void AddTransition(Transition trans, StateID id, float delaytime = 0.02f)
    {
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed for a real transition");
            return;
        }

        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
            return;
        }

        if (map.ContainsKey(trans))
        {
            Debug.LogError("FSMState ERROR: State " + stateID.ToString() + " already has transition " + trans.ToString() +
                        "Impossible to assign to another state");
            return;
        }

        map.Add(trans, id);
        dic.Add(trans, delaytime);
    }

    public StateID GetOutputState(Transition trans)
    {
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }
        return StateID.NullStateID;
    }


    public abstract void DoBeforeEnter();
    public abstract void DoBeforeLeaving();
    public abstract void BaseLayerUpdate();
    public abstract void OtherLayerUpdate();
    public abstract void StateUpdate();
}
