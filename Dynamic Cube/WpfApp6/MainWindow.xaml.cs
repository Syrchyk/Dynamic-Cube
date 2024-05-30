using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawCube((int)(this.Width / 2), 100);
        }


        void Update()
        {
            canvas.Children.Clear();
            DrawCube((int)(this.Width / 2), 100);
        }

        double angle = 0;
        double angleUp = 0;
        
        void ctxDrawLine(bool isDraw, bool vert, bool isReverse, int lenght, Point point)
        {
            Line line;
            if (!vert)
            {
                line = new Line()
                {
                    X1 = point.X - lenght * Math.Cos(angle),
                    Y1 = point.Y - lenght * Math.Sin(-angleUp) * Math.Sin(angle),
                    X2 = point.X + lenght * Math.Cos(angle),
                    Y2 = point.Y + lenght * Math.Sin(-angleUp) * Math.Sin(angle),
                };
            }
            else
            {
                if(isReverse)
                {
                    line = new Line()
                    {
                        X1 = point.X,
                        Y1 = point.Y - Math.Sqrt(Math.Pow(lenght, 2) / 2) * Math.Cos(angleUp),
                        X2 = point.X,
                        Y2 = point.Y + Math.Sqrt(Math.Pow(lenght, 2) / 2) * Math.Cos(angleUp),
                    };
                }
                else
                {
                    line = new Line()
                    {
                        X1 = point.X - lenght * Math.Sin(-angle),
                        Y1 = point.Y - lenght * Math.Sin(-angleUp) * Math.Cos(-angle),
                        X2 = point.X + lenght * Math.Sin(-angle),
                        Y2 = point.Y + lenght * Math.Sin(-angleUp) * Math.Cos(-angle),
                    };
                }

            }
            if(isDraw)
            {
                line.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 255));
            }
            canvas.Children.Add(line);
        }
        
        void DrawCubeDown(Line line1, Line line2)
        {
            canvas.Children.Add(new Line()
            {
                X1 = line1.X1,
                X2 = line2.X2,
                Y1 = line1.Y1,
                Y2 = line2.Y2,
                Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 255))
            });
            canvas.Children.Add(new Line()
            {
                X1 = line2.X2,
                X2 = line1.X2,
                Y1 = line2.Y2,
                Y2 = line1.Y2,
                Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 255))
            });
            canvas.Children.Add(new Line()
            {
                X1 = line1.X2,
                X2 = line2.X1,
                Y1 = line1.Y2,
                Y2 = line2.Y1,
                Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 255))
            });
            canvas.Children.Add(new Line()
            {
                X1 = line2.X1,
                X2 = line1.X1,
                Y1 = line2.Y1,
                Y2 = line1.Y1,
                Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 255))
            });
        }
        void DrawCubeSide(Line line1, Line line2)
        {
            canvas.Children.Add(new Line()
            {
                X1 = line1.X1,
                X2 = line2.X1,
                Y1 = line1.Y1,
                Y2 = line2.Y1,
                Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 255))
            });
            canvas.Children.Add(new Line()
            {
                X1 = line2.X2,
                X2 = line1.X2,
                Y1 = line2.Y2,
                Y2 = line1.Y2,
                Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 255))
            });
        }

        void DrawCube(int center, int lenght)
        {
            ctxDrawLine(false, true, true, lenght, new Point(center, center));
            ctxDrawLine(false, false, false, lenght, new Point((canvas.Children[0] as Line).X1, (canvas.Children[0] as Line).Y1));
            ctxDrawLine(false, true, false, lenght, new Point((canvas.Children[0] as Line).X1, (canvas.Children[0] as Line).Y1));
            DrawCubeDown(canvas.Children[1] as Line, canvas.Children[2] as Line);
            ctxDrawLine(false, false, false, lenght, new Point((canvas.Children[0] as Line).X2, (canvas.Children[0] as Line).Y2));
            ctxDrawLine(false, true, false, lenght, new Point((canvas.Children[0] as Line).X2, (canvas.Children[0] as Line).Y2));
            DrawCubeDown(canvas.Children[7] as Line, canvas.Children[8] as Line);
            DrawCubeSide(canvas.Children[1] as Line, canvas.Children[7] as Line);
            DrawCubeSide(canvas.Children[2] as Line, canvas.Children[8] as Line);
        }


        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            angle = e.GetPosition(this).X / 100;
            angleUp = -e.GetPosition(this).Y / 100;
            Update();
        }
    }
}
