﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using QuartzServiceLib;
using PMS.IModel;
using PMS.Model;

namespace QuartzProxy
{
    /// <summary>
    /// 作业调度服务代理类
    /// </summary>
   public class QuartzServiceClientProxy:ClientBase<IJobService>,IJobService
    {       

        public bool AddScheduleJob(J_JobInfo jobInfo, IJobData jobData = null)
        {
            return base.Channel.AddScheduleJob(jobInfo);
        }

        public void PauseJob(string jobName, string jobGroup)
        {
            base.Channel.PauseJob(jobName, jobGroup);
        }

        public void ResumeAllJob()
        {
            base.Channel.ResumeAllJob();
        }
    }
}