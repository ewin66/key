using DrectSoft.FrameWork;
using DrectSoft.FrameWork.WinForm;
using DrectSoft.FrameWork.WinForm.Plugin;
using System;

namespace DrectSoft.Core.OwnBedInfo
{
    /// <summary>
    /// ����һ��������
    /// </summary>
    public class BedMappingStartUp : IStartPlugIn
    {
        #region IStartup ��Ա

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public IPlugIn Run(IEmrHost application)
        {
            if (application == null)
                throw new ArgumentNullException("application");

            DocCenter frmDocCenter = new DocCenter(application);
            PlugIn plg = new PlugIn(this.GetType().ToString(), frmDocCenter);
            return plg;
        }

        void plg_PatientChanging(object sender, System.ComponentModel.CancelEventArgs arg)
        {
            //TODO
            //����Ƿ���Ҫ����
        }
        #endregion
    }
}
