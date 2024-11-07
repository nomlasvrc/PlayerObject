
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

namespace Nomlas.PlayerObjectAddon
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class PlayerObjectAddon : UdonSharpBehaviour
    {
        [SerializeField] private PlayerObjectAddonBase main;

        //----- ここにPersistenceさせたい変数を入れる


        //これは例
        public string example
        {
            get { return _example; }
            set { _example = value; }
        }
        [UdonSynced] private string _example;
        //


        //-----
        //----- ここに関数を追加(あれば)



        //-----

        public PlayerObjectAddonBase GetAddonBase()
        {
            return main;
        }

        internal void RequestPersistence()
        {
            RequestSerialization();
        }
    }
}