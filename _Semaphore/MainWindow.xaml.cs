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

        public static Semaphore semaphore = new Semaphore(1, 1);
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
                ChangeLight.semaphore.WaitOne();
                if (ChangeLight.precessing)
                {
                    light.Change((Ellipse)obj, main);
                    ChangeLight.semaphore.Release();
                }
                else
                {
                    ChangeLight.semaphore.Release();
                    break;
                }
            }
        }

        public void Res(object obj)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "RYThread";
            Thrd.Start(obj);
            Thread.Sleep(10);
            Console.WriteLine(Thrd.Name + Thrd.ThreadState.ToString());
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
                ChangeLight.semaphore.WaitOne();
                if (ChangeLight.precessing)
                {
                    light.Change((Ellipse)obj, main);
                    ChangeLight.semaphore.Release();
                }
                else
                {
                    ChangeLight.semaphore.Release();
                    break;
                }
            }
        }

        public void Res(object obj)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "YGThread";
            Thrd.Start(obj);
            Thread.Sleep(10);
            Console.WriteLine(Thrd.Name + Thrd.ThreadState.ToString());
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
                ChangeLight.semaphore.WaitOne();
                if (ChangeLight.precessing)
                {
                    light.Change((Ellipse)obj, main);
                    ChangeLight.semaphore.Release();
                }
                else
                {
                    ChangeLight.semaphore.Release();
                    break;
                }
            }
        }

        public void Res(object obj)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "GYThread";
            Thrd.Start(obj);
            Thread.Sleep(10);
            Console.WriteLine(Thrd.Name + Thrd.ThreadState.ToString());
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
                ChangeLight.semaphore.WaitOne();
                if (ChangeLight.precessing)
                {
                    light.Change((Ellipse)obj, main);
                    ChangeLight.semaphore.Release();
                }
                else
                {
                    ChangeLight.semaphore.Release();
                    break;
                }
            }
        }

        public void Res(object obj)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = "YRThread";
            Thrd.Start(obj);
            Thread.Sleep(10);
            Console.WriteLine(Thrd.Name + Thrd.ThreadState.ToString());
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
        int numColor;

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
            if (!ChangeLight.precessing)
            {
                if (ProcessUnstarted())
                {
                    ChangeLight.precessing = true;

                    Yellow.Tag = "up";
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
                    ChangeLight.precessing = true;
                    switch (numColor)
                    {
                        case 1:
                            {
                                Console.WriteLine("1");
                                RY.Res(Yellow);
                                //Thread.Sleep(10);
                                YG.Res(Green);
                                //Thread.Sleep(10);
                                GY.Res(Yellow);
                                //Thread.Sleep(10);
                                YR.Res(Red);
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("2");
                                YR.Res(Red);
                                //Thread.Sleep(10);
                                RY.Res(Yellow);
                                //Thread.Sleep(10);
                                YG.Res(Green);
                                //Thread.Sleep(10);
                                GY.Res(Yellow);
                                break;
                            }
                        case 22:
                            {
                                Console.WriteLine("22");
                                YG.Res(Green);
                                //Thread.Sleep(10);
                                GY.Res(Yellow);
                                //Thread.Sleep(10);
                                YR.Res(Red);
                                //Thread.Sleep(10);
                                RY.Res(Yellow);
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("3");
                                GY.Res(Yellow);
                                //Thread.Sleep(10);
                                YR.Res(Red);
                                //Thread.Sleep(10);
                                RY.Res(Yellow);
                                //Thread.Sleep(10);
                                YG.Res(Green);
                                break;
                            }
                        default: break;
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ChangeLight.precessing)
            {
                if (Red.Fill == Brushes.Red) numColor = 1;
                if (Yellow.Fill == Brushes.Yellow && (string)(Yellow.Tag) == "up") numColor = 2;
                if (Yellow.Fill == Brushes.Yellow && (string)(Yellow.Tag) == "down") numColor = 22;
                if (Green.Fill == Brushes.Green) numColor = 3;

                ChangeLight.precessing = false;

                ChangeLight.semaphore.WaitOne();
                Console.WriteLine("stopBtn");
                ChangeLight.semaphore.Release();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ChangeLight.precessing = false;
            RY.Thrd.Abort();
            YG.Thrd.Abort();
            GY.Thrd.Abort();
            YR.Thrd.Abort();
        }

        bool ProcessUnstarted()
        {
            if (RY.Thrd.ThreadState == ThreadState.Unstarted || YG.Thrd.ThreadState == ThreadState.Unstarted || GY.Thrd.ThreadState == ThreadState.Unstarted || YR.Thrd.ThreadState == ThreadState.Unstarted)
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
                        if ((string)(Yellow.Tag) == "up") Yellow.Tag = "down";
                        else                    Yellow.Tag = "up";
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
            Console.WriteLine(((Ellipse)el).Name);
        }

    }
}