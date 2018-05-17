using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// �������
    /// </summary>
    public enum QCResultType
    {
        /// <summary>
        /// ��
        /// </summary>
        None = 0,

        /// <summary>
        /// �����ļ��ı�
        /// </summary>
        [Description("�����ļ��ı�")]
        EmrChange = 1,

        /// <summary>
        /// ʱ����ʾ��Ч��
        /// </summary>
        [Description("ʱ����ʾ��Ч��")]
        TimeChange = 2,
    }
}
