using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// ����ʱ������ʱ����ʹ�õ�����
    /// </summary>
    public class TimeLimitUseAttribute:Attribute
    {
        bool _canuse;
        string _description = string.Empty;

        /// <summary>
        /// CanUse
        /// </summary>
        public bool CanUse
        {
            get { return _canuse; }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="description">��������</param>
        public TimeLimitUseAttribute(string description)
        {
            _canuse = true;
            _description = description;
        }
    }
}
