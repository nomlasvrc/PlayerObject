
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Nomlas.PlayerObject
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class PlayerObjectManager : UdonSharpBehaviour
    {
        [SerializeField] PlayerObjectBase playerObjectBase;
        public PlayerObject localPlayerObject { get; private set; }
        public bool isConnected { get; private set; }
        public bool isReady { get; private set; }
        private bool isBaseConnected;
        private bool isPlayerRestored;

        void Start()
        {
            if (!Utilities.IsValid(playerObjectBase))
            {
                Debug.LogError("Managerにスクリプトが指定されていません!");
            }
            isBaseConnected = playerObjectBase.SetManager(this);
            if (isConnected) //Manager <=> Objectが接続されており、OnConnected()が保留状態
            {
                playerObjectBase.OnConnected();
            }
            if (isConnected && isPlayerRestored) //Restoreが完了しManager <=> Objectが接続されている
            {
                playerObjectBase.OnReady();
            }
        }

        internal void OnDataUpdated(VRCPlayerApi player)
        {
            playerObjectBase.OnDataUpdated(player);
        }

        internal void RequestPersistence()
        {
            if (!isConnected) return;
            localPlayerObject.RequestPersistence();
        }

        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (!player.isLocal) return;
            isPlayerRestored = true;
            if (isConnected && isBaseConnected)
            {
                playerObjectBase.OnReady();
            }
        }

        internal bool ConnectToManager(VRCPlayerApi player, PlayerObject playerObject)
        {
            if (!player.isLocal) return false;
            localPlayerObject = playerObject;
            if (Utilities.IsValid(localPlayerObject))
            {
                isConnected = true;
                if (isBaseConnected) //Base <=> Managerが接続されている
                {
                    playerObjectBase.OnConnected();
                }
                if (isBaseConnected && isPlayerRestored) //Restoreが完了しBase <=> Managerが接続されている
                {
                    playerObjectBase.OnReady();
                }
                return true;
            }
            return false;
        }
    }
}