using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cyotek.Windows.Forms;
using System.Windows.Forms;

namespace SpyPointData
{
    public class ImageViewer
    {
        public List<ImageBox> IBs = new List<ImageBox>();
        public int LastShowCount = 0;

        public ImageViewer()
        {
            IBs = new List<ImageBox>();
        }
        public void NullPicture(TableLayoutPanel tlp)
        {
            tlp.Controls.Clear();
            IBs.Clear();
            tlp.RowCount = 1;
            tlp.ColumnCount = 1;
        }

        public void ShowPictures(TableLayoutPanel tlp, Photo p, List<Photo> pics = null)
        {
            int thisShowCount;
            if (p != null)
                thisShowCount = 1;
            else
                thisShowCount = pics.Count;



            if (thisShowCount == LastShowCount && thisShowCount == 1)
            {
                //The likely
                //Already have 1 ImageBox
                //Just Update Image
                //SetTableLayout(1);
                if (IBs.Count == 0)
                    IBs.Add(new ImageBox());
                IBs = new List<ImageBox>() { IBs[0] };
                p.PicToImageBox(IBs[0]);
                if (IBs.Count > 1)
                {
                    IBs.RemoveRange(1, IBs.Count - 1);
                }
                //tableLayoutPanelPic.Controls.Clear();
                SetTableLayout(thisShowCount, tlp);
                tlp.Controls.Add(IBs[0]);
            }

            if (thisShowCount > LastShowCount && thisShowCount > 1)
            {
                //Add more items
                //GetTableLayout(tableLayoutPanelPic, thisShowCount);
                tlp.Controls.Clear();

                foreach (Photo pic in pics)
                {
                    pic.ForceId();
                    int ind = IBs.FindIndex(ib => ib.Tag.Equals(pic.id));
                    if (ind < 0)
                    {
                        ImageBox ib = new ImageBox();
                        pic.PicToImageBox(ib);
                        IBs.Add(ib);
                    }
                }
                int cnt = 0;
                int size = SetTableLayout(thisShowCount, tlp);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        tlp.Controls.Add(IBs[cnt], j, i);
                        cnt++;
                        if (cnt >= IBs.Count)
                            break;
                    }
                    if (cnt >= IBs.Count)
                        break;
                }

                foreach (var ib in IBs)
                {
                    if (ib.ZoomFactor < 5)
                        ib.ZoomToFit();
                }
            }

            if (thisShowCount < LastShowCount && thisShowCount > 1)
            {
                //Add more items
                //GetTableLayout(tableLayoutPanelPic, thisShowCount);
                tlp.Controls.Clear();

                List<ImageBox> newIBs = new List<ImageBox>();
                foreach (Photo pic in pics)
                {
                    pic.ForceId();

                    int ind = IBs.FindIndex(ib => ib.Tag.Equals(pic.id));
                    if (ind >= 0)
                    {
                        newIBs.Add(IBs[ind]);
                    }
                }
                IBs = newIBs;

                int cnt = 0;
                int size = SetTableLayout(thisShowCount, tlp);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        tlp.Controls.Add(IBs[cnt], j, i);
                        cnt++;
                        if (cnt >= IBs.Count)
                            break;
                    }
                    if (cnt >= IBs.Count)
                        break;
                }

                foreach (var ib in IBs)
                {
                    if (ib.ZoomFactor < 5)
                        ib.ZoomToFit();
                }
            }

            if (IBs.Count == 0)
            {
                tlp.RowCount = 1;
                tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, tlp.Height));
                tlp.ColumnCount = 1;
                tlp.RowStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, tlp.Width));

                ImageBox ib = new ImageBox();
                ib.Dock = DockStyle.Fill;
                tlp.Controls.Clear();
                tlp.Controls.Add(ib);
                IBs.Add(ib);
                if (p != null)
                    p.PicToImageBox(ib);
            }



            if (p != null)
                LastShowCount = 1;
            else
                LastShowCount = pics.Count;
        }
        public int SetTableLayout(int num, TableLayoutPanel tlp)
        {
            int rc;
            if (num == 1)
                rc = 1;
            else if (num <= 4)
            {
                rc = 2;
            }
            else if (num <= 9)
                rc = 3;
            else
                rc = 0;

            tlp.RowStyles.Clear();
            tlp.ColumnStyles.Clear();
            tlp.RowCount = rc;
            tlp.ColumnCount = rc;

            for (int i = 0; i < rc; i++)
            {
                tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, tlp.Height / rc));
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, tlp.Width / rc));
            }
            return rc;
        }
    }
}
