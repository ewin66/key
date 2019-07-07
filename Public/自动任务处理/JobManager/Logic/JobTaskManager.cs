using DrectSoft.Core;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;

namespace DrectSoft.JobManager
{
    /// <summary>
    /// ������������������߼�������
    /// </summary>
    public class JobTaskManager
    {

        #region properties
        /// <summary>
        /// �����ļ��ж����ϵͳ����
        /// </summary>
        public JobConfig Systems
        {
            get
            {
                if (_systems == null)
                    InitializeConfig();
                return _systems;
            }
        }
        private JobConfig _systems;

        /// <summary>
        /// ���е�����
        /// </summary>
        public Collection<Job> AllJobs
        {
            get
            {
                if (_allJobs == null)
                    InitializeConfig();
                return _allJobs;
            }
        }
        private Collection<Job> _allJobs;
        #endregion

        #region fields
        private JobDespatch m_MissionDespatch;
        private const string m_JobtaskInfo = "JobTaskConfig";
        #endregion

        #region ctor
        public JobTaskManager()
        {
            // ȷ��EMR�����ݿ��Ѿ����������򴴽������ʧ��
            //TestSqlServiceHadStarted();

            // ��������������Ϣ����ÿ�������Actionʵ��
            CreateJobAction();
        }
        #endregion

        #region public & internal method

        public void WriteAllLog()
        {
            foreach (Job job in AllJobs)
            {
                JobLogHelper.WriteLog(new JobExecuteInfoArgs(job, "��Ӧ�ó���ر�", TraceLevel.Info));
            }
        }

        /// <summary>
        /// ��¼��־
        /// </summary>
        /// <param name="e"></param>
        public void WriteLog(JobExecuteInfoArgs e)
        {
            JobLogHelper.WriteLog(e);
        }

        /// <summary>
        /// �����������޸�
        /// </summary>
        public void SaveJobConfig()
        {
            if (Systems != null)
            {
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\" + "DataSynchConfig.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(typeof(JobConfig));
                serializer.Serialize(file, Systems);
                file.Close();
            }
        }
        #endregion

        #region private methods
        private void InitializeConfig()
        {
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"\" + "DataSynchConfig.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            XmlSerializer serializer = new XmlSerializer(typeof(JobConfig));
            _systems = (JobConfig)serializer.Deserialize(file);
            file.Close();

            _allJobs = new Collection<Job>();
            foreach (SystemsJobDefine system in _systems.JobsOfSystem)
            {
                foreach (Job job in system.Jobs)
                    _allJobs.Add(job);
            }
        }

        public void RegisterMissions()
        {

            m_MissionDespatch = new JobDespatch(this);
            m_MissionDespatch.Start();
        }


        public void StopMissions()
        {
            if (m_MissionDespatch != null)
                m_MissionDespatch.Stop();
        }

        /// <summary>
        /// ����sql�����Ƿ��Ѿ�����
        /// </summary>
        /// <returns></returns>
        private void TestSqlServiceHadStarted()
        {
            int testTimes = 60;
            do
            {
                try
                {
                    IDataAccess sqlHelp = DataAccessFactory.DefaultDataAccess;
                    sqlHelp.ExecuteDataTable("select * from Users where 1=2");
                    return;
                }
                catch
                {
                    testTimes--;
                    Thread.Sleep(30000); // 30����һ��
                }
            } while (testTimes > 0);

            throw new ApplicationException("�޷��������ݿ�");
        }

        private void CreateJobAction()
        {
            for (int i = AllJobs.Count - 1; i >= 0; i--)
            {
                AllJobs[i].Action = CreateActionInstance(AllJobs[i].Class, AllJobs[i].Library);
                if (AllJobs[i].Action != null)
                    AllJobs[i].Action.Parent = AllJobs[i];
            }
        }

        private IJobAction CreateActionInstance(string className, string assemblyName)
        {
            try
            {
                Assembly assembly = Assembly.Load(Path.GetFileNameWithoutExtension(assemblyName));
                Type actionType = assembly.GetType(className, true, true);

                return Activator.CreateInstance(actionType) as IJobAction;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
