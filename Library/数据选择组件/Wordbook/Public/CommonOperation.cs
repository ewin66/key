using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DrectSoft.Wordbook
{
    /// <summary>
    /// �ṩһЩ�����Ĵ�����
    /// </summary>
    public static class CommonOperation
    {
        /// <summary>
        /// �õ�������ַ������ַ�������            edit by wangj 2013 1 17
        /// </summary>
        /// <param name="input">Ҫ�ж����͵��ַ���</param>
        /// <returns></returns>
        public static StringType GetStringType(string input)
        {
            if (String.IsNullOrEmpty(input))
                return StringType.Empty;

            int index;
            bool hasMinus = (input[0] == '-'); // ��ǵ�һ���ַ��Ƿ��Ǹ���
            if (hasMinus) // ������и��ţ���ӵڶ����ַ���ʼ����
                index = 1;
            else
                index = 0;

            StringType type = StringType.Char;
            for (; index < input.Length; index++)
            {
                switch (CharUnicodeInfo.GetUnicodeCategory(input[index]))
                {

                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.ModifierLetter:
                    case UnicodeCategory.TitlecaseLetter:
                    case UnicodeCategory.UppercaseLetter:
                        // ����ȫ����ĸ��ֱ����Ϊ��������
                        if ((input[index] >= 65281) && (input[index] < 65373))
                        {
                            type = StringType.Other;
                        }
                        else
                        {
                            if (index == 0)
                                type = StringType.EnglishChar;
                            else
                            {
                                // ֻ��ǰ�����жϳ�����������ȫ���֣�����Ҫ�ı�����
                                if (type == StringType.Numeric)
                                    type = StringType.Char;
                            }
                        }
                        break;
                    case UnicodeCategory.OtherLetter:
                        type = StringType.Other;
                        break;
                    case UnicodeCategory.DecimalDigitNumber:
                        if (index == 0)
                            type = StringType.Numeric;
                        else
                        {
                            // ֻ��ǰ�����жϳ�����������ȫӢ����ĸ������Ҫ�ı�����
                            if (type == StringType.EnglishChar)
                                type = StringType.Char;
                        }
                        //break; 
                        continue;                                                            //edit by wangj 2013 1 17
                    default: type = StringType.Other;                                        //add by wangj 2013 1 17
                        break;
                }
                // ����Ѿ��жϳ����������������������ֱ���˳�ѭ��
                if (type == StringType.Other)
                    break;
            }
            if (hasMinus && (type != StringType.Numeric))
                return StringType.Other;

            return type;
        }

        /// <summary>
        /// �Դ�����������ʽ�ַ����е������ַ�����ת�塣
        /// ������DataView��DataColumn��Expressionʱ��Ҫ���д˴���
        /// </summary>
        /// <param name="condition">Expression �е��������ʽ</param>
        /// <param name="operaName">������</param>
        /// <returns></returns>
        public static string TransferCondition(string condition, CompareOperator operaName)
        {
            if (condition == null)
                throw new ArgumentNullException(MessageStringManager.GetString("NullParameter"));
            // ����ֻ����Like�ı��ʽ
            switch (operaName)
            {
                case CompareOperator.Like:
                    if (condition.Contains("["))
                        condition = condition.Replace("[", "[[]");
                    if (condition.Contains("*"))
                        condition = condition.Replace("*", "[*]");
                    if (condition.Contains("%"))
                        condition = condition.Replace("%", "[%]");
                    if (condition.Contains("_"))
                        condition = condition.Replace("_", "[_]");
                    break;
                default:
                    break;
            }

            return condition;
        }

        /// <summary>
        /// �Դ�����������ʽ�ַ����е������ַ�����ת�塣
        /// ������DataView��DataColumn��Expressionʱ��Ҫ���д˴���
        /// </summary>
        /// <param name="condition">Expression �е��������ʽ</param>
        /// <param name="operaName">������</param>
        /// <param name="handleQuote">�Ƿ�������</param>
        /// <returns></returns>
        public static string TransferCondition(string condition, CompareOperator operaName, bool handleQuote)
        {
            if (condition == null)
                throw new ArgumentNullException(MessageStringManager.GetString("NullParameter"));
            // �������滻������������
            if (handleQuote)
                condition = condition.Replace("'", "''");

            return TransferCondition(condition, operaName);
        }

        /// <summary>
        /// ���ݲ�����ö�ٱ����õ�ʵ�ʵĲ�����
        /// </summary>
        /// <param name="filterOperator">������ö�ٱ���</param>
        /// <returns>ʵ�ʵĲ�����</returns>
        public static string GetOperatorSign(CompareOperator filterOperator)
        {
            switch (filterOperator)
            {
                case CompareOperator.Equal:
                    return "=";
                case CompareOperator.In:
                    return "IN";
                case CompareOperator.Less:
                    return "<";
                case CompareOperator.Like:
                    return "LIKE";
                case CompareOperator.More:
                    return ">";
                case CompareOperator.NotEqual:
                    return "<>";
                case CompareOperator.NotLess:
                    return ">=";
                case CompareOperator.NotMore:
                    return "<=";
                default:
                    throw new ArgumentOutOfRangeException(MessageStringManager.GetString("OperatorSignNotDefined"));
            }
        }

        /// <summary>
        /// ���ݴ����Wordbook�ؼ���Ϣ�ַ��������ֵ�ʵ��
        /// </summary>
        /// <param name="keyInfo">�ֵ�Ĺؼ���Ϣ��ɵ��ַ���</param>
        /// <returns>�ֵ���ʵ��</returns>
        public static BaseWordbook GetWordbookByString(string keyInfo)
        {
            if (String.IsNullOrEmpty(keyInfo))
                return null;
            //throw new ArgumentNullException("�ֵ�����Ϊ��");

            // �������ֵ�������
            int p = keyInfo.IndexOf(SeparatorSign.OtherSeparator);
            if (p < 0)
                return null;
            //throw new ArgumentException("δ�����ֵ�����");
            keyInfo = keyInfo.Substring(p + 3, keyInfo.Length - p - 3);
            p = keyInfo.IndexOf(SeparatorSign.OtherSeparator);
            if (p < 0)
                return null;

            BaseWordbook wordbook = WordbookStaticHandle.GetWordbook(keyInfo.Substring(0, p));
            if (wordbook != null)
                wordbook.ParameterValueComb = keyInfo.Substring(p + 3, keyInfo.Length - p - 3);
            return wordbook;
        }
    }
}
