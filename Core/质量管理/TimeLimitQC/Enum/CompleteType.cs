using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.ObjectModel;

namespace DrectSoft.Core.TimeLimitQC
{
    /// <summary>
    /// ���״̬
    /// </summary>
    public enum CompleteType
    {
        /// <summary>
        /// δ���
        /// </summary>
        NonComplete = 0, 

        /// <summary>
        /// �����
        /// </summary>
        Completed =1, 
    }

    /// <summary>
    /// ʱ�޹����¼״̬
    /// </summary>
    public enum RuleRecordState
    { 
        /// <summary>
        /// ��
        /// </summary>
        None = 0,

        /// <summary>
        /// ��ʱ��δ���
        /// </summary>
        UndoIntime = 1,

        /// <summary>
        /// ��ʱ�����
        /// </summary>
        DoIntime = 2,

        /// <summary>
        /// ��ʱδ���
        /// </summary>
        UndoOuttime = 3,

        /// <summary>
        /// ��ʱ���
        /// </summary>
        DoOuttime = 4,
    }

    /// <summary>
    /// ���ݼ�¼״̬
    /// </summary>
    public enum RecordState
    {
        /// <summary>
        /// ��Ч
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// ��Ч
        /// </summary>
        Valid = 1,

        /// <summary>
        /// ��Ч����Ҫ��������
        /// </summary>
        ValidWait = 2,

        /// <summary>
        /// ��Ч������ʾ
        /// </summary>
        ValidNonVisible = 3,
    }

    /// <summary>
    /// ʱ�޹����¼״̬������ʾ
    /// </summary>
    public class Enum2Chinese
    {
        //Type _type;
        Collection<ChineseEnum> _enumNames = new Collection<ChineseEnum>();

        public Collection<ChineseEnum> EnumNames
        {
            get { return _enumNames; }
        }

        public struct ChineseEnum
        {
            public string Name;
            public Enum Value;
            public ChineseEnum(string name, Enum value)
            {
                Name = name;
                Value = value;
            }
            public override string ToString()
            {
                return Name;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="enumtype"></param>
        public Enum2Chinese(Type enumtype)
        {
            if (!enumtype.IsEnum) return;
            FieldInfo[]  fields = enumtype.GetFields();
            foreach (FieldInfo field in fields)
            {
                object[] attrs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs.Length > 0)
                {
                    ChineseEnum ce = new ChineseEnum(((System.ComponentModel.DescriptionAttribute)attrs[0]).Description, field.GetValue(field) as Enum);
                    if (!_enumNames.Contains(ce)) _enumNames.Add(ce);
                }
            }
        }

        public ChineseEnum FindChineseEnum(Enum value)
        {
            for (int i = 0; i < _enumNames.Count; i++)
            {
                if (_enumNames[i].Value.Equals(value)) return _enumNames[i];
            }
            return new ChineseEnum();
        }
    }

}
