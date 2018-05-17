using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace DrectSoft.Common.Library {
    /// <summary>
    /// showlist ƥ�䷽ʽ��¼��
    /// ���û���¼�Բ�ͬ�ֵ�Ĳ�ѯ��ʽ�����ã�������ע�����(���������У��ڵ�¼��Ӧ���õ�ǰ�û�����)
    /// </summary>
    public static class ShowListMatchTypeRecorder {
        #region const variables
        private const string ApplicationRegKey = @"SOFTWARE\ & DrectSoft\ShowList";
        #endregion

        #region properties
        /// <summary>
        /// ��ǰʹ��ShowList���û�
        /// </summary>
        public static string CurrentUserId {
            get { return _currentUserId; }
            set {
                _currentUserId = value;
            }
        }
        private static string _currentUserId;

        private static Dictionary<string, ShowListMatchTypeSetting> CurrentUserSetting {
            get {
                if (!m_SettingCache.ContainsKey(CurrentUserId)) {
                    m_SettingCache.Add(CurrentUserId, new Dictionary<string, ShowListMatchTypeSetting>());

                    RegistryKey regKey = Registry.LocalMachine.OpenSubKey(CurrentUserRegFullKey);
                    try {
                        if (regKey == null) {
                            regKey = Registry.LocalMachine.CreateSubKey(CurrentUserRegFullKey);
                        }

                        foreach (string valueName in regKey.GetValueNames())
                            m_SettingCache[CurrentUserId].Add(valueName, new ShowListMatchTypeSetting(regKey.GetValue(valueName).ToString()));
                    }
                    finally {
                        regKey.Close();
                    }
                }
                return m_SettingCache[CurrentUserId];
            }
        }

        private static string CurrentUserRegFullKey { get { return ApplicationRegKey + "\\" + CurrentUserId; } }

        private static ShowListMatchTypeSetting DefaultMatchSetting {
            get {
                if (_defaultMatchSetting == null)
                    _defaultMatchSetting = new ShowListMatchTypeSetting();
                return _defaultMatchSetting;
            }
        }
        private static ShowListMatchTypeSetting _defaultMatchSetting;
        #endregion

        #region fields
        private static Dictionary<string, Dictionary<string, ShowListMatchTypeSetting>> m_SettingCache = new Dictionary<string, Dictionary<string, ShowListMatchTypeSetting>>();
        #endregion

        #region internal methods
        /// <summary>
        /// ��ȡָ���ֵ��Ӧ��Ĭ������
        /// </summary>
        /// <param name="wordbookName"></param>
        /// <returns></returns>
        internal static ShowListMatchTypeSetting ReadDefaultSetting(string wordbookName) {
            if (String.IsNullOrEmpty(CurrentUserId) || String.IsNullOrEmpty(wordbookName))
                return DefaultMatchSetting;
            // ���ȼ�黺�棬���û����ʹ��Ĭ��ֵ(ÿ���û���������һ�ζ����)
            if (CurrentUserSetting.ContainsKey(wordbookName))
                return CurrentUserSetting[wordbookName];
            else
                return DefaultMatchSetting;
        }

        /// <summary>
        /// ����ָ���ֵ��Ӧ��Ĭ�����á������Ĭ�ϵ�������ͬ�򲻱���
        /// </summary>
        /// <param name="wordbookName"></param>
        /// <param name="matchType"></param>
        /// <param name="isDynamic"></param>
        internal static void WriteSetting(string wordbookName, ShowListMatchType matchType, bool isDynamic) {
            if (String.IsNullOrEmpty(CurrentUserId) || String.IsNullOrEmpty(wordbookName))
                return;

            if ((matchType == DefaultMatchSetting.MatchType) && (isDynamic == DefaultMatchSetting.IsDynamic))
                RemoveSetting(wordbookName);
            else {
                // �����в����ڣ�����ӻ��棬���뵽ע���
                if (!CurrentUserSetting.ContainsKey(wordbookName)) {
                    CurrentUserSetting.Add(wordbookName, new ShowListMatchTypeSetting(matchType, isDynamic));
                    WriteSettingToReg(wordbookName, CurrentUserSetting[wordbookName]);
                }
                // ����뻺���е������Ƿ�һ�£���һ��ʱ�޸Ļ��棬�ٸ���ע���
                else {
                    if ((CurrentUserSetting[wordbookName].IsDynamic != isDynamic)
                       || (CurrentUserSetting[wordbookName].MatchType != matchType)) {
                        CurrentUserSetting[wordbookName].IsDynamic = isDynamic;
                        CurrentUserSetting[wordbookName].MatchType = matchType;
                        WriteSettingToReg(wordbookName, CurrentUserSetting[wordbookName]);
                    }
                }
            }
        }
        #endregion

        #region private methods

        //private static ShowListMatchTypeSetting ReadSettingFromReg(string wordbookName)
        //{
        //   string regValue = Registry.GetValue(CurrentUserRegFullKey, wordbookName, String.Empty);

        //   CurrentUserSetting.Add(wordbookName, new ShowListMatchTypeSetting(regValue));

        //   return CurrentUserSetting[wordbookName];
        //}

        private static void WriteSettingToReg(string wordbookName, ShowListMatchTypeSetting setting) {
            try {
                Registry.SetValue(Registry.LocalMachine + "\\" + CurrentUserRegFullKey, wordbookName, setting.ToString());
            }
            catch { }
        }

        private static void RemoveSetting(string wordbookName) {
            if (!CurrentUserSetting.ContainsKey(wordbookName)) {
                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(CurrentUserRegFullKey, true);
                if (regKey == null) return;
                try {

                    regKey.DeleteValue(wordbookName);
                }
                catch { }
                finally {
                    regKey.Close();
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// showList ƥ�䷽ʽ������
    /// </summary>
    internal class ShowListMatchTypeSetting {

        /// <summary>
        /// ƥ��ģʽ
        /// </summary>
        public ShowListMatchType MatchType {
            get { return _matchType; }
            set { _matchType = value; }
        }
        private ShowListMatchType _matchType;

        /// <summary>
        /// ��̬����
        /// </summary>
        public bool IsDynamic {
            get { return _isDynamic; }
            set { _isDynamic = value; }
        }
        private bool _isDynamic;

        /// <summary>
        /// 
        /// </summary>
        public ShowListMatchTypeSetting() {
            MatchType = ShowListMatchType.Any;
            IsDynamic = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matchType"></param>
        /// <param name="isDynamic"></param>
        public ShowListMatchTypeSetting(ShowListMatchType matchType, bool isDynamic) {
            _matchType = matchType;
            _isDynamic = isDynamic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public ShowListMatchTypeSetting(string value)
            : this() {
            if (!String.IsNullOrEmpty(value)) {
                // ��ֱ���ֵ
                string[] settings = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (settings.Length == 2) {
                    _matchType = (ShowListMatchType)Enum.Parse(typeof(ShowListMatchType), settings[0]);
                    _isDynamic = Convert.ToBoolean(settings[1]);
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return MatchType.ToString() + ";" + IsDynamic.ToString();
        }
    }
}
