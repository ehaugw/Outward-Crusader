using InstanceIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Crusader
{
    public class CrusaderRPCManager : Photon.MonoBehaviour
    {
        public static CrusaderRPCManager Instance;

        internal void Start()
        {
            Instance = this;

            var view = this.gameObject.AddComponent<PhotonView>();
            view.viewID = IDs.CrusaderRPCPhotonID;
            Debug.Log("Registered CrusaderRPC with ViewID " + this.photonView.viewID);
        }
    }
}
