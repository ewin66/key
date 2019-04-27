using DrectSoft.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Xml.Serialization;

namespace DrectSoft.Common.Eop
{
    /// <summary>
    /// �����־��๤��
    /// </summary>
    public static class PersistentObjectFactory
    {
        #region static & const string
        private const string EOPNameSpace = "DrectSoft.Common.Eop.";
        #endregion

        #region properties
        private static ORMCollection OrmSettings
        {
            get
            {
                if (_ormSettings == null)
                    ResetORMSettings();

                return _ormSettings;
            }
        }
        private static ORMCollection _ormSettings;

        /// <summary>
        /// Ԥ�����Sql���
        /// </summary>
        public static PreDefineSqlCollection SqlSentences
        {
            get
            {
                if (_sqlSentences == null)
                    GetPreDefineSqlSentence();

                return _sqlSentences;
            }
        }
        private static PreDefineSqlCollection _sqlSentences;

        /// <summary>
        /// ���ݷ��ʶ���
        /// </summary>
        public static IDataAccess SqlExecutor
        {
            get
            {
                if (_sqlExecutor == null)
                    _sqlExecutor = DataAccessFactory.DefaultDataAccess;
                return _sqlExecutor;
            }
            set { _sqlExecutor = value; }
        }
        private static IDataAccess _sqlExecutor;

        #endregion

        #region private variables
        private static Dictionary<string, PropertyInfo[]> m_PropertyCache = new Dictionary<string, PropertyInfo[]>();
        private static Dictionary<string, ConstructorInfo[]> m_ConstructorCache = new Dictionary<string, ConstructorInfo[]>();
        private static Dictionary<string, MethodInfo[]> m_MethodCache = new Dictionary<string, MethodInfo[]>();
        #endregion

        #region private common methods
        private static PropertyInfo[] GetProperties(Type objType)
        {
            if (!m_PropertyCache.ContainsKey(objType.FullName))
                m_PropertyCache.Add(objType.FullName
                   , objType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));

            return m_PropertyCache[objType.FullName];
        }

        private static ConstructorInfo[] GetConstructors(Type objType)
        {
            if (!m_ConstructorCache.ContainsKey(objType.FullName))
                m_ConstructorCache.Add(objType.FullName, objType.GetConstructors());

            return m_ConstructorCache[objType.FullName];
        }

        private static MethodInfo[] GetMethods(Type objType)
        {
            if (!m_MethodCache.ContainsKey(objType.FullName))
                m_MethodCache.Add(objType.FullName
                   , objType.GetMethods(BindingFlags.Public | BindingFlags.Instance));
            return m_MethodCache[objType.FullName];
        }

        private static MethodInfo GetMethod(Type objType, string methodName)
        {
            MethodInfo[] methods = GetMethods(objType);
            if ((methods != null) && (!String.IsNullOrEmpty(methodName)))
            {
                foreach (MethodInfo info in methods)
                    if (info.Name == methodName)
                        return info;
            }
            return null;
        }

        private static void MergeParentAndChildSetting()
        {
            if (_ormSettings == null)
                return;

            // ѭ������Normal�������
            foreach (ORMapping orm in _ormSettings.ORMappings)
            {
                if (!String.IsNullOrEmpty(orm.ParentClass))
                {
                    DoMergeParentAndChildSetting(orm);
                }
            }
        }

