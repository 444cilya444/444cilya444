using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace WindowsFormsApp1
{
    public class MultiColumnComboBox : ComboBox
    {
        public MultiColumnComboBox()
        {
            DrawMode = DrawMode.OwnerDrawVariable;
        }

        public new DrawMode DrawMode
        {
            get
            {
                return base.DrawMode;
            }
            set
            {
                if (value != DrawMode.OwnerDrawVariable)
                {
                    throw new NotSupportedException("Needs to be DrawMode.OwnerDrawVariable");
                }
                base.DrawMode = value;
            }
        }

        public new ComboBoxStyle DropDownStyle
        {
            get
            {
                return base.DropDownStyle;
            }
            set
            {
                if (value == ComboBoxStyle.Simple)
                {
                    throw new NotSupportedException("ComboBoxStyle.Simple not supported");
                }
                base.DropDownStyle = value;
            }
        }

        protected override void OnDataSourceChanged(EventArgs e)
        {                  
            base.OnDataSourceChanged(e);      
            InitializeColumns();
        }

        protected override void OnValueMemberChanged(EventArgs e)
        {
            base.OnValueMemberChanged(e);
            InitializeValueMemberColumn();
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
            this.DropDownWidth = (int)CalculateTotalWidth();
        }

        const int columnPadding = 5;
        private float[] columnWidths = Array.Empty<float>();
        private string[] columnNames = Array.Empty<string>();
        private int valueMemberColumnIndex = 0;
        private void InitializeColumns()
        {
            PropertyDescriptorCollection propertyDescriptorCollection = DataManager.GetItemProperties();
            Array.Resize(ref columnWidths, propertyDescriptorCollection.Count);
            Array.Resize(ref columnNames, propertyDescriptorCollection.Count);
            for (int colIndex = 0; colIndex < propertyDescriptorCollection.Count; colIndex++)
            {
                string name = propertyDescriptorCollection[colIndex].Name;
                columnNames[colIndex] = name;
            }
        }

        private void InitializeValueMemberColumn()
        {
            int colIndex = 0;
            foreach (string columnName in columnNames)
            {
                if (string.Compare(columnName, ValueMember, true, CultureInfo.CurrentUICulture) == 0)
                {
                    valueMemberColumnIndex = colIndex;
                    break;
                }
                colIndex++;
            }
        }

        private float CalculateTotalWidth()
        {
            float totWidth = 0;
            foreach (int width in columnWidths)
            {
                totWidth += (width + columnPadding);
            }
            return totWidth + SystemInformation.VerticalScrollBarWidth;
        }
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (DesignMode)
                return;
            for (int colIndex = 0; colIndex < columnNames.Length; colIndex++)
            {
                string item = Convert.ToString(FilterItemOnProperty(Items[e.Index], columnNames[colIndex]));
                SizeF sizeF = e.Graphics.MeasureString(item, Font);
                columnWidths[colIndex] = Math.Max(columnWidths[colIndex], sizeF.Width);
            }
            float totWidth = CalculateTotalWidth();
            e.ItemWidth = (int)totWidth;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (DesignMode)
                return;

            e.DrawBackground();

            Rectangle boundsRect = e.Bounds;
            int lastRight = 0;

            using (Pen linePen = new Pen(SystemColors.GrayText))
            {
                using (SolidBrush brush = new SolidBrush(ForeColor))
                {
                    if (columnNames.Length == 0)
                    {
                        e.Graphics.DrawString(Convert.ToString(Items[e.Index]), Font, brush, boundsRect);
                    }
                    else
                    {
                        for (int colIndex = 0; colIndex < columnNames.Length; colIndex++)
                        {
                            string item = Convert.ToString(FilterItemOnProperty(Items[e.Index], columnNames[colIndex]));

                            boundsRect.X = lastRight;
                            boundsRect.Width = (int)columnWidths[colIndex] + columnPadding;
                            lastRight = boundsRect.Right;

                            if (colIndex == valueMemberColumnIndex)
                            {
                                using (Font boldFont = new Font(Font, FontStyle.Bold))
                                    e.Graphics.DrawString(item, boldFont, brush, boundsRect);
                            }
                            else
                            {
                                e.Graphics.DrawString(item, Font, brush, boundsRect);
                            }

                            if (colIndex < columnNames.Length - 1)
                            {
                                e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom);
                            }
                        }
                    }
                };
                
            }          
            e.DrawFocusRectangle();
        }
    }
}
