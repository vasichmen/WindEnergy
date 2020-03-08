using SolarEnergy;
using SolarEnergy.SolarLib.Classes.Collections;
using SolarEnergy.SolarLib.Classes.Structures;
using SolarEnergy.UI.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindEnergy.UI.Ext
{
    /// <summary>
    /// контроллер вкладок документов
    /// </summary>
    public class TabControlExt : TabControl
    {
        public TabControlExt()
        {
            ShowToolTips = true;
            DrawMode = TabDrawMode.OwnerDrawFixed;
            this.ItemSize = new Size(100, 20);
            this.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        }

        /// <summary>
        /// открыть новую вкладку с заданным рядом
        /// </summary>
        /// <param name="range">ряд данных для отображения</param>
        /// <param name="text">заголовок вкладки</param>
        internal TabPageExt OpenNewTab(DataRange data, string text = "Новый документ")
        {
            TabPageExt ntab = new TabPageExt(data, text);
            this.TabPages.Add(ntab);
            this.SelectedTab = ntab;
            return ntab;
        }

        /// <summary>
        /// обработка нажатий на заголовки вкладок
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            e = e ?? throw new ArgumentNullException(nameof(e));

            //закрытие вкладки по нажатию на кнопку закрыть
            Rectangle tabTextArea = this.GetTabRect(SelectedIndex);
            Rectangle closeButtonArea = new Rectangle(tabTextArea.X + tabTextArea.Width - 16, 5, 13, 13);
            Point pt = new Point(e.X, e.Y);
            if (closeButtonArea.Contains(pt))
            {
                //закрытие вкладки
                _ = (this.SelectedTab as TabPageExt).ClosePage();
            }

            //закрытие при нажатии СКМ
            if (e.Button == MouseButtons.Middle)
                for (int i = 0; i < this.TabPages.Count;i++)
                {
                    Rectangle rect = GetTabRect(i);
                    if (rect.Contains(pt))
                        _ = (TabPages[i] as TabPageExt).ClosePage();
                }
        }

        /// <summary>
        /// прорисовка заголовка вкладки и кнопки закрытия
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e = e ?? throw new ArgumentNullException(nameof(e));
            RectangleF tabTextArea = RectangleF.Empty;
            for (int nIndex = 0; nIndex < this.TabCount; nIndex++)
            {
                if (nIndex != this.SelectedIndex)
                {
                    /*if not active draw ,inactive close button*/
                    tabTextArea = this.GetTabRect(nIndex);
                    using (Bitmap bmp = Resources.page_close_button)
                    {
                        e.Graphics.DrawImage(bmp, tabTextArea.X + tabTextArea.Width - 16, 5, 13, 13);
                    }
                }
                else
                {
                    tabTextArea = this.GetTabRect(nIndex);
                    LinearGradientBrush br = new LinearGradientBrush(tabTextArea,
                        SystemColors.ControlLightLight, SystemColors.Control,
                        LinearGradientMode.Vertical);
                    e.Graphics.FillRectangle(br, tabTextArea);

                    /*if active draw ,inactive close button*/
                    using (Bitmap bmp = Resources.page_close_button)
                    {
                        e.Graphics.DrawImage(bmp,
                            tabTextArea.X + tabTextArea.Width - 16, 5, 13, 13);
                    }
                    br.Dispose();
                }
                string str = this.TabPages[nIndex].Text;
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                using (SolidBrush brush = new SolidBrush(
                    this.TabPages[nIndex].ForeColor))
                {
                    //Draw the tab header text
                    e.Graphics.DrawString(str, this.Font, brush,
                    tabTextArea, stringFormat);
                }
            }
        }
        
    }
}
