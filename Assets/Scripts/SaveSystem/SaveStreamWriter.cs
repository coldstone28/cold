using System;
using System.IO;

namespace UnityEngine.UI.SaveSystem
{
    public class SaveStreamWriter : StreamWriter
    {
        internal delegate void DisposedHandler(object sender, EventArgs e);
        internal event DisposedHandler OnDispose;
        
        internal SaveStreamWriter(string s) : base(s) { }

        protected override void Dispose(bool f) {
            base.Dispose(f);
            FireOnDispose();
        }
        
        private void FireOnDispose()
        {
            // Make sure someone is listening to event
            if (OnDispose == null) return;

            EventArgs args = new EventArgs();
            OnDispose(this, args);
        }
    }
}