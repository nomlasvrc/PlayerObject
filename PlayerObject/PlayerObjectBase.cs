
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Nomlas.PlayerObject
{
    public class PlayerObjectBase : UdonSharpBehaviour
    {
        /// <summary>
        /// これは内部用変数です。
        /// Player Object Managerとの接続が完了したかどうか。
        /// </summary>
        private bool isManagerConnected;

        /// <summary>
        /// これは内部用変数です。
        /// Player Object Managerを返します。
        /// </summary>
        private PlayerObjectManager manager;

        /// <summary>
        /// 自分のPlayer Objectを返します。
        /// </summary>
        public PlayerObject localPlayerObject
        {
            get
            {
                if (isManagerConnected)
                {
                    return manager.localPlayerObject;
                }
                else { return null; }
            }
            private set { }
        }

        /// <summary>
        /// 自分のPlayer Objectが準備できたかどうか。
        /// </summary>
        public bool isConnected
        {
            get
            {
                if (isManagerConnected)
                {
                    return manager.isConnected;
                }
                else { return false; }
            }
            private set { }
        }

        /// <summary>
        /// 自分のPlayer Objectが準備できたかどうか。
        /// </summary>
        public bool isReady
        {
            get
            {
                if (isManagerConnected)
                {
                    return manager.isReady;
                }
                else { return false; }
            }
            private set { }
        }

        /// <summary>
        /// データが更新されたときに呼び出されます。
        /// 値が変わっていなくても呼び出されることに注意してください。
        /// </summary>
        /// <param name="player"></param>
        public virtual void OnDataUpdated(VRCPlayerApi player) { }

        /// <summary>
        /// 自分のPlayer Objectの準備が完了したときに呼び出されます。
        /// 値は同期していない可能性があります。
        /// </summary>
        public virtual void OnConnected() { }

        /// <summary>
        /// 準備が完了したときに呼び出されます。
        /// </summary>
        public virtual void OnReady() { }

        /// <summary>
        /// 変数を保存します。
        /// </summary>
        public void RequestPersistence()
        {
            manager.RequestPersistence();
        }

        /// <summary>
        /// これは内部用関数です。
        /// Player Object Managerをセットします。
        /// </summary>
        /// <param name="playerObjectManager"></param>
        public bool SetManager(PlayerObjectManager playerObjectManager)
        {
            manager = playerObjectManager;
            isManagerConnected = true;
            return true;
        }
    }
}
