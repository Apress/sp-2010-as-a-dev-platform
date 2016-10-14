using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Microsoft.SharePoint;

namespace AsynchWebPart
{

    public abstract class AsyncTask<T> where T : new()
    {
        public T Result { get; protected set; }
        private Action task;
        private Action taskFinished;

        public AsyncTask()
        {
        }

        public AsyncTask(Action finishHandler)
        {
            taskFinished = finishHandler;
        }

        public virtual IAsyncResult OnBegin(object sender, EventArgs e, AsyncCallback cb, object data)
        {
            task = new Action(Execute);
            return task.BeginInvoke(cb, data);
        }

        public virtual void OnEnd(IAsyncResult result)
        {
            if (taskFinished != null)
            {
                taskFinished.Invoke();
            }
            task.EndInvoke(result);
        }

        public virtual void OnTimeout(IAsyncResult result)
        {
            Result = default(T);
        }

        public abstract void Execute();

    }


    public class ReadFileAsync : AsyncTask<XDocument>
    {

        public string FileName { get; set; }

        public ReadFileAsync(string fileName, Action finishCallback)
            : base(finishCallback)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName");
            if (!File.Exists(fileName))
                throw new FileNotFoundException();
            FileName = fileName;
        }

        public override void Execute()
        {
            try
            {
                XDocument xdoc = XDocument.Load(FileName);
                Result = xdoc;
            }
            catch
            {
            }
        }

    }
}