        private static void DoMergeParentAndChildSetting(ORMapping child)
        {
            // �ҵ��丸��
            // ��������������������û�У�����뵽������
            ORMapping parent;
            parent = FindParentORMapping(child.ParentClass);
            if (parent == null)
                throw new ArgumentOutOfRangeException("ORM���ò���ȷ(ȱ��"
                   + child.ClassName + "�ĸ��ඨ��)");

            // ��һ��������������List�������ж����Զ����Ƿ����
            Collection<string> properties = new Collection<string>();

            if (parent.OneOnes != null)
                DoMergeOneOnes(child, parent, properties);
            if (parent.States != null)
                DoMergeSates(child, parent, properties);
            if (parent.Structures != null)
                DoMergeStructures(child, parent, properties);
            if (parent.ObjectClasses != null)
                DoMergeObjectClasses(child, parent, properties);
            if (parent.SubClasses != null)
                DoMergeSubClasses(child, parent, properties);
            if (parent.DefaultValues != null)
                DoMergeDefaultValues(child, parent);
        }
        /// <summary>
        /// �����޸ļӲ�����֤����
        /// add by ywk 2013��3��13��9:42:04 
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        /// <param name="properties"></param>
        private static void DoMergeOneOnes(ORMapping child, ORMapping parent, Collection<string> properties)
        {
            try
            {
                if (child.OneOnes == null)
                {
                    child.OneOnes = new Collection<OneToOneType>();
                }
                else
                {
                    //edit by ywk 2013��3��13��9:41:37 
                    //foreach (OneToOneType one in child.OneOnes)
                    //{
                    //    if (!properties.Contains(one.Property))
                    //    {
                    //        properties.Add(one.Property);
                    //    }
                    //}
                    //��Ϊforѭ�� ywk 
                    for (int i = 0; i < child.OneOnes.Count; i++)
                    {
                        if (!properties.Contains(child.OneOnes[i].Property))
                        {
                            properties.Add(child.OneOnes[i].Property);
                        }
                    }
                }

                //foreach (OneToOneType one in parent.OneOnes)
                //{
                //    //���ж�properties����
                //    if (properties.Count >= 0 && !properties.Contains(one.Property))
                //        child.OneOnes.Add(one.Clone());
                //}
                //��Ϊfor ѭ�� edit by ywk 2013��3��13��10:19:37 
                for (int i = 0; i < parent.OneOnes.Count; i++)
                {
                    if (!properties.Contains(parent.OneOnes[i].Property))
                    {
                        child.OneOnes.Add(parent.OneOnes[i].Clone());
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("DoMergeOneOnes��������" + ex.Message);
                //throw;
            }

        }

        private static void DoMergeSates(ORMapping child, ORMapping parent, Collection<string> properties)
        {
            if (child.States == null)
                child.States = new Collection<OneToStateType>();
            else
                foreach (OneToStateType state in child.States)
                    properties.Add(state.Property);

            foreach (OneToStateType state in parent.States)
            {
                if (!properties.Contains(state.Property))
                    child.States.Add(state.Clone());
            }
        }

        private static void DoMergeStructures(ORMapping child, ORMapping parent, Collection<string> properties)
        {
            try
            {
                if (child.Structures == null)
                    child.Structures = new Collection<ManyToStructureType>();
                else
                    foreach (ManyToStructureType structure in child.Structures)
                        properties.Add(structure.Property);

                foreach (ManyToStructureType structure in parent.Structures)
                {
                    if (!properties.Contains(structure.Property))
                        child.Structures.Add(structure.Clone());
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("DoMergeStructures��������" + ex.Message);
                //throw;
            }

        }

        private static void DoMergeObjectClasses(ORMapping child, ORMapping parent, Collection<string> properties)
        {
            if (child.ObjectClasses == null)
                child.ObjectClasses = new Collection<ManyToObjectClassType>();
            else
                foreach (ManyToObjectClassType objClass in child.ObjectClasses)
                    properties.Add(objClass.Property);

            foreach (ManyToObjectClassType objClass in parent.ObjectClasses)
            {
                if (!properties.Contains(objClass.Property))
                    child.ObjectClasses.Add(objClass.Clone());
            }
        }

        private static void DoMergeSubClasses(ORMapping child, ORMapping parent, Collection<string> properties)
        {
            if (child.SubClasses == null)
                child.SubClasses = new Collection<ManyToSubClassType>();
            else
                foreach (ManyToSubClassType subObj in child.SubClasses)
                    properties.Add(subObj.Property);

            foreach (ManyToSubClassType subObj in parent.SubClasses)
            {
                if (!properties.Contains(subObj.Property))
                    child.SubClasses.Add(subObj.Clone());
            }
        }

        private static void DoMergeDefaultValues(ORMapping child, ORMapping parent)
        {
            Collection<string> defValues = new Collection<string>();
            if (child.DefaultValues == null)
                child.DefaultValues = new Collection<DefaultValueType>();
            else
                foreach (DefaultValueType defValue in child.DefaultValues)
                    defValues.Add(defValue.Column);

            foreach (DefaultValueType defValue in parent.DefaultValues)
            {
                if (!defValues.Contains(defValue.Column))
                    child.DefaultValues.Add(defValue.Clone());
            }
        }

        private static ORMapping FindParentORMapping(string className)
        {
            if ((_ormSettings != null) && (_ormSettings.ParentORMappings != null))
            {
                foreach (ORMapping temp in _ormSettings.ParentORMappings)
                {
                    if (temp.ClassName == className)
                        return temp;
                }
            }
            return null;
        }

        private static ORMapping FindORMapping(string className)
        {
            if (OrmSettings.ORMappings != null)
            {
                foreach (ORMapping temp in OrmSettings.ORMappings)
                {
                    if (temp.ClassName == className)
                        return temp;
                }
            }
            return null;
        }

        private static int LocateProperty(PropertyInfo[] properties, string propName)
        {
            if (properties == null)
                return -1;
            if (String.IsNullOrEmpty(propName))
                throw new ArgumentNullException();

            for (int index = 0; index < properties.Length; index++)
                if (properties[index].Name == propName)
                    return index;
            return -1;
        }

        /// <summary>
        /// �ڶ�Ӧ���и���ԭʼ�������ӳ���������ȱʡֵ
        /// </summary>
        /// <param name="originalCol"></param>
        /// <param name="colMaps"></param>
        /// <returns>{����, ȱʡֵ}������ Cell ����ͬʱ�ǿ�</returns>
        private static string[] GetTrueColumnAndDefaultValue(string originalCol, Dictionary<string, ColumnToColumn> colMaps)
        {
            if ((colMaps == null) || (colMaps.Count == 0))
                return new string[] { originalCol, "" };
            else
            {
                if ((originalCol != null) && colMaps.ContainsKey(originalCol))
                    return new string[] { colMaps[originalCol].SourceColumn, colMaps[originalCol].DefaultValue };
                else
                    return new string[] { "", "" };
            }
        }

        /// <summary>
        /// �ڶ�Ӧ���и���ԭʼ�������ӳ�������
        /// </summary>
        /// <param name="originalCol"></param>
        /// <param name="colMaps"></param>
        /// <returns></returns>
        private static string GetTrueColumn(string originalCol, Dictionary<string, string> colMaps)
        {
            if ((colMaps == null) || (colMaps.Count == 0))
                return originalCol;
            else
            {
                if ((originalCol != null) && colMaps.ContainsKey(originalCol))
                    return colMaps[originalCol];
                else
                    return "";
            }
        }

        private static object GetTargetColumnValue(DataRow sourceRow, string targetColumn, Dictionary<string, ColumnToColumn> colMaps)
        {
            string[] trueCol = GetTrueColumnAndDefaultValue(targetColumn, colMaps);

            if (String.IsNullOrEmpty(trueCol[0])) // ����ƥ�������δ�ҵ���ǰ�еĶ�Ӧ��
            {
                if (!String.IsNullOrEmpty(trueCol[1])) // ��ȱʡֵ���򷵻�ȱʡֵ
                    return trueCol[1];
                else
                    return null;
            }
            else if (sourceRow.Table.Columns.Contains(trueCol[0]))
                return sourceRow[trueCol[0]];
            else
                return null;
        }

        private static object GetPropertyValue(object obj, PropertyInfo[] properties, string property)
        {
            int index = LocateProperty(properties, property);
            if (index > -1)
                return properties[index].GetValue(obj, null);
            return null;
        }

        private static bool CheckSpecialAttribute(object[] attrs, MethodSpecialKind targeAttr)
        {
            if (attrs.Length > 0)
            {
                SpecialMethodAttribute specAttr;
                foreach (object attr in attrs)
                {
                    specAttr = attr as SpecialMethodAttribute;
                    if ((attr != null) && (specAttr.Flag == targeAttr))
                        return true;
                }
            }
            return false;
        }

        private static string CastClassNameFromFullName(string typeFullName)
        {
            int lastIndex = typeFullName.LastIndexOf(Type.Delimiter);
            return typeFullName.Substring(lastIndex + 1, typeFullName.Length - lastIndex - 1);
        }

        private static object ConvertObjectValueBaseOnTrueType(object objValue, Type trueType)
        {
            bool nullValue;
            if ((objValue.GetType() == typeof(DBNull))
               || (objValue == null)
               || (String.IsNullOrEmpty(objValue.ToString().Trim())))
                nullValue = true;
            else
                nullValue = false;

            if (trueType == typeof(bool))
            {
                if (nullValue)
                    return false;
                else
                    //return Convert.ToBoolean(objValue, CultureInfo.CurrentCulture); modified by wwj 2012-07-18

                    if (objValue == null)
                        return Convert.ToBoolean(objValue, CultureInfo.CurrentCulture);
                    else
                        return Convert.ToBoolean(Convert.ToInt32(objValue), CultureInfo.CurrentCulture);
            }
            if (trueType == typeof(int))
            {
                if (nullValue)
                    return -1;
                else
                    return Convert.ToInt32(objValue, CultureInfo.CurrentCulture);
            }
            if (trueType == typeof(double))
            {
                if (nullValue)
                    return -1d;
                else
                    return Convert.ToDouble(objValue, CultureInfo.CurrentCulture);
            }
            if (trueType == typeof(decimal))
            {
                if (nullValue)
                    return -1m;
                else
                    return Convert.ToDecimal(objValue, CultureInfo.CurrentCulture);
            }
            if (trueType == typeof(string))
            {
                if (nullValue)
                    return "";
                else
                    return objValue;
            }
            if (trueType == typeof(DateTime))
            {
                if (nullValue)
                    return Convert.ToDateTime(null, CultureInfo.CurrentCulture);
                else
                {
                    try
                    {
                        return Convert.ToDateTime(objValue, CultureInfo.CurrentCulture);
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                }

            }
            if (trueType.BaseType == typeof(Enum))
            {
                if (nullValue)
                    return 0;
                else
                    return Convert.ToInt32(objValue, CultureInfo.CurrentCulture);//Add By zhouhui 2011-06-24 ���Enum���ܱ�ת��������
            }

            return objValue;
        }
        #endregion

        #region private methods
        private static void GetPreDefineSqlSentence()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PreDefineSqlCollection));

            //FileStream fileStream = new FileStream(FullSqlSentenceFileName, FileMode.Open);

            _sqlSentences = (PreDefineSqlCollection)serializer.Deserialize(BasicSettings.GetConfig(BasicSettings.PreDefineSqlSetting));

            //fileStream.Close();
        }

        private static void SetPropertyValueOfSubClass(object obj, PropertyInfo[] properties, DataRow sourceRow, ManyToSubClassType subObj)
        {
            // ʹ���������ù�����ȡ��ʵ�ʵ�����
            string className;

            if (!String.IsNullOrEmpty(subObj.KindColumn))
            {
                if (subObj.ClassName == "OrderContentFactory")
                    className = OrderContentFactory.GetOrderContentClassName(sourceRow[subObj.KindColumn]);
                else
                    throw new ArgumentException(MessageStringManager.GetString("ClassNotImplement"));
            }
            else
                className = subObj.ClassName;

            Object newObj = CreateAndIntializeObject(className, sourceRow);
            SetPropertyValue(obj, properties, newObj, subObj.Property);
        }
        /// <summary>
        /// �����޸� TYR Catch
        /// add by ywk 2013��3��13��10:22:29 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="properties"></param>
        /// <param name="sourceRow"></param>
        /// <param name="objClass"></param>
        /// <param name="colMaps"></param>
        private static void SetPropertyValueOfLinkObject(object obj, PropertyInfo[] properties, DataRow sourceRow, ManyToObjectClassType objClass, Dictionary<string, ColumnToColumn> colMaps)
        {
            try
            {
                // ʹ���������ù�����ȡ��ʵ�ʵ�����
                string className;

                if (String.IsNullOrEmpty(objClass.KindColumn))
                    className = objClass.ClassName;
                else
                    throw new ArgumentException(MessageStringManager.GetString("ClassNotImplement"));

                object newObj = null;
                // ĳЩ�������ͬһ�ű�������ͬ���ֶα��治ͬ����µ����ݣ�Ȼ����ĳ����־λ�����жϵ�ǰ�����ݱ�ʾ���������
                //   ��ӳ�䵽����ʱ��ÿ�������������һ�ֵ������ࡣ�����������лὫ�кͲ�ͬ�������ӳ��Ĺ�ϵ���������ɱ�־�ֶε�ֵ������ȡ��һ�����á�
                foreach (LinkedObject linkObj in objClass.LinkedObjects)
                {
                    if ((String.IsNullOrEmpty(className)) || (linkObj.ClassName == className))
                    {
                        // �п��ܴ��ڼ��������(���һ���й�����Link���󣬶�Link�����еĲ����ֶ��ֹ���������Ķ���)
                        // ������Ҫ�õ����յ�ԭʼ�к͹����еĹ�ϵ( a -> b, b -> c ==> a -> c)
                        Collection<ColumnToColumn> newMaps;
                        if ((colMaps != null) && (colMaps.Count > 0))
                        {
                            newMaps = new Collection<ColumnToColumn>();
                            ColumnToColumn newCol2Col;
                            foreach (ColumnToColumn linkColMap in linkObj.ColumnToColumns)
                            {
                                newCol2Col = new ColumnToColumn();
                                newCol2Col.TargetColumn = linkColMap.TargetColumn;// �������������
                                if (colMaps.ContainsKey(linkColMap.SourceColumn))
                                {
                                    newCol2Col.SourceColumn = colMaps[linkColMap.SourceColumn].SourceColumn;
                                    newCol2Col.DefaultValue = colMaps[linkColMap.SourceColumn].DefaultValue;
                                }
                                else if (!String.IsNullOrEmpty(linkColMap.DefaultValue))
                                {
                                    // �м�Ͽ��ˣ����ʾ���ܽ���������ϵ
                                    newCol2Col.SourceColumn = ""; // linkColMap.SourceColumn;
                                    newCol2Col.DefaultValue = linkColMap.DefaultValue;
                                }
                                else
                                    continue;
                                newMaps.Add(newCol2Col);
                            }
                        }
                        else
                            newMaps = linkObj.ColumnToColumns;

                        newObj = CreateAndIntializeObject(linkObj.ClassName, sourceRow, newMaps);
                        break;
                    }
                }
                SetPropertyValue(obj, properties, newObj, objClass.Property);
            }
            catch (Exception ex)
            {

                //MessageBox.Show("SetPropertyValueOfLinkObject��������" + ex.Message);
            }

        }

        private static void SetPropertyValueOfStruct(object obj, PropertyInfo[] properties, DataRow sourceRow, ManyToStructureType structure, Dictionary<string, ColumnToColumn> colMaps)
        {
            if ((structure.PropertyToColumn == null)
               || (structure.PropertyToColumn.Count == 0))
                throw new ArgumentNullException();

            // ȡ�ù��캯������Ϣ������ʵ��
            Type objType = Type.GetType(EOPNameSpace + structure.ClassName);
            ConstructorInfo[] ctors = GetConstructors(objType);
            ParameterInfo[] ctorParameters;
            object objValue;
            if (ctors != null)
            {
                object[] attrs;
                foreach (ConstructorInfo ctor in ctors)
                {
                    attrs = ctor.GetCustomAttributes(false);
                    if (CheckSpecialAttribute(attrs, MethodSpecialKind.DefaultCtor))
                    {
                        ctorParameters = ctor.GetParameters();
                        object[] values = new object[structure.PropertyToColumn.Count];
                        int index = 0;
                        object colValue;
                        for (; index < structure.PropertyToColumn.Count; index++)
                        {
                            if (String.IsNullOrEmpty(structure.PropertyToColumn[index].Column))
                            {
                                objValue = ConvertObjectValueBaseOnTrueType(
                                   structure.PropertyToColumn[index].DefaultValue
                                   , ctorParameters[index].ParameterType);
                            }
                            else
                            {
                                colValue = GetTargetColumnValue(sourceRow, structure.PropertyToColumn[index].Column, colMaps);
                                if (colValue == null)
                                    break;
                                else
                                    objValue = ConvertObjectValueBaseOnTrueType(colValue, ctorParameters[index].ParameterType);
                            }
                            values[index] = objValue;
                        }
                        if (index == structure.PropertyToColumn.Count)
                        {
                            Object structureObj = ctor.Invoke(values);
                            SetPropertyValue(obj, properties, structureObj, structure.Property);
                        }
                        break;
                    }
                }
            }
        }

        private static void SetPropertyValueOfState(object obj, PropertyInfo[] properties, DataRow sourceRow, OneToStateType state, Dictionary<string, ColumnToColumn> colMaps)
        {
            if (String.IsNullOrEmpty(state.Column))
                return;

            object value = GetTargetColumnValue(sourceRow, state.Column, colMaps);

            if (value == null)
                return;

            // ����ʵ��
            Type objType = Type.GetType(EOPNameSpace + state.ClassName);
            Object stateObj = Activator.CreateInstance(objType);

            MethodInfo[] methods = GetMethods(objType);
            if (methods != null)
            {
                object[] attrs;
                foreach (MethodInfo method in methods)
                {
                    attrs = method.GetCustomAttributes(false);
                    if (CheckSpecialAttribute(attrs, MethodSpecialKind.StateInitValueMethod))
                    {
                        method.Invoke(stateObj, new object[1] { value });

                        SetPropertyValue(obj, properties, stateObj, state.Property);
                        break;
                    }
                }
            }
        }

        private static void SetPropertyValueOfNormal(object obj, PropertyInfo[] properties, DataRow sourceRow, OneToOneType one, Dictionary<string, ColumnToColumn> colMaps)
        {
            if (String.IsNullOrEmpty(one.Column))
                return;

            object value = GetTargetColumnValue(sourceRow, one.Column, colMaps);

            if (value != null)
                SetPropertyValue(obj, properties, value, one.Property);
        }

        private static void SetPropertyValue(object obj, PropertyInfo[] properties, object objValue, string propertyName)
        {
            int index = LocateProperty(properties, propertyName);
            if (index > -1)
            {
                properties[index].SetValue(obj
                   , ConvertObjectValueBaseOnTrueType(objValue, properties[index].PropertyType)
                   , null);
            }
        }

        private static void SetColumnValueFromSubClass(DataRow targetRow, object obj, PropertyInfo[] properties, ManyToSubClassType subObject)
        {
            object linkObj = GetPropertyValue(obj, properties, subObject.Property);

            if (linkObj != null)
                SetDataRowValueFromObject(targetRow, linkObj);
        }

        private static void SetColumnValueFromLinkObject(DataRow targetRow, object obj, PropertyInfo[] properties, ManyToObjectClassType objClass, Dictionary<string, string> colMaps)
        {
            object linkObj = GetPropertyValue(obj, properties, objClass.Property);
            if (linkObj == null)
                return;

            Type objType = linkObj.GetType();

            foreach (LinkedObject link in objClass.LinkedObjects)
            {
                if (link.ClassName == CastClassNameFromFullName(objType.FullName))
                {
                    Collection<ColumnToColumn> newMaps;
                    if ((colMaps != null) && (colMaps.Count > 0))
                    {
                        newMaps = new Collection<ColumnToColumn>();
                        ColumnToColumn newCol2Col;
                        foreach (ColumnToColumn colMap in link.ColumnToColumns)
                        {
                            newCol2Col = new ColumnToColumn();
                            newCol2Col.TargetColumn = colMap.TargetColumn;
                            if (colMaps.ContainsKey(colMap.SourceColumn))
                                newCol2Col.SourceColumn = colMaps[colMap.SourceColumn];
                            else
                                newCol2Col.SourceColumn = colMap.SourceColumn;
                            newMaps.Add(newCol2Col);
                        }
                    }
                    else
                        newMaps = link.ColumnToColumns;

                    SetDataRowValueFromObject(targetRow, linkObj, newMaps);
                }
            }
        }

        private static void SetColumnValueFromStruct(DataRow targetRow, object obj, PropertyInfo[] properties, ManyToStructureType structure, Dictionary<string, string> colMaps)
        {
            if ((structure.PropertyToColumn == null)
               || (structure.PropertyToColumn.Count == 0))
                throw new ArgumentNullException();

            object objStruct = GetPropertyValue(obj, properties, structure.Property);
            if (objStruct == null)
                return;

            Type objType = objStruct.GetType();
            PropertyInfo[] structProps = GetProperties(objType);
            string trueColumn;
            object objValue;
            foreach (StructPropertyMap structMap in structure.PropertyToColumn)
            {
                trueColumn = GetTrueColumn(structMap.Column, colMaps);
                objValue = GetPropertyValue(objStruct, structProps, structMap.Property);

                if ((!String.IsNullOrEmpty(structMap.ActualProperty)) && (objValue != null))
                    objValue = GetPropertyValue(objValue
                       , objValue.GetType().GetProperties()
                       , structMap.ActualProperty);

                SetDataColumnValue(targetRow, trueColumn, objValue);
            }
        }

        private static void SetColumnValueFromState(DataRow targetRow, object obj, PropertyInfo[] properties, OneToStateType state, Dictionary<string, string> colMaps)
        {
            //if (String.IsNullOrEmpty(state.Column))
            //   throw new ArgumentNullException();

            object objState = GetPropertyValue(obj, properties, state.Property);
            if (objState == null)
                return;

            // �������ɷ���ֵ�ķ���
            Type objType = objState.GetType();
            MethodInfo[] methods = GetMethods(objType);
            if (methods != null)
            {
                object[] attrs;
                foreach (MethodInfo method in methods)
                {
                    attrs = method.GetCustomAttributes(false);
                    if (CheckSpecialAttribute(attrs, MethodSpecialKind.StateGetValueMethod))
                    {
                        string trueCol = GetTrueColumn(state.Column, colMaps);
                        object objValue = method.Invoke(objState, null);
                        SetDataColumnValue(targetRow, trueCol, objValue);
                        break;
                    }
                }
            }
        }

        private static void SetColumnValueFromNormal(DataRow targetRow, object obj, PropertyInfo[] properties, OneToOneType one, Dictionary<string, string> colMaps)
        {
            object objValue = GetPropertyValue(obj, properties, one.Property);

            string column = GetTrueColumn(one.Column, colMaps);

            SetDataColumnValue(targetRow, column, objValue);
        }

        private static void SetDataColumnValue(DataRow row, string columnName, object objValue)
        {
            if (row.Table.Columns.Contains(columnName))
            {
                if (objValue != null)
                {
                    if (objValue.GetType() == typeof(DateTime))
                        row[columnName] = ((DateTime)objValue).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);
                    else
                        row[columnName] = objValue;
                }
                else
                {
                    if (row.Table.Columns[columnName].AllowDBNull)
                        row[columnName] = DBNull.Value;
                    else
                        row[columnName] = 1;
                }
            }
        }

