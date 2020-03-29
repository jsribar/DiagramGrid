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
            using (Pen majorTicPen = new Pen(MajorTicColor))
            using (Pen minorTicPen = new Pen(MinorTicColor))
            {
                int deltaYMajor = GetMajorYTicsDelta();
                int nYMinor = deltaYMajor / minDelta;
                int deltaY = nYMinor == 0 ? deltaYMajor : deltaYMajor / nYMinor;
                int nY = (Height - 1) / deltaY;
                for (int i = 0, y = Height - 1; i <= nY; ++i)
                {
                    if (nYMinor != 0 && i % nYMinor != 0)
                        g.DrawLine(minorTicPen, 0, y, Width, y);
                    else
                        g.DrawLine(majorTicPen, 0, y, Width, y);
                    y -= deltaY;
                }

                int deltaXMajor = GetMajorXTicsDelta();
                int nXMinor = deltaXMajor / minDelta;
                int deltaX = nXMinor == 0 ? deltaXMajor : deltaXMajor / nXMinor;
                int nX = (Width - 1) / deltaX;
                for (int i = 0, x = 0; i <= nX; ++i)
                {
                    if (nXMinor != 0 && i % nXMinor != 0)
                        g.DrawLine(minorTicPen, x, 0, x, Height - 1);
                    else
                        g.DrawLine(majorTicPen, x, 0, x, Height - 1);
                    x += deltaX;
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

        [Category("Custom")]
        [Browsable(true)]
        [Description("Sets or gets the number of major tics count on x-axis")]
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
        [Description("Sets or gets the number of major tics count on y-axis")]
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
        [Description("Sets or gets the minimal distance between grid lines"), DefaultValue(20)]
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
        [Description("Sets or gets the color of major tics line")]
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
        [Description("Sets or gets the color of minor tics line")]
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

        private int minDelta = 20;
        private int majorXTicsCount = 3;
        private int majorYTicsCount = 2;
        private Color majorTicColor = Color.LightSlateGray;
        private Color minorTicColor = Color.LightGray;
    }
}
