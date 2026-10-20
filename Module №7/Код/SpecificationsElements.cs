using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace энциклопедияПоГайдамЮтюб
{
    public partial class SpecificationsElements : UserControl
    {
        public SpecificationsElements()
        {
            InitializeComponent();
        }

        private string _Wingspan;
        private string _Length;
        private string _HeightLbl;
        private string _WeightLbl;
        private string _FuelLbl;
        private string _engineTypeLb;
        private string _maximumSpeedLbl;
        private string _cruisingSpeedLbel;
        private string _crewPeopleLbl;
        private string _serviceLifeLbl;
        private string _unitCostLbl;
        private string _ArmamentLbl;
        private string _TipefuelLbl;

       [Category("Custom Props")]
        public string Wingspan
        {
            get { return _Wingspan; }
            set
            {
                _Wingspan = value;
                if (WingspanLbl != null)
                    WingspanLbl.Text = value;
            }
        }

        [Category("Custom Props")]
        public string Length
        {
            get { return _Length; }
            set
            {
                _Length = value;
                if (LengthLbl != null)
                    LengthLbl.Text = value;
            }
        }

        [Category("Custom Props")]
        public string HeightSpec
        {
            get { return _HeightLbl; }
            set
            {
                _HeightLbl = value;
                if (Heightds != null)
                    Heightds.Text = value;
            }
        }

        [Category("Custom Props")]
        public string Weight
        {
            get { return _WeightLbl; }
            set
            {
                _WeightLbl = value;
                if (WeightLbl != null)
                    WeightLbl.Text = value;
            }
        }

        [Category("Custom Props")]
        public string Fuel
        {
            get { return _FuelLbl; }
            set
            {
                _FuelLbl = value;
                if (FuelLbl != null)
                    FuelLbl.Text = value;
            }
        }

        [Category("Custom Props")]
        public string engineType
        {
            get { return _engineTypeLb; }
            set
            {
                _engineTypeLb = value;
                if (engineTypeLbl != null)
                    engineTypeLbl.Text = value;
            }
        }

        [Category("Custom Props")]
        public string maximumSpeed
        {
            get { return _maximumSpeedLbl; }
            set
            {
                _maximumSpeedLbl = value;
                if (maximumSpeedLbl != null)
                    maximumSpeedLbl.Text = value;
            }
        }

        [Category("Custom Props")]
        public string cruisingSpeed
        {
            get { return _cruisingSpeedLbel; }
            set
            {
                _cruisingSpeedLbel = value;
                if (cruisingSpeedLbel != null)
                    cruisingSpeedLbel.Text = value;
            }
        }

        [Category("Custom Props")]
        public string crewPeople
        {
            get { return _crewPeopleLbl; }
            set
            {
                _crewPeopleLbl = value;
                if (crewPeopleLbl != null)
                    crewPeopleLbl.Text = value;
            }
        }

        [Category("Custom Props")]
        public string serviceLife
        {
            get { return _serviceLifeLbl; }
            set
            {
                _serviceLifeLbl = value;
                if (serviceLifeLbl != null)
                    serviceLifeLbl.Text = value;
            }
        }

        [Category("Custom Props")]
        public string unitCost
        {
            get { return _unitCostLbl; ; }
            set
            {
                _unitCostLbl = value;
                if (unitCostLbl != null)
                    unitCostLbl.Text = value;
            }
        }

        [Category("Custom Props")]
        public string Armament
        {
            get { return _ArmamentLbl;}
            set
            {
                _ArmamentLbl = value;
                if (ArmamentLbl != null)
                    ArmamentLbl.Text = value;
            }
        }

        [Category("Custom Props")]
        public string Tipefuel
        {
            get { return _TipefuelLbl; }
            set
            {
                _TipefuelLbl = value;
                if (TipefuelLbl != null)
                    TipefuelLbl.Text = value;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