        private static void SetObjectPropertyFromDataRow(object targetObj, DataRow sourceRow, Collection<ColumnToColumn> colMaps)
        {
            try
            {
                if (targetObj == null)
                {
                    return;
                }
                Type objType = targetObj.GetType();

                ORMapping orm = FindORMapping(CastClassNameFromFullName(objType.FullName));
                if (orm == null)
                    throw new ArgumentOutOfRangeException("δ�ҵ�ָ����Ķ���");

                // ��ȡ������Public����
                PropertyInfo[] properties = GetProperties(objType);

                // �����BeginInit���������
                MethodInfo beginInitMethod = GetMethod(objType, "BeginInit");
                if (beginInitMethod != null)
                    beginInitMethod.Invoke(targetObj, null);

                Dictionary<string, ColumnToColumn> colMapDic = new Dictionary<string, ColumnToColumn>(); // ԭʼ������������ǰ��������
                if (colMaps != null)
                {
                    for (int i = 0; i < colMaps.Count; i++)
                    {
                        if (!colMapDic.ContainsKey(colMaps[i].TargetColumn))//���ж��Ƿ��������ͬ��KEY
                        {
                            colMapDic.Add(colMaps[i].TargetColumn, colMaps[i]);
                        }
                    }

                }

                // �����Ը�ֵ
                if (orm.OneOnes != null)
                {
                    foreach (OneToOneType oneOne in orm.OneOnes)
                        SetPropertyValueOfNormal(targetObj, properties, sourceRow, oneOne, colMapDic);
                }
                if (orm.States != null)
                {
                    foreach (OneToStateType state in orm.States)
                        SetPropertyValueOfState(targetObj, properties, sourceRow, state, colMapDic);
                }
                if (orm.Structures != null)
                {
                    foreach (ManyToStructureType structure in orm.Structures)
                        SetPropertyValueOfStruct(targetObj, properties, sourceRow, structure, colMapDic);
                }
                if (orm.ObjectClasses != null)
                {
                    foreach (ManyToObjectClassType objClass in orm.ObjectClasses)
                        SetPropertyValueOfLinkObject(targetObj, properties, sourceRow, objClass, colMapDic);
                }
                if (orm.SubClasses != null)
                {
                    foreach (ManyToSubClassType subObj in orm.SubClasses)
                        SetPropertyValueOfSubClass(targetObj, properties, sourceRow, subObj);
                }

                // �����EndInit���������
                MethodInfo endInitMethod = GetMethod(objType, "EndInit");
                if (endInitMethod != null)
                    endInitMethod.Invoke(targetObj, null);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("SetObjectPropertyFromDataRow��������" + ex.Message);
                //throw;
            }

        }
        #endregion

