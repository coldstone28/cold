using System;
using System.Collections.Generic;

namespace UnityEngine.UI.WebService
{
    [Serializable]
    public class RemoteProjectList
    {
        public List<RemoteData> remoteData = new List<RemoteData>();
    }
    
    [Serializable]
    public class RemoteData
    {
        public string projectName;
        public long lastRemoteSave;
        public int projectSize;
        public int sceneCount;
    }
}