using DrectSoft.Common.Eop;
using System;

namespace DrectSoft.Core.DoctorAdvice
{
    /// <summary>
    /// ���������ʾ����
    /// </summary>
    internal class ProcessHintArgs : EventArgs
    {
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        public string ProcessHint
        {
            get { return _processHint; }
            set { _processHint = value; }
        }
        private string _processHint;

        /// <summary>
        /// ���������ʾ����
        /// </summary>
        /// <param name="processHint">��ʾ��Ϣ</param>
        public ProcessHintArgs(string processHint)
        {
            _processHint = processHint;
        }
    }

    /// <summary>
    /// �����ύ�¼��Ĳ���
    /// </summary>
    public class DataCommitArgs : EventArgs
    {
        /// <summary>
        /// �����ύ����
        /// </summary>
        public DataCommitType CommitType
        {
            get { return _commitType; }
        }
        private DataCommitType _commitType;

        /// <summary>
        /// ����Ƿ��Ѿ������
        /// </summary>
        public bool Handled
        {
            get { return _handled; }
            set { _handled = value; }
        }
        private bool _handled;

        /// <summary>
        /// �����ύ�¼��Ĳ���
        /// </summary>
        /// <param name="commitType">�����ύ����</param>
        public DataCommitArgs(DataCommitType commitType)
        {
            _commitType = commitType;
        }
    }

    /// <summary>
    /// ��ǰѡ����Ŀ����
    /// </summary>
    public class OrderItemArgs : EventArgs
    {
        /// <summary>
        /// ����Ƿ�����Ŀ����
        /// </summary>
        public bool HadData
        {
            get { return _hadData; }
        }
        private bool _hadData;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public ItemKind Kind
        {
            get { return _kind; }
        }
        private ItemKind _kind;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ItemCode
        {
            get { return _itemCode; }
        }
        private string _itemCode;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string ItemName
        {
            get { return _itemName; }
        }
        private string _itemName;

        /// <summary>
        /// ������λ
        /// </summary>
        public string DoseUnit
        {
            get { return _doseUnit; }
        }
        private string _doseUnit;

        /// <summary>
        /// �÷�
        /// </summary>
        public string Usage
        {
            get { return _usage; }
        }
        private string _usage;

        /// <summary>
        /// ��ǰѡ����Ŀ����
        /// </summary>
        /// <param name="kind">��Ŀ����</param>
        /// <param name="code">��Ŀ����</param>
        /// <param name="name">��Ŀ����</param>
        /// <param name="unit">������λ</param>
        /// <param name="usage">�÷�</param>
        public OrderItemArgs(bool hadData, ItemKind kind, string code, string name, string unit, string usage)
        {
            _hadData = hadData;
            _kind = kind;
            _itemCode = code;
            _itemName = name;
            _doseUnit = unit;
            _usage = usage;
        }
    }
}