        #region public methods
        /// <summary>
        /// ��������ʼ��ָ������ʵ��
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="sourceRow">������ʼ�����ݵ�������</param>
        /// <returns></returns>
        public static Object CreateAndIntializeObject(string objectName, DataRow sourceRow)
        {
            return CreateAndIntializeObject(objectName, sourceRow, null);
        }

        /// <summary>
        /// ��������ʼ��ָ������ʵ��
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="sourceRow">������ʼ�����ݵ�������</param>
        /// <param name="colMaps">�����ͨ���������еĲ����ֶδ������õ��࣬����Ҫ�ṩ��ǰ���ݼ�����Ĭ�����ݼ����ֶ�ӳ���ϵ</param>
        /// <returns></returns>
        public static Object CreateAndIntializeObject(string objectName, DataRow sourceRow, Collection<ColumnToColumn> colMaps)
        {
            if (String.IsNullOrEmpty(objectName))
                throw new ArgumentNullException();
            if (sourceRow == null)
                throw new ArgumentNullException();

            // ���ݴ������������ʵ��
            Type objType = Type.GetType(EOPNameSpace + objectName);
            object obj = Activator.CreateInstance(objType);

            SetObjectPropertyFromDataRow(obj, sourceRow, colMaps);
            return obj;
        }

