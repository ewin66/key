using DrectSoft.Emr.QCTimeLimit;
using DrectSoft.JobManager;
using System;

[assembly: Job("ʱ���������ݸ���", "���ڸ��²�����дʱ�����", "���Ӳ���", true, typeof(TimeQcManager))]
namespace DrectSoft.JobManager
{
    /// <summary>
    /// ʱ���������ݸ���
    /// </summary>
    public class TimeQcManager : BaseJobAction
    {
        #region ctor
        public TimeQcManager()
        {
        }
        #endregion

        #region public IJobAction ��Ա

        public override void Execute()
        {
            base.SynchState = SynchState.Busy;
            try
            {
                QCTimeLimitInnerService service = new QCTimeLimitInnerService();
                service.MainProcess();
            }
            catch (Exception ex)
            {
                JobLogHelper.WriteLog(new JobExecuteInfoArgs(this.Parent, ex.Message));
                throw ex;
            }
            finally
            {
                base.SynchState = SynchState.Stop;
            }
        }
        #endregion
    }
}
