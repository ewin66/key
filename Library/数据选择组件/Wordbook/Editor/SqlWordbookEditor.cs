using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DrectSoft.Wordbook
{
   /// <summary>
   /// SqlWordbook���Եı༭��
   /// </summary>
   [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode), PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
   public class SqlWordbookEditor : System.Drawing.Design.UITypeEditor
   {
      /// <summary>
      /// ����Sql�ֵ�༭��
      /// </summary>
      public SqlWordbookEditor()
      { }

      /// <summary>
      /// ��ȡ�� EditValue ����ʹ�õı༭����ʽ��
      /// </summary>
      /// <param name="context"></param>
      /// <returns></returns>
      public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
      {
         if (context != null)
         {
            return UITypeEditorEditStyle.Modal;
         }
         return base.GetEditStyle(context);
      }

      /// <summary>
      /// ʹ�� GetEditStyle ָʾ�ı༭����ʽ�༭ָ���Ķ���ֵ��
      /// </summary>
      /// <param name="context"></param>
      /// <param name="provider"></param>
      /// <param name="value"></param>
      /// <returns></returns>
      public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
      {
         if ((context != null) && (provider != null))
         {
            // Access the property browser's UI display service, IWindowsFormsEditorService
            IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (editorService != null)
            {
               // ���� UI editor ��ʵ��
               FormSqlWordbook modalEditor = new FormSqlWordbook();

               // ���뵱ǰ���Ե�ֵ
               modalEditor.Wordbook = (SqlWordbook)value;

               if (editorService.ShowDialog(modalEditor) == DialogResult.OK)
               {
                  // �������Ե���ֵ
                  return modalEditor.Wordbook;
               }
            }
         }
         return base.EditValue(context, provider, value);
      }
   }
}
