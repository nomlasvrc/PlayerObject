
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
        public bool isReady { get; private set; }
        private bool isBaseConnected;
        private bool isConnected;
        private bool isPlayerRestored;

        void Start()
        {
            if (!Utilities.IsValid(connectTarget))
            {
                Debug.LogError("Managerに接続するコンポーネントが指定されていません!");
            }
            isBaseConnected = connectTarget.SetManager(this);
            CheckStatus();
        }

        private void CheckStatus()
        {
            if (isBaseConnected && isConnected && isPlayerRestored)
            {
                isReady = true;
                connectTarget.OnReady();
            }
        }

        internal void OnDataUpdated(VRCPlayerApi player)
        {
            connectTarget.OnDataUpdated(player);
        }

        public void RequestPersistence()
        {
            if (!isConnected) return;
            localPlayerObject.RequestPersistence();
        }

        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (!player.isLocal) return;
            isPlayerRestored = true;
            CheckStatus();
        }

        internal bool ConnectToManager(VRCPlayerApi player, PlayerObject playerObject)
        {
            if (!player.isLocal) return false;
            localPlayerObject = playerObject;
            if (!Utilities.IsValid(localPlayerObject)) return false;

            isConnected = true;
            CheckStatus();
            return true;
        }
    }
}