        /// <summary>
        /// ��DataRow��ʼ�����������
        /// </summary>
        /// <param name="targetObj"></param>
        /// <param name="sourceRow"></param>
        public static void InitializeObjectProperty(object targetObj, DataRow sourceRow)
        {
            if (targetObj == null)
                throw new ArgumentNullException();

            SetObjectPropertyFromDataRow(targetObj, sourceRow, null);
        }

        /// <summary>
        /// �ö������Ը�DataRow�ж�Ӧ���ֶθ�ֵ
        /// </summary>
        /// <param name="targetRow">��Ҫ��ֵ��������</param>
        /// <param name="obj">������ֵ�Ķ���</param>
        public static void SetDataRowValueFromObject(DataRow targetRow, object obj)
        {
            SetDataRowValueFromObject(targetRow, obj, null);
        }

        /// <summary>
        /// �ö������Ը�DataRow�ж�Ӧ���ֶθ�ֵ
        /// </summary>
        /// <param name="targetRow">��Ҫ��ֵ��������</param>
        /// <param name="obj">������ֵ�Ķ���</param>
        /// <param name="colMaps">���Ҫʹ�ù��������������ֵ������Ҫ�ṩ��ǰ���ݼ�����Ĭ�����ݼ����ֶ�ӳ���ϵ</param>
        public static void SetDataRowValueFromObject(DataRow targetRow, object obj, Collection<ColumnToColumn> colMaps)
        {
            if (obj == null)
                throw new ArgumentNullException();
            if (targetRow == null)
                throw new ArgumentNullException();

            // ���ݴ������������ʵ��
            Type objType = obj.GetType();

            ORMapping orm = FindORMapping(CastClassNameFromFullName(objType.FullName));
            if (orm == null)
                throw new ArgumentOutOfRangeException("δ�ҵ�ָ����Ķ���");

            // ��ȡ������Public����
            PropertyInfo[] properties = GetProperties(objType);

            // �򻯴�����ͳһ���ֶε�Ĭ��ֵ���ٸ������Ը�ֵ
            if (orm.DefaultValues != null)
            {
                foreach (DefaultValueType defVal in orm.DefaultValues)
                    if (targetRow.Table.Columns.Contains(defVal.Column))
                        targetRow[defVal.Column] = defVal.Value;
            }

            Dictionary<string, string> colMapDic = new Dictionary<string, string>(); // ԭʼ������������ǰ��������
            if (colMaps != null)
            {
                foreach (ColumnToColumn colCol in colMaps)
                {
                    if (!String.IsNullOrEmpty(colCol.SourceColumn))
                        colMapDic.Add(colCol.TargetColumn, colCol.SourceColumn);
                }
            }

            // ���ֶθ�ֵ
            if (orm.OneOnes != null)
            {
                foreach (OneToOneType one in orm.OneOnes)
                    SetColumnValueFromNormal(targetRow, obj, properties, one, colMapDic);
            }
            if (orm.States != null)
            {
                foreach (OneToStateType state in orm.States)
                    SetColumnValueFromState(targetRow, obj, properties, state, colMapDic);
            }
            if (orm.Structures != null)
            {
                foreach (ManyToStructureType structure in orm.Structures)
                    SetColumnValueFromStruct(targetRow, obj, properties, structure, colMapDic);
            }
            if (orm.ObjectClasses != null)
            {
                foreach (ManyToObjectClassType objClass in orm.ObjectClasses)
                    SetColumnValueFromLinkObject(targetRow, obj, properties, objClass, colMapDic);
            }
            if (orm.SubClasses != null)
            {
                foreach (ManyToSubClassType subObj in orm.SubClasses)
                    SetColumnValueFromSubClass(targetRow, obj, properties, subObj);
            }
        }

