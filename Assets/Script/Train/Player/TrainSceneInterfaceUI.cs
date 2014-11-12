using UnityEngine;
using System.Collections;

public class TrainSceneInterfaceUI : MonoBehaviour
{
    ////////PlayerCtrl/////////////
    private static GameObject playerCtrlComObj;
    protected static GameObject playerCtrlCom;
    ////////Scene/////////////
    private static GameObject sceneComObj;
    protected static GameObject sceneCom;
    /////////TrainSceneUI/////////////
    private static GameObject trainSceneUIObj;
    protected static GameObject trainSceneUI;
    /////////TrainSceneUI/////////////
    private static GameObject trainSceneCameraObj;
    protected static GameObject trainSceneCamera;
    //////////////EventUIs///////////////
    private static GameObject eventUIObj;
    protected static GameObject eventUI;
    private static GameObject evoUIObj;
    public static GameObject evoUI;
    //////////////GoUIs///////////////
    private static GameObject goUIObj;
    protected static GameObject goUI;
    protected static GameObject mapChoiceUI;
    protected static GameObject questChoiceUI;
    protected static GameObject questDataUI;
    protected static GameObject readyUI;

    //////////////PartyUIs///////////////
    private static GameObject partyUIObj;
    protected static GameObject partyUI;
    protected static GameObject editMemberListUI;
    protected static GameObject memberUI;
    protected static GameObject editMemberDataUI;
    protected static GameObject memberTitleUI;
    protected static GameObject memberDataUI;
    protected static GameObject memberSkillUI;
    protected static GameObject skillListUI;
    protected static GameObject formationUI;
    protected static GameObject changeMemberListUI;
    protected static GameObject changeMemberUI;
    protected static GameObject menuUI;
    protected static GameObject EnhanceMonsterUI;
    ///RoomCtrlUI///////////
    static private GameObject roomCtrlUIObj;
    static protected GameObject roomCtrlUI;
    ////////ResourceUI///////////
    private static GameObject resourceUIObj;
    protected static GameObject resourceUI;
    protected static UILabel goldLabel;
    protected static UILabel dimandLabel;
    protected static UILabel energyLabel;



    //////////EditUI///////////////////
    // Use this for initialization
    void Awake()
    {
        LoadingScene.ChangeSceneFunction += ChangeScene;
    }

