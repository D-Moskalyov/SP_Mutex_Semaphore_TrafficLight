using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace Lock
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public class ChangeLight
    {
        public static bool precessing = false;
        public static bool suspended = false;

        static object obj = new object();

        public void ChangeIt(Ellipse el, MainWindow cntrl)
        {
            lock (obj)
            {
                object[] objs = { el };
                if(precessing)
                    cntrl.Dispatcher.Invoke(cntrl.myDelegate, objs);
                Thread.Sleep(1000);
            }
        }
    }

    public class ChangeRedYellow
    {
        public Thread Thrd;
        public MainWindow main;
        //public Ellipse ellipse;
        ChangeLight changeLight;

        public ChangeRedYellow(MainWindow mn)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "RYThread";
            main = mn;
            //ellipse = el;
            changeLight = new ChangeLight();

        }

        void Run(object ell)
        {
            while (true)
            {
                if (ChangeLight.precessing)
                {
                    changeLight.ChangeIt((Ellipse)ell, main);
                }
                else
                    break;
            }
        }
    }

    public class ChangeYellowGreen
    {
        public Thread Thrd;
        public MainWindow main;
        //public Ellipse ellipse;
        ChangeLight changeLight;

        public ChangeYellowGreen(MainWindow mn)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "YGThread";
            main = mn;
            //ellipse = el;
            changeLight = new ChangeLight();

        }

        void Run(object ell)
        {
            while (true)
            {
                if (ChangeLight.precessing)
                {
                    changeLight.ChangeIt((Ellipse)ell, main);

                }
                else 
                    break; 
            }
        }
    }

    public class ChangeGreenYellow
    {
        public Thread Thrd;
        public MainWindow main;
        //public Ellipse ellipse;
        ChangeLight changeLight;

        public ChangeGreenYellow(MainWindow mn)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "GYThread";
            main = mn;
            //ellipse = el;
            changeLight = new ChangeLight();

        }

        void Run(object ell)
        {
            while (true)
            {
                if (ChangeLight.precessing)
                {
                    changeLight.ChangeIt((Ellipse)ell, main);

                }
                else  
                    break; 
            }
        }
    }

    public class ChangeYellowRed
    {
        public Thread Thrd;
        public MainWindow main;
        //public Ellipse ellipse;
        ChangeLight changeLight;

        public ChangeYellowRed(MainWindow mn)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "YRThread";
            main = mn;
            changeLight = new ChangeLight();
            //ellipse = el;

        }

        void Run(object ell)
        {
            while (true)
            {
                if (ChangeLight.precessing)
                {
                    changeLight.ChangeIt((Ellipse)ell, main);
                }
                else 
                    break; 
            }
        }
    }

    public partial class MainWindow : Window
    {
        public delegate void ChangeColor(object el);
        public ChangeColor myDelegate;
        Brush def;

        public ChangeRedYellow RY;
        public ChangeYellowGreen YG;
        public ChangeGreenYellow GY;
        public ChangeYellowRed YR;

        public MainWindow()
        {
            InitializeComponent();
            def = Green.Fill;

            RY = new ChangeRedYellow(this);
            YG = new ChangeYellowGreen(this);
            GY = new ChangeGreenYellow(this);
            YR = new ChangeYellowRed(this);

            myDelegate = new ChangeColor(ChangeColorMethod);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessUnstarted())
            {
                ChangeLight.precessing = true;

                //Console.WriteLine("start");
                RY.Thrd.Start(Yellow);
                //RY.Thrd.Join();
                Thread.Sleep(10);
                YG.Thrd.Start(Green);
                Thread.Sleep(10);
                GY.Thrd.Start(Yellow);
                Thread.Sleep(10);
                YR.Thrd.Start(Red);
                
            }
            else
            {
                if (!ProcessRunning())
                {
                    ChangeLight.precessing = true;

                    RY.Thrd.Abort();
                    YG.Thrd.Abort();
                    GY.Thrd.Abort();
                    YR.Thrd.Abort();

                    RY = new ChangeRedYellow(this);
                    YG = new ChangeYellowGreen(this);
                    GY = new ChangeGreenYellow(this);
                    YR = new ChangeYellowRed(this);

                    RY.Thrd.Start(Yellow);
                    YG.Thrd.Start(Green);
                    GY.Thrd.Start(Yellow);
                    YR.Thrd.Start(Red);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("stopBtn");
            //if (ProcessRunning())
            //{
            ChangeLight.precessing = false;
            //RY.Thrd.Suspend();
            //YG.Thrd.Suspend();
            //GY.Thrd.Suspend();
            //YR.Thrd.Suspend();
            
            //}
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // (ProcessRunning())
            //{
            ChangeLight.precessing = false;
            RY.Thrd.Abort();
            YG.Thrd.Abort();
            GY.Thrd.Abort();
            YR.Thrd.Abort();
            //
        }

        bool ProcessUnstarted()
        {
            if (RY.Thrd.ThreadState == ThreadState.Unstarted || YG.Thrd.ThreadState == ThreadState.Unstarted || GY.Thrd.ThreadState == ThreadState.Unstarted || YR.Thrd.ThreadState == ThreadState.Unstarted)
                return true;
            return false;
        }

        bool ProcessStopped()
        {
            if (RY.Thrd.ThreadState == ThreadState.Stopped || YG.Thrd.ThreadState == ThreadState.Stopped || GY.Thrd.ThreadState == ThreadState.Stopped || YR.Thrd.ThreadState == ThreadState.Stopped)
                return true;
            return false;
        }

        bool ProcessRunning()
        {
            if (RY.Thrd.ThreadState == ThreadState.Running || YG.Thrd.ThreadState == ThreadState.Running || GY.Thrd.ThreadState == ThreadState.Running || YR.Thrd.ThreadState == ThreadState.Running)
                return true;
            return false;
        }

        public void ChangeColorMethod(object el)
        {
            switch (((Ellipse)el).Name)
            {
                case "Yellow":
                    {
                        Yellow.Fill = Brushes.Yellow;
                        Red.Fill = def;
                        Green.Fill = def;
                        break;
                    }
                case "Green":
                    {
                        Yellow.Fill = def;
                        Red.Fill = def;
                        Green.Fill = Brushes.Green; ;
                        break;
                    }
                case "Red":
                    {
                        Yellow.Fill = def;
                        Red.Fill = Brushes.Red; ;
                        Green.Fill = def;
                        break;
                    }
                default: break;

                    
            }
            //Ellipse eli = (Ellipse)el;
            Console.WriteLine(((Ellipse)el).Name);
            //Console.WriteLine(eli.Name);
        }
    }
}