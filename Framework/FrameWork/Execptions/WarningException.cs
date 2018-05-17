using System;
using System.Collections.Generic;
using System.Text;
using DrectSoft.FrameWork.Execptions;

namespace DrectSoft.FrameWork.Execptions
{
    /// <summary> 
    /// �������쳣������ֻ����Ҫ���������쳣���Ӵ���̳�
    /// </summary>
    public class WarningException:FrameWorkException
    {
        /// <summary>
        /// ��ʼ��һ���쳣
        /// </summary>
        public WarningException() 
        {
        }

        /// <summary>
        /// ��ʼ��һ���쳣
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public WarningException(string message)
            : base(message)
        { }

        /// <summary>
        /// ��ʼ��һ���쳣
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="exception">�ڲ��쳣</param>
        public WarningException(string message, Exception exception)
            : base(message, exception)
        { }
    }
}