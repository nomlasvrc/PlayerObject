
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Nomlas.PlayerObject
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class PlayerObject : UdonSharpBehaviour
    {
        [SerializeField] PlayerObjectManager manager;
        private VRCPlayerApi owner;

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

        private void Start()
        {
            owner = Networking.GetOwner(this.gameObject);
            manager.ConnectToManager(owner, this);
        }

        public override void OnDeserialization()
        {
            manager.OnDataUpdated(owner);
        }

        internal void RequestPersistence()
        {
            RequestSerialization();
        }
    }
}