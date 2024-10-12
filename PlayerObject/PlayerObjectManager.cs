
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
        private bool isBaseConnected;

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
                return true;
            }
            return false;
        }
    }
}