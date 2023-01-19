using System;
using System.Collections.Generic;

namespace UnityEngine.UI.SaveSystem
{
    [System.Serializable]
    public class ProjectData
    {
        public ProjectData(string projectName)
        {
            this.projectName = projectName;
        }

        public string projectName;
        public string firstSceneName;
        public long lastRemoteSave;
        public long lastLocalSave;
    }
}