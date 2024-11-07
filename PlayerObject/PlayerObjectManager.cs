
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Nomlas.PlayerObject
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class PlayerObjectManager : UdonSharpBehaviour
    {
        [SerializeField] PlayerObjectBase connectTarget;
        public PlayerObject localPlayerObject { get; private set; }
        public bool isConnected { get; private set; }
        public bool isReady { get; private set; }
        private bool isBaseConnected;
        private bool isPlayerRestored;

        void Start()
        {
            if (!Utilities.IsValid(connectTarget))
            {
                Debug.LogError("Managerに接続するコンポーネントが指定されていません!");
            }
            isBaseConnected = connectTarget.SetManager(this);
            if (isConnected) //Manager <=> Objectが接続されており、OnConnected()が保留状態
            {
                connectTarget.OnConnected();
            }
            if (isConnected && isPlayerRestored) //Restoreが完了しManager <=> Objectが接続されている
            {
                connectTarget.OnReady();
            }
        }

        internal void OnDataUpdated(VRCPlayerApi player)
        {
            connectTarget.OnDataUpdated(player);
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
                connectTarget.OnReady();
            }
        }

        internal bool ConnectToManager(VRCPlayerApi player, PlayerObject playerObject)
        {
            if (!player.isLocal) return false;
            localPlayerObject = playerObject;
            if (!Utilities.IsValid(localPlayerObject)) return false;
            
            isConnected = true;
            if (isBaseConnected) //Base <=> Managerが接続されている
            {
                connectTarget.OnConnected();
            }
            if (isBaseConnected && isPlayerRestored) //Restoreが完了しBase <=> Managerが接続されている
            {
                connectTarget.OnReady();
            }
            return true;
        }
    }
}