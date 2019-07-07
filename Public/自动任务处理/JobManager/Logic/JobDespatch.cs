using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;

namespace DrectSoft.JobManager
{
    /// <summary>
    /// �����������
    /// </summary>
    internal class JobDespatch
    {
        #region fields
        private JobTaskManager m_MissionManager;
        private Collection<Job> m_JobQueue;
        private Object m_LockJobQueue = new Object();
        private Timer m_Timer;
        #endregion

        #region ctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        public JobDespatch(JobTaskManager manager)
        {
            m_MissionManager = manager;
            m_JobQueue = new Collection<Job>();
            m_Timer = new Timer(10000);
            //����ʱ���ʱ��ִ���¼��� 
            m_Timer.AutoReset = true;
            m_Timer.Enabled = true;
            m_Timer.Elapsed += new ElapsedEventHandler(m_Timer_Elapsed);
        }


        #endregion

        #region public methods
        /// <summary>
        /// ��ʼ��ѯ
        /// </summary>
        public void Start()
        {
            m_Timer.Start();
        }

        /// <summary>
        /// ֹͣ������
        /// </summary>
        public void Stop()
        {
            m_Timer.Stop();
        }
        #endregion

        #region private methods

        void m_Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            PollingJob();
        }

        /// <summary>
        /// ��ѯ�Ƿ�����Ҫִ�е�����
        /// </summary>
        private void PollingJob()
        {
            m_Timer.Stop();
            lock (m_LockJobQueue)
            {
                // ����ǰʱ�����Ҫִ�е�������ӵ�����
                foreach (Job job in m_MissionManager.AllJobs)
                {
                    if (job.Enable && (job.Action != null) && (job.Action.SynchState != SynchState.Busy)
                       && job.JobSchedule.NeedRunNow)
                        if (!m_JobQueue.Contains(job))
                            m_JobQueue.Add(job);
                }
            }

            foreach (Job job in m_JobQueue)
                DoJobThread(job);
            m_JobQueue.Clear();
            m_Timer.Start();
        }

        private void DoJobThread(Job job)
        {

            lock (job)
            {
                if ((job.Action != null) && (job.Action.SynchState == SynchState.Stop))
                {
                    try
                    {
                        JobLogHelper.WriteLog(new JobExecuteInfoArgs(job, "��ʼ", TraceLevel.Info));
                        job.Action.Execute();
                        job.JobSchedule.LastExecuteTime = DateTime.Now;
                        JobLogHelper.WriteLog(new JobExecuteInfoArgs(job, "����", TraceLevel.Info));

                    }
                    catch (Exception exception)
                    {
                        JobLogHelper.WriteLog(new JobExecuteInfoArgs(job, string.Empty, exception));
                    }
                }

            }
            //// �����߳�
            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += new DoWorkEventHandler(JobThread_DoWork);
            //worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(JobThread_RunWorkerCompleted);

            //// ���У��������Ӷ������Ƴ�����
            //worker.RunWorkerAsync(job);
        }

        private void JobThread_DoWork(object sender, DoWorkEventArgs e)
        {
            Job job = e.Argument as Job;
            e.Result = e.Argument;
            lock (job)
            {
                if ((job.Action != null) && (job.Action.SynchState == SynchState.Stop))
                {
                    try
                    {
                        JobLogHelper.WriteLog(new JobExecuteInfoArgs(job, "��ʼ", TraceLevel.Info));
                        job.Action.Execute();
                    }
                    catch (Exception err)
                    {
                        JobLogHelper.WriteLog(new JobExecuteInfoArgs(job, String.Empty, err));
                    }
                }
            }
        }

        private void JobThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Job job = e.Result as Job;
            //lock (job)
            {
                if (job != null)
                {
                    if ((job.Action != null) && (job.Action.SynchState == SynchState.Stop))
                    {
                        job.JobSchedule.LastExecuteTime = DateTime.Now;
                        JobLogHelper.WriteLog(new JobExecuteInfoArgs(job, "����", TraceLevel.Info));
                    }
                    lock (m_LockJobQueue)
                    {
                        if (m_JobQueue.Contains(job))
                            m_JobQueue.Remove(job);
                    }
                }
            }
        }
        #endregion
    }
}
