using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConTime.Screens
{
    public partial class PlanoCon : Form
    {
        public PlanoCon()
        {
            InitializeComponent();
        }
        private void btnDrop_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Control header = btn.Parent;
            Control guia = btn.Parent.Parent;
            Control mainbody = btn.Parent.Parent.Parent;
            int minsize = guia.Height - (header.Height + 5);
            int maxsize = 2 * (header.Height + 5) + 2;

            if (btn.Text == "˄")
            {
                guia.Height -= minsize;
                btn.Text = "˅";
                Reposition(mainbody, guia.TabIndex, -minsize);
                if (btn.Tag == "child")
                {
                    mainbody.Height -= 150;
                    Reposition(mainbody.Parent, mainbody.TabIndex, -150);
                }
                else
                {
                    foreach (Panel pnl in guia.Controls.OfType<Panel>())
                    {
                        foreach (Panel cnt in pnl.Controls.OfType<Panel>())
                        {
                            foreach (Button child_btn in cnt.Controls.OfType<Button>())
                            {
                                if (child_btn.Text == "˄")
                                {
                                    int childminsize = pnl.Height - (cnt.Height + 5);
                                    child_btn.Text = "˅";
                                    pnl.Height -= childminsize;
                                    Reposition(pnl.Parent, pnl.TabIndex, -childminsize);
                                }
                            }
                        }
                    }
                }
            }
            else if (btn.Text == "˅")
            {
                guia.Height += maxsize;
                btn.Text = "˄";
                Reposition(mainbody, guia.TabIndex, maxsize);
                if (btn.Tag == "child")
                {
                    mainbody.Height += 150;
                    Reposition(mainbody.Parent, mainbody.TabIndex, 150);
                }
            }
        }

        private void Reposition(Control parent, int tabIndex, int p)
        {
            foreach (Panel panel in parent.Controls.OfType<Panel>())
            {
                if (tabIndex < panel.TabIndex)
                {
                    panel.Top += p;
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