    public static void ChangeScene()//切換場景
    {
        goUI.SetActive(true);
        partyUI.SetActive(false);
    }
    public static void StartSetUI()
    {
        //////////////////////////////////////////LoadPlayerCtrlCom////////////////////////////////////////
        playerCtrlComObj = Resources.Load<GameObject>("GameObject/RoomCom/PlayerCtrl");
        Quaternion playerCtrlComRot = Quaternion.Euler(0, 0, 0);
        playerCtrlCom = Instantiate(playerCtrlComObj, Vector3.zero, playerCtrlComRot) as GameObject;
        playerCtrlCom.name = "PlayerCtrl";
        //////////////////////////////////////////LoadTrainSceneUI////////////////////////////////////////
        trainSceneUIObj = Resources.Load<GameObject>("GameObject/RoomUI/UI");
        Vector3 trainSceneUIPos = new Vector3(5.450761f, 0, 0);
        Vector3 trainSceneUIScale = new Vector3(0.0015625f, 0.0015625f, 0.0015625f);
        Quaternion trainSceneUIRot = Quaternion.Euler(0, 0, 0);
        trainSceneUI = Instantiate(trainSceneUIObj, trainSceneUIPos, trainSceneUIRot) as GameObject;
        trainSceneUI.transform.localScale = trainSceneUIScale;
        trainSceneUI.transform.localPosition = trainSceneUIPos;
        trainSceneUI.transform.localRotation = trainSceneUIRot;
        trainSceneUI.name = "TrainSceneUI";
        //////////////////////////////////////////LoadEvolutionUI////////////////////////////////////////

        evoUIObj = Resources.Load<GameObject>("GameObject/RoomUI/Evolution");
        Vector3 evoUIPos = new Vector3(0, 90, 0);
        Vector3 evoUIScale = new Vector3(1, 1, 1);
        Quaternion evoUIRot = Quaternion.Euler(0, 0, 0);
        evoUI = Instantiate(evoUIObj, evoUIPos, evoUIRot) as GameObject;
        evoUI.transform.parent = GameObject.FindGameObjectWithTag("RoomInterfaceUI").transform;
        evoUI.transform.localScale = evoUIScale;
        evoUI.transform.localPosition = evoUIPos;
        evoUI.transform.localRotation = evoUIRot;
        evoUI.name = "EvolutionUI";
        evoUI.SetActive(false);

        ///////////////////////////////////////////////LoadEventUI////////////////////////////////////////
        eventUIObj = Resources.Load<GameObject>("GameObject/RoomUI/Event");
        Vector3 eventUIPos = Vector3.zero;
        Vector3 eventUIScale = new Vector3(1, 1, 1);
        Quaternion eventUIRot = Quaternion.Euler(0, 0, 0);
        eventUI = Instantiate(eventUIObj, eventUIPos, eventUIRot) as GameObject;
        eventUI.transform.parent = GameObject.FindGameObjectWithTag("RoomInterfaceUI").transform;
        eventUI.transform.localScale = eventUIScale;
        eventUI.transform.localPosition = eventUIPos;
        eventUI.transform.localRotation = eventUIRot;
        eventUI.name = "EventUI";
        /////////////////////////////////////LoadGoUI////////////////////////////////////////////////////
        goUIObj = Resources.Load<GameObject>("GameObject/RoomUI/Go");
        Vector3 goUIPos = Vector3.zero;
        Vector3 goUIScale = new Vector3(1, 1, 1);
        Quaternion goUIRot = Quaternion.Euler(0, 0, 0);
        goUI = Instantiate(goUIObj, goUIPos, goUIRot) as GameObject;
        goUI.transform.parent = GameObject.FindGameObjectWithTag("RoomInterfaceUI").transform;
        goUI.transform.localScale = goUIScale;
        goUI.transform.localPosition = goUIPos;
        goUI.transform.localRotation = goUIRot;
        goUI.name = "GoUI";
        goUI.SetActive(false);
        //Find MapChoice in GoUI's child
        mapChoiceUI = goUI.transform.FindChild("MapChoice").gameObject;
        //Find QuestChoice in GoUI's child
        questChoiceUI = goUI.transform.FindChild("QuestChoice").gameObject;
        //Find Ready in GoUI's child
        readyUI = goUI.transform.FindChild("Ready").gameObject;
        //Find Ready in GoUI's child
        questDataUI = goUI.transform.FindChild("QuestData").gameObject;




        /////////////////////////////////////LoadPartyUI//////////////////////////////////////////////
        partyUIObj = Resources.Load<GameObject>("GameObject/RoomUI/party");
        Vector3 partyUIPos = Vector3.zero;
        Vector3 partyUIScale = new Vector3(1, 1, 1);
        Quaternion partyUIRot = Quaternion.Euler(0, 0, 0);
        partyUI = Instantiate(partyUIObj, partyUIPos, partyUIRot) as GameObject;
        partyUI.transform.parent = GameObject.FindGameObjectWithTag("RoomInterfaceUI").transform;
        partyUI.transform.localScale = partyUIScale;
        partyUI.transform.localPosition = partyUIPos;
        partyUI.transform.localRotation = partyUIRot;
        partyUI.name = "PartyUI";
        partyUI.SetActive(false);
        //Find Menu in partyUI's child
        menuUI = partyUI.transform.FindChild("Menu").gameObject;
        //Find EnhanceUI
        EnhanceMonsterUI = partyUI.transform.FindChild("Enhance").gameObject;
        //Find EditMemberList in partyUI's child
        editMemberListUI = partyUI.transform.FindChild("EditMemberList").gameObject;
        //Find EditMemberList in partyUI's child
        //memberUI = editMemberListUI.transform.FindChild("member").gameObject;
        //Find EditMemberData in partyUI's child
        editMemberDataUI = partyUI.transform.FindChild("EditMemberData").gameObject;
        //Find MemberTitle in EditMemberDataUI's child
        memberTitleUI = editMemberDataUI.transform.FindChild("MemberTitle").gameObject;
        memberDataUI = editMemberDataUI.transform.FindChild("TalentBoard").gameObject;
        memberSkillUI = editMemberDataUI.transform.FindChild("SkillBoard").gameObject;
        //Find SkillLIst in partyUI's child
        skillListUI = partyUI.transform.FindChild("SkillList").gameObject;
        //Find Formation in partyUI's child
        formationUI = partyUI.transform.FindChild("Formation").gameObject;
        //Find ChangeMemberList in partyUI's child
        changeMemberListUI = partyUI.transform.FindChild("ChangeMemberList").gameObject;
        //Find ChangeMemberList in partyUI's child
        //changeMemberUI = changeMemberListUI.transform.FindChild("member").gameObject;
        /////////////////////////////////RoomCtrlUI/////////////////////////////////
        roomCtrlUIObj = Resources.Load<GameObject>("GameObject/RoomUI/BottomUI");
        Vector3 roomCtrlUIPos = Vector3.zero;
        Vector3 roomCtrlUIScale = new Vector3(1, 1, 1);
        Quaternion roomCtrlUIRot = Quaternion.Euler(0, 0, 0);
        roomCtrlUI = Instantiate(roomCtrlUIObj, roomCtrlUIPos, roomCtrlUIRot) as GameObject;
        roomCtrlUI.transform.parent = GameObject.FindGameObjectWithTag("CtrlUI").transform;
        roomCtrlUI.transform.localScale = roomCtrlUIScale;
        roomCtrlUI.transform.localPosition = roomCtrlUIPos;
        roomCtrlUI.transform.localRotation = roomCtrlUIRot;
        roomCtrlUI.transform.FindChild("go").GetComponent<GO>().StartSet();
        roomCtrlUI.transform.FindChild("party").GetComponent<Party>().StartSet();
        roomCtrlUI.name = "RommCtrlUI";
        ///////////////////////ResourceUI///////////////////////
        resourceUIObj = Resources.Load<GameObject>("GameObject/RoomUI/PlayerSource");
        Vector3 resourceUIPos = Vector3.zero;
        Vector3 resourceUIScale = new Vector3(1, 1, 1);
        Quaternion resourceUIRot = Quaternion.Euler(0, 0, 0);
        resourceUI = Instantiate(resourceUIObj, resourceUIPos, resourceUIRot) as GameObject;
        resourceUI.transform.parent = GameObject.FindGameObjectWithTag("ResourceUI").transform;
        resourceUI.transform.localScale = resourceUIScale;
        resourceUI.transform.localPosition = resourceUIPos;
        resourceUI.transform.localRotation = resourceUIRot;
        goldLabel = resourceUI.transform.FindChild("gold").GetComponent<UILabel>();
        dimandLabel = resourceUI.transform.FindChild("dimand").GetComponent<UILabel>();
        energyLabel = resourceUI.transform.FindChild("energy").GetComponent<UILabel>();
        resourceUI.name = "ResourceUI";
        ///////////////////////TrainSceneCamera///////////////////////
        trainSceneCameraObj = Resources.Load<GameObject>("GameObject/Room/SceneCamera");
        Vector3 trainSceneCameraPos = new Vector3(0, 0, -2);
        Vector3 trainSceneCameraScale = new Vector3(1, 1, 1);
        Quaternion trainSceneCameraRot = Quaternion.Euler(0, 0, 0);
        trainSceneCamera = Instantiate(trainSceneCameraObj, trainSceneCameraPos, trainSceneCameraRot) as GameObject;
        trainSceneCamera.transform.localScale = trainSceneCameraScale;
        trainSceneCamera.transform.localPosition = trainSceneCameraPos;
        trainSceneCamera.transform.localRotation = trainSceneCameraRot;
        trainSceneCamera.name = "TrainSceneCamera";


        //初始化設置UI
        editMemberListUI.GetComponent<EditMemberList>().StartSetting();
        editMemberDataUI.GetComponent<EditMemberData>().StartSetting();
        skillListUI.GetComponent<SkillList>().StartSetting();
        formationUI.GetComponent<Formation>().StartSetting();
        changeMemberListUI.GetComponent<ChangeMemberList>().StartSetting();
        mapChoiceUI.GetComponent<MapUI>().StartSetting();
        questChoiceUI.GetComponent<QuestUI>().StartSetting();
        readyUI.GetComponent<ReadyUI>().StartSetting();
        questDataUI.GetComponent<QuestData>().StartSetting();
        EnhanceUI.StartSetting();//強化介面初始設置
    }
}
