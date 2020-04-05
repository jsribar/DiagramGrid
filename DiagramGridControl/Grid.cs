using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiagramGrid.Controls
{
    public partial class Grid : Control, INotifyPropertyChanged
    {
        public Grid()
        {
            InitializeComponent();
            BackColor = SystemColors.Window;
            MajorTicColor = Color.LightGray;
            MinorTicColor = Color.WhiteSmoke;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string PropertyName)
        {
            Invalidate();
            Parent?.Invalidate(this.Bounds, true);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            DrawGrid(pe.Graphics);
        }

        private void DrawGrid(Graphics g)
        {
            int deltaY = GetMajorYTicsDelta();
            int nYMinor = GetMinorTicsCount(deltaY);
            if (nYMinor != 0)
                deltaY /= nYMinor;
            int minorYTicsCount = (Height - 1) / deltaY;
            int yMin = Height - 1 - minorYTicsCount * deltaY;

            int deltaX = GetMajorXTicsDelta();
            int nXMinor = GetMinorTicsCount(deltaX);
            if (nXMinor != 0)
                deltaX /= nXMinor;
            int minorXTicsCount = (Width - 1) / deltaX;
            int xMax = majorXTicsCount * deltaX;
            if (nXMinor != 0)
                xMax *= nXMinor;

            using (Pen minorTicPen = new Pen(MinorTicColor))
            {
                if (nYMinor != 0)
                {
                    for (int y = Height - 1, i = 0; y >= yMin; y -= deltaY, ++i)
                    {
                        if (i % minorYTicsCount != 0)
                            g.DrawLine(minorTicPen, 0, y, xMax, y);
                    }
                }

                if (nXMinor != 0)
                {
                    for (int x = 0, i = 0; x <= xMax; x += deltaX, ++i)
                    {
                        if (i % minorXTicsCount != 0)
                            g.DrawLine(minorTicPen, x, yMin, x, Height - 1);
                    }
                }
            }

            if (nYMinor != 0)
                deltaY *= nYMinor;
            if (nXMinor != 0)
                deltaX *= nXMinor;
            using (Pen majorTicPen = new Pen(MajorTicColor))
            {
                for (int y = Height - 1; y >= 0; y -= deltaY)
                {
                    g.DrawLine(majorTicPen, 0, y, xMax, y);
                }

                for (int x = 0; x <= Width; x += deltaX)
                {
                    g.DrawLine(majorTicPen, x, yMin, x, Height - 1);
                }
            }
        }

        private int GetMajorXTicsDelta()
        {
            return (Width - 1) / majorXTicsCount;
        }

        private int GetMajorYTicsDelta()
        {
            return (Height - 1) / majorYTicsCount;
        }

        private int GetMinorTicsCount(int deltaMajor)
        {
            int nMinor = deltaMajor / minDelta;
            if (nMinor == 0)
                return nMinor;
            return minorTicsCount.Find(x => x <= nMinor);
        }

        [Category("Custom")]
        [Browsable(true)]
        [Description("Sets or gets the number of major tics on x-axis.")]
        public int MajorXTicsCount
        {
            get => majorXTicsCount;
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException(nameof(MajorXTicsCount) + " must be > 0");
                if (majorXTicsCount != value)
                {
                    majorXTicsCount = value;
                    NotifyPropertyChanged(nameof(MajorXTicsCount));
                }
            }
        }

        [Category("Custom")]
        [Browsable(true)]
        [Description("Sets or gets the number of major tics on y-axis.")]
        public int MajorYTicsCount
        {
            get => majorYTicsCount;
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException(nameof(MajorYTicsCount) + " must be > 0");
                if (majorYTicsCount != value)
                {
                    majorYTicsCount = value;
                    NotifyPropertyChanged(nameof(MajorYTicsCount));
                }
            }
        }

        [Category("Custom")]
        [Browsable(true)]
        [Description("Sets or gets the minimal distance between grid lines."), DefaultValue(20)]
        public int MinDelta 
        {
            get => minDelta;
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException(nameof(MinDelta) + " must be > 0");
                if (minDelta != value)
                {
                    minDelta = value;
                    NotifyPropertyChanged(nameof(MinDelta));
                }
            }
        }

        [Category("Custom")]
        [Browsable(true)]
        [Description("Sets or gets the color of major tics line.")]
        [DefaultValue(typeof(Color), "LightGray")]
        public Color MajorTicColor
        {
            get => majorTicColor;
            set
            {
                if (majorTicColor != value)
                {
                    majorTicColor = value;
                    NotifyPropertyChanged(nameof(MajorTicColor));
                }
            }
        }

        [Category("Custom")]
        [Browsable(true)]
        [Description("Sets or gets the color of minor tics line.")]
        [DefaultValue(typeof(Color), "WhiteSmoke")]
        public Color MinorTicColor
        {
            get => minorTicColor;
            set
            {
                if (minorTicColor != value)
                {
                    minorTicColor = value;
                    NotifyPropertyChanged(nameof(MinorTicColor));
                }
            }
        }

        [DefaultValue(typeof(Color), "Window")]
        public new Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
            }
        }

        [Category("Custom")]
        [Browsable(true)]
        [Description("Sets or gets the possible number of minor tics between two major tics.")]
        [DefaultValue(typeof(Color), "WhiteSmoke")]
        public List<int> MinorTicsNumber
        {
            get => minorTicsCount;
            set
            {
                minorTicsCount = new DiagramGridControl.SortedList<int>(value, (x, y) => y - x);
                NotifyPropertyChanged(nameof(MinorTicsNumber));
            }
        }

        private int minDelta = 20;
        private int majorXTicsCount = 3;
        private int majorYTicsCount = 2;
        private Color majorTicColor = Color.LightSlateGray;
        private Color minorTicColor = Color.LightGray;
        private DiagramGridControl.SortedList<int> minorTicsCount = new DiagramGridControl.SortedList<int>(new List<int>{ 2, 4, 5, 10 }, (x, y) => y - x);
    }
}
