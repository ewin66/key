using System;
using System.Collections.Generic;
using System.Text;

namespace DrectSoft.FrameWork.Plugin
{
    /// <summary>
    /// UI��ܲ������
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class FramePluginAttribute:PluginAttribute
    {
       /// <summary>
       /// UI���
       /// </summary>
       public override PluginType PluginType
        {
            get { return PluginType.Frame; }
        }
    }
}
