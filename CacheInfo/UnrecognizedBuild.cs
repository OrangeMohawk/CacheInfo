using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CacheInfo;

namespace BadBuild
{
    public partial class UnrecognizedBuild : Form
    {
        CacheInfo.MainForm CI;


        public UnrecognizedBuild(string _build, CacheInfo.MainForm info)
        {
            InitializeComponent();
            CI = info;
            ChangeLabelText(_build);
        }

        public void ChangeLabelText(string _build)
        {
            labeltext.Text = "The build " + _build + " is not recognized, but the map appears to be third generation. You can add support in the source code.";
        }

        private void loadNoneButton_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        private void loadH3BButton_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
            CI.H3BForceLoad();
        }

        private void loadH3Button_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
            CI.H3ForceLoad();
        }

        private void loadODSTButton_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
            CI.ODSTForceLoad();
        }

        private void loadReachBetaButton_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
            CI.ReachBetaForceLoad();
        }

        private void loadReachButton_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
            CI.ReachForceLoad();
        }

        private void loadH4Button_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
            CI.H4StandardForceLoad();
        }

        private void loadH4NoRawButton_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
            CI.H4NoRawForceLoad();
        }

    }
}
