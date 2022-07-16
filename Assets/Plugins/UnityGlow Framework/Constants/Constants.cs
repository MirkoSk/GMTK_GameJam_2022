using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    #region Tags and Layers
    public static readonly string TAG_PLAYER = "Player";
    public static readonly string TAG_PLAYER_HEAD = "PlayerHead";
    public static readonly string TAG_PlAYER_LEFTHANDRENDERER = "LeftHandRenderer";
    public static readonly string TAG_PlAYER_RIGHTHANDRENDERER = "RightHandRenderer";
    public static readonly string TAG_PlAYER_LEFTHANDCONTROLLER = "LeftHandController";
    public static readonly string TAG_PlAYER_RIGHTHANDCONTROLLER = "RightHandController";
    public static readonly string TAG_VIVETRACKER_GRABBABLE = "ViveTrackerGrabbable";
    public static readonly string TAG_VRSETUP = "VRSetup";
    public static readonly string TAG_DONTDESTROYONLOAD = "DontDestroyOnLoad";
    public static readonly string TAG_GROUND = "Ground";
    public static readonly string LAYER_DEFAULT = "Default";
    public static readonly string LAYER_GROUND = "Ground";
    public static readonly string LAYER_DOORHINGE = "DoorHinge";
    #endregion



    #region AnimatorTrigger
    public static readonly string ANIMATOR_TRIGGER_ELEVATOR_JOURNEY = "doJourney";
    public static readonly string ANIMATOR_TRIGGER_ELEVATOR_CRASH = "doCrash";
    public static readonly string ANIMATOR_TRIGGER_ELEVATOR_IDLE = "doIdle";
    public static readonly string ANIMATOR_TRIGGER_BOAT_JOURNEY = "isBoatJourney";
    public static readonly string ANIMATOR_TRIGGER_LIFT_UPWARDS = "DoLiftUpwards"; 
    #endregion



    #region Times
    public static readonly float TIME_WAITFORPLAYERSINTRIGGER = 3f;
    public static readonly float TIME_DELAY_GUIDEMONOLOG = 3f;
    public static readonly float TIME_DELAY_BETWEEN_PARAGRAPHS = 0.5f;
    public static readonly float TIME_COUNTDOWN_TELEFONAT = 15f;
    public static readonly float TIME_WAITFOROWNERSHIP = 0.25f;
    public static readonly float TIME_SCREENGOESBLACK = 2f;
    public static readonly float TIME_WAITFORSETSTARTPOSITION = 3f;
    public static readonly float TIME_STARTGAMEDELAY = TIME_WAITFORSETSTARTPOSITION + 2f;
    public static readonly float TIME_COUNTDOWNMESSRAUM = 420;
    //public static readonly float TIME_COUNTDOWN_STROMRAETSEL = 240;
    public static readonly float TIME_COUNTDOWN_STROMRAETSEL = 5;
    #endregion



    #region names
    public static readonly string NAME_STARTPOSITION = "StartPosition";
    public static readonly string NAME_AUTOHANDHEAD = "Head Follower";
    public static readonly string NAME_SCENE_STARTUP = "00 StartUpScene";
    public const string NAME_SCENE_KAPITEL1 = "01 Kapitel In der Talsperre";
    public const string NAME_SCENE_KAPITEL2 = "02 Kapitel Das Höhlensystem";
    public static readonly string NAME_LOCALPLAYER = "local Client";
    public static readonly string NAME_REMOTEPLAYER = "remote Client";
    #endregion



    #region Audio
    // Exposed Parameters in Mixers
    public static readonly string MIXER_VOICE_VOLUME = "VoiceVolume";
    public static readonly string MIXER_SFX_VOLUME = "SFXVolume";
    public static readonly string MIXER_MUSIC_VOLUME = "MusicVolume";
    #endregion



    #region Paths
    public static readonly string pathToNetWorkManager = "Assets/Prefabs/SetupScene/NetworkManager.prefab";
    public static readonly string pathToNetWorkStartupManager = "Assets/Prefabs/SetupScene/NetworkStartupManager.prefab";
    public static readonly string pathToXRInteractionManager = "Assets/Prefabs/SetupScene/XR Interaction Manager.prefab";
    public static readonly string pathToNetworkVrSetup = "Assets/Prefabs/SetupScene/Network VR Setup.prefab";
    public static readonly string pathToEnvironment = "Assets/Prefabs/Environment.prefab";
    public static readonly string pathToGameManager = "Assets/Prefabs/SetupScene/GameManager.prefab";
    #endregion

}
