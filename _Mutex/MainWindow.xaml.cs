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
        public static bool precessing = true;
        public static bool suspended = false;

        public static Mutex mtx = new Mutex();
        public void Change(Ellipse el, MainWindow cntrl)
        {
            cntrl.Dispatcher.Invoke(DispatcherPriority.Normal, cntrl.myDelegate, el);
            Thread.Sleep(1000);
        }

    }

    public class ChangeRedYellow
    {
        public Thread Thrd;
        public MainWindow main;
        ChangeLight light;

        public ChangeRedYellow(MainWindow mn, ChangeLight chgLight)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "RYThread";
            main = mn;
            light = chgLight;
        }

        void Run(object obj)
        {
            while (true)
            {
                ChangeLight.mtx.WaitOne();
                if (ChangeLight.precessing)
                {
                    light.Change((Ellipse)obj, main);
                    ChangeLight.mtx.ReleaseMutex();
                    //ChangeLight.mtx.WaitOne();
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
        ChangeLight light;

        public ChangeYellowGreen(MainWindow mn, ChangeLight chgLight)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "YGThread";
            main = mn;
            light = chgLight;
        }

        void Run(object obj)
        {
            while (true)
            {
                ChangeLight.mtx.WaitOne();
                if (ChangeLight.precessing)
                {
                    light.Change((Ellipse)obj, main);
                    ChangeLight.mtx.ReleaseMutex();
                    //ChangeLight.mtx.WaitOne();
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
        ChangeLight light;

        public ChangeGreenYellow(MainWindow mn, ChangeLight chgLight)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "GYThread";
            main = mn;
            light = chgLight;

        }

        void Run(object obj)
        {
            while (true)
            {
                ChangeLight.mtx.WaitOne();
                if (ChangeLight.precessing)
                {
                    light.Change((Ellipse)obj, main);
                    ChangeLight.mtx.ReleaseMutex();
                    //ChangeLight.mtx.WaitOne();
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
        ChangeLight light;

        public ChangeYellowRed(MainWindow mn, ChangeLight chgLight)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "YRThread";
            main = mn;
            light = chgLight;
            //Thrd.Start();

        }

        void Run(object obj)
        {
            while (true)
            {
                ChangeLight.mtx.WaitOne();
                if (ChangeLight.precessing)
                {
                    light.Change((Ellipse)obj, main);
                    ChangeLight.mtx.ReleaseMutex();
                    //ChangeLight.mtx.WaitOne();
                }
                else
                    break;
            }
        }
    }

    public partial class MainWindow : Window
    {
        public delegate void ChangeColor(object ell);
        public ChangeColor myDelegate;

        public ChangeRedYellow RY;
        public ChangeYellowGreen YG;
        public ChangeGreenYellow GY;
        public ChangeYellowRed YR;

        Brush def;
        ChangeLight changeLight;

        public MainWindow()
        {
            InitializeComponent();
            def = Green.Fill;

            changeLight = new ChangeLight();

            RY = new ChangeRedYellow(this, changeLight);
            YG = new ChangeYellowGreen(this, changeLight);
            GY = new ChangeGreenYellow(this, changeLight);
            YR = new ChangeYellowRed(this, changeLight);

            myDelegate = new ChangeColor(ChangeColorMethod);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessUnstarted())
            {
                ChangeLight.precessing = true;

                RY.Thrd.Start(Yellow);
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

                    RY = new ChangeRedYellow(this, changeLight);
                    YG = new ChangeYellowGreen(this, changeLight);
                    GY = new ChangeGreenYellow(this, changeLight);
                    YR = new ChangeYellowRed(this, changeLight);

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

        //public void ChangeToRedMethod()
        //{
        //    //Ellipse eli = (Ellipse)el;
        //    Console.WriteLine("dsada");
        //    //Console.WriteLine(eli.Name);
        //    Yellow.Fill = def;
        //    Red.Fill = Brushes.Red;
        //    Green.Fill = def;
        //}

        //public void ChangeToGreenMethod()
        //{
        //    //Ellipse eli = (Ellipse)el;
        //    Console.WriteLine("dsada");
        //    //Console.WriteLine(eli.Name);
        //    Yellow.Fill = def;
        //    Red.Fill = def;
        //    Green.Fill = Brushes.Green;
        //}
    }
}