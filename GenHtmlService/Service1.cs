using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Configuration;

namespace GenHtmlService
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer timer1;  //计时器
        public Service1()
        {
            InitializeComponent();
        }

        //protected override void OnStart(string[] args)
        //{
        //    using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\log.txt", true))
        //    {
        //        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Start.");
        //    }
        //}

        //protected override void OnStop()
        //{
        //    using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\log.txt", true))
        //    {
        //        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Stop.");
        //    }
        //}


        protected override void OnStart(string[] args)
        {
            timer1 = new System.Timers.Timer();
            timer1.Interval = 3000;  //设置计时器事件间隔执行时间
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
            timer1.Enabled = true;
        }


        protected override void OnStop()
        {
            this.timer1.Enabled = false;
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string folderName = ConfigurationManager.AppSettings["LogFile"].ToString();
            string txtName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string path = folderName.EndsWith("\\") ? folderName : folderName + "\\" + txtName;          
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
           
            
            sw.WriteLine("WindowsService: Service Started" + DateTime.Now.ToString() + "\n");

            sw.Flush();
            sw.Close();
            fs.Close();
            //执行SQL语句或其他操作
        }



    }
}
