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

        static object obj = new object();

        public void ChangeIt(MainWindow cntrl)
        {
            lock (obj)
            {
                //Console.WriteLine(Thread.CurrentThread.Name);

                //Dispatcher disp;
                //object[] objs = { el };
                if (Thread.CurrentThread.Name == "RYThread" || Thread.CurrentThread.Name == "GYThread")
                    //Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, cntrl.myDelegateYellow);
                    cntrl.Dispatcher.Invoke(DispatcherPriority.Normal, cntrl.myDelegateYellow);
                if (Thread.CurrentThread.Name == "YRThread")
                    //Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, cntrl.myDelegateRed);
                    cntrl.Dispatcher.Invoke(DispatcherPriority.Normal, cntrl.myDelegateRed);
                if (Thread.CurrentThread.Name == "YGThread")
                    //Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, cntrl.myDelegateGreen);
                    cntrl.Dispatcher.Invoke(DispatcherPriority.Normal, cntrl.myDelegateGreen);
                //disp.Invoke(cntrl.myDelegate, el);
                //cntrl.Invoke(cntrl.myDelegate);

                //Console.WriteLine(el.Name);
                Thread.Sleep(1000);
                //Thread.CurrentThread.Join(1000);
            }
        }
    }

    public class ChangeRedYellow
    {
        public Thread Thrd;
        public MainWindow main;
        ChangeLight light;
        //static ChangeLight chgLight = new ChangeLight();

        public ChangeRedYellow(MainWindow mn)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "RYThread";
            main = mn;
            light = new ChangeLight();

            //Thrd.Start();

        }

        void Run()
        {
            while (true)
            {
                if (ChangeLight.precessing)
                {
                    //lock (chgLight)
                    //{
                    //Thread.Sleep(1000);
                    //Console.WriteLine(ellipse.Name);
                    light.ChangeIt(main);
                    //Thread.Sleep(1000);
                    //}

                }
                else //{ Thread.Sleep(0); 
                    break; //}
            }
            //Console.WriteLine("stop");
        }
    }

    public class ChangeYellowGreen
    {
        public Thread Thrd;
        public MainWindow main;
        ChangeLight light;
        //static ChangeLight chgLight = new ChangeLight();

        public ChangeYellowGreen(MainWindow mn)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "YGThread";
            main = mn;
            light = new ChangeLight();
            //Thrd.Start();

        }

        void Run()
        {
            while (true)
            {
                if (ChangeLight.precessing)
                {
                    //lock (chgLight)
                    //{
                    //Thread.Sleep(1000);
                    //Console.WriteLine("YG");
                    light.ChangeIt(main);
                    //Thread.Sleep(1000);
                    //}
                }
                else //{ Thread.Sleep(0); 
                    break; //}
            }
        }
    }

    public class ChangeGreenYellow
    {
        public Thread Thrd;
        public MainWindow main;
        ChangeLight light;
        //static ChangeLight chgLight = new ChangeLight();

        public ChangeGreenYellow(MainWindow mn)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "GYThread";
            main = mn;
            light = new ChangeLight();
            //Thrd.Start();

        }

        void Run()
        {
            while (true)
            {
                if (ChangeLight.precessing)
                {
                    //lock (chgLight)
                    //{
                    //Thread.Sleep(1000);
                    //Console.WriteLine("GY");
                    light.ChangeIt(main);
                    //Thread.Sleep(1000);
                    //}
                }
                else //{Thread.Sleep(0); 
                    break; //}
            }
        }
    }

    public class ChangeYellowRed
    {
        public Thread Thrd;
        public MainWindow main;
        ChangeLight light;
        //static ChangeLight chgLight = new ChangeLight();

        public ChangeYellowRed(MainWindow mn)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "YRThread";
            main = mn;
            light = new ChangeLight();
            //Thrd.Start();

        }

        void Run()
        {
            while (true)
            {
                if (ChangeLight.precessing)
                {
                    //lock (chgLight)
                    //{
                    //Thread.Sleep(1000);
                    //Console.WriteLine("YR");
                    light.ChangeIt(main);
                    //Thread.Sleep(1000);
                    //}
                }
                else //{Thread.Sleep(0); 
                    break; //}
            }
        }
    }

    public partial class MainWindow : Window
    {
        public delegate void ChangeColor();
        public ChangeColor myDelegateYellow;
        public ChangeColor myDelegateRed;
        public ChangeColor myDelegateGreen;

        public ChangeRedYellow RY;
        public ChangeYellowGreen YG;
        public ChangeGreenYellow GY;
        public ChangeYellowRed YR;
        Brush def;

        public MainWindow()
        {
            InitializeComponent();
            def = Green.Fill;

            RY = new ChangeRedYellow(this);
            YG = new ChangeYellowGreen(this);
            GY = new ChangeGreenYellow(this);
            YR = new ChangeYellowRed(this);

            myDelegateYellow = new ChangeColor(ChangeToYellowMethod);
            myDelegateRed = new ChangeColor(ChangeToRedMethod);
            myDelegateGreen = new ChangeColor(ChangeToGreenMethod);
            //Control f = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessUnstarted())
            {
                RY.Thrd.Start();
                //RY.Thrd.Join();
                YG.Thrd.Start();
                GY.Thrd.Start();
                YR.Thrd.Start();

                ChangeLight.precessing = true;
            }
            else
            {
                if (ProcessStopped())
                {

                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("stopBtn");
            //if (ProcessRunning())
            //{
            ChangeLight.precessing = false;
            //}
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // (ProcessRunning())
            //{
            ChangeLight.precessing = false;
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

        public void ChangeToYellowMethod()
        {
            //Ellipse eli = (Ellipse)el;
            //Console.WriteLine("ChangeToYellowMethod");
            //Console.WriteLine(eli.Name);

            Yellow.Fill = Brushes.Yellow;
            Red.Fill = def;
            Green.Fill = def;
        }

        public void ChangeToRedMethod()
        {
            //Ellipse eli = (Ellipse)el;
            //Console.WriteLine("ChangeToRedMethod");
            //Console.WriteLine(eli.Name);

            Yellow.Fill = def;
            Red.Fill = Brushes.Red;
            Green.Fill = def;
        }

        public void ChangeToGreenMethod()
        {
            //Ellipse eli = (Ellipse)el;
            //Console.WriteLine("ChangeToGreenMethod");
            //Console.WriteLine(eli.Name);

            Yellow.Fill = def;
            Red.Fill = def;
            Green.Fill = Brushes.Green;
        }
    }
}