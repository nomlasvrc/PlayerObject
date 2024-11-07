
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Nomlas.PlayerObjectAddon
{
    public class PlayerObjectAddonBase : UdonSharpBehaviour
    {
        /// <summary>
        /// Local Playerのアドオン。
        /// </summary>
        public PlayerObjectAddon localPlayerObjectAddon { get; private set; }

        /// <summary>
        /// Local Playerのアドオンが準備できたか。
        /// </summary>
        public bool isReady { get; private set; }

        /// <summary>
        /// アドオンの準備が完了したときに呼び出されます。
        /// </summary>
        /// <param name="playerObjectAddon"></param>
        public virtual void OnAddonReady(PlayerObjectAddon playerObjectAddon) {}


        // ここから下は内部用です
        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (!player.isLocal) return;
            localPlayerObjectAddon = FindAddon(player);
            if (localPlayerObjectAddon == null)
            {
                Debug.LogError("アドオンの取得に失敗しました (" + this.gameObject.name + ")");
            }
            isReady = true;
            OnAddonReady(localPlayerObjectAddon);
        }

        private PlayerObjectAddon FindAddon(VRCPlayerApi player)
        {
            var objects = Networking.GetPlayerObjects(player);
            for (int i = 0; i < objects.Length; i++)
            {
                if (!Utilities.IsValid(objects[i])) continue;
                PlayerObjectAddon foundScript = objects[i].GetComponentInChildren<PlayerObjectAddon>();
                if (!Utilities.IsValid(foundScript)) continue;
                if (foundScript.GetAddonBase() == this) return foundScript;
            }
            return null;
        }
    }
}