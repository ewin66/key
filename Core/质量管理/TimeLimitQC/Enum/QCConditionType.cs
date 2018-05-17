using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// ��������
    /// </summary>
    public enum QCConditionType
    {
        /// <summary>
        /// ��
        /// </summary>
        None = 0, 

        /// <summary>
        /// ����״̬�ı�
        /// </summary>
        [Description("����״̬�ı�")]
        PatStateChange = 1,

        /// <summary>
        /// �����ļ��ı�
        /// </summary>
        [Description("�����ļ��ı�")]
        EmrChange = 2,
        
        /// <summary>
        /// ҽ���ı�
        /// </summary>
        [Description("ҽ���ı�")]
        AdviceChange = 3,
    }
}
