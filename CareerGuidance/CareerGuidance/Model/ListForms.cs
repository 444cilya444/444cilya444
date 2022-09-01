using CareerGuidance.Forms;
using CareerGuidance.Forms.PersonalityTrait;
using CareerGuidance.Forms.Stat;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace CareerGuidance
{

    class ListForms
    {
        static Dictionary<string, Form> DicForms = new Dictionary<string, Form>();
        public static void RegistrForm(Form owner)
        {
            DicForms.Add(owner.Name, owner);
            Assembly project = Assembly.GetExecutingAssembly();
            foreach (Type t in project.GetTypes())
            {
                if (t.BaseType == typeof(Form))
                {
                    var emptyCtor = t.GetConstructor(Type.EmptyTypes);
                    if (emptyCtor != null)
                    {
                        var f = (Form)emptyCtor.Invoke(new object[] { });
                        if(f.Name != "")                        
                            DicForms.Add(f.Name, f);
                        
                     
                    }
                }
            }
        }

        /// <summary>
        /// Открывает форму по ключу с параметром TopMost
        /// </summary>
        /// <param name="Forms">Ключ формы</param>
        /// <param name="TopMost">TopMost</param>
        /// <param name="form">(this)Формакоторую нужно закрыть</param>
        /// <param name="hide">Закрывать или не закрывать форму?(bool)</param>
        public static void OpenForms(string KeyForm, bool TopMost, Form form, bool hide)
        {
            if (hide)
                form.BeginInvoke((MethodInvoker)(() => form.Hide()));
            Form fr = form;
            fr.BeginInvoke((MethodInvoker)(() => fr = GetForm($"{KeyForm}")));
            fr.BeginInvoke((MethodInvoker)(() => fr.TopMost = TopMost));
            fr.BeginInvoke((MethodInvoker)(() => fr.Show()));
        }
        /// <summary>
        /// Метод возвращает форму по ключу
        /// </summary>
        /// <param name="KeyForm">Ключ формы</param>
        public static Form GetForm(string KeyForm)
        {
            return DicForms[KeyForm];
        }
        public static void CleanText(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(EgoldsGoogleTextBox))
                    c.Text = string.Empty;
                if (c.GetType() == typeof(GroupBox))
                    CleanText(c);
            }
        }
        public static string capSentences(string str)
        {
            string s = "";

            if (str[str.Length - 1] == '.')
                str = str.Remove(str.Length - 1, 1);

            char[] delim = { '.' };

            string[] tokens = str.Split(delim);

            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim();

                tokens[i] = char.ToUpper(tokens[i][0]) + tokens[i].Substring(1);

                s += tokens[i];
            }

            return s;
        }
    }
}
