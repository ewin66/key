using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.WinForm {
    /// <summary>
    /// ö���࣬������ʾ��ܹ���ļ����׶�
    /// </summary>
    [Flags]
    public enum BuilderStage {
        /// <summary>
        /// ��¼
        /// </summary>
        Login = 0,
        /// <summary>
        /// ��ʼ��
        /// </summary>
        Initialization = 1,
        /// <summary>
        /// ��ɳ�ʼ��
        /// </summary>
        PostInitialization = 2
    }
}