        /// <summary>
        /// ���µ���ORM����������
        /// </summary>
        public static void ResetORMSettings()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ORMCollection));

            //FileStream fileStream = new FileStream(FullOrmSettingFileName, FileMode.Open);

            _ormSettings = (ORMCollection)serializer.Deserialize(BasicSettings.GetConfig(BasicSettings.ORMappingSetting));
            // �ϲ���������������
            MergeParentAndChildSetting();

            //fileStream.Close();
        }

        /// <summary>
        /// �Դ������Ϊĸ���¡����
        /// </summary>
        /// <param name="sourceObj"></param>
        /// <returns></returns>
        public static object CloneEopBaseObject(object sourceObj)
        {
            if (sourceObj == null)
                return null;

            Type objType = sourceObj.GetType();

            if (objType.IsValueType)
                return null;

            object newObj = Activator.CreateInstance(objType);
            // ��������ֵ

            // ȡ������Public���ԣ������Set��������ֵ
            // �����Class��ݹ���ã�����ǽṹ�壬��Ҫ��Ĭ�Ϲ��촴��

            // ��ȡ������Public����
            PropertyInfo[] properties = GetProperties(objType);

            // �����BeginInit���������
            MethodInfo beginInitMethod = GetMethod(objType, "BeginInit");
            if (beginInitMethod != null)
                beginInitMethod.Invoke(newObj, null);

            object objValue;
            foreach (PropertyInfo prop in properties)
            {
                if (!prop.CanWrite)
                    continue;

                objValue = prop.GetValue(sourceObj, null);

                if ((prop.PropertyType.IsValueType) || (prop.PropertyType == typeof(string)))
                {
                    prop.SetValue(newObj, objValue, null);
                }
                else if (prop.PropertyType.IsClass)
                {
                    if (prop.PropertyType.BaseType == typeof(MulticastDelegate))
                        prop.SetValue(newObj, objValue, null);
                    else
                    {
                        object obj = PersistentObjectFactory.CloneEopBaseObject(objValue);
                        prop.SetValue(newObj, obj, null);
                    }
                }
            }

            // �����EndInit���������
            MethodInfo endInitMethod = GetMethod(objType, "EndInit");
            if (endInitMethod != null)
                endInitMethod.Invoke(newObj, null);

            return newObj;
        }

        /// <summary>
        /// ��ȡָ�����ƵĲ�ѯ���
        /// </summary>
        /// <param name="sqlName">��ѯ�������</param>
        /// <returns>��ѯ���</returns>
        public static string GetQuerySentenceByName(string sqlName)
        {
            foreach (SelectSentence sqlStatement in SqlSentences.Sentences)
                if (sqlStatement.Name == sqlName)
                    return sqlStatement.QuerySentence;

            throw new ArgumentOutOfRangeException(sqlName);
        }
        #endregion

    }
}
