using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HeadOrTail.View
{
    public sealed partial class CoinFlipper : UserControl
    {
        private DispatcherTimer t;
        private const int NumberOfFrames = 12;
        private const int FrameWidth = 200;
        private int currentFrame = 0;

        public CoinFlipper()
        {
            this.InitializeComponent();
            t = new DispatcherTimer();

            
            t.Tick += t_Tick;

        }


        
        private int counterStep = 1;
        private int[] counters = {1, 1, 2, 2, 4, 4, 6, 6};

        private int a = 0;
        private Random r = new Random();

        private int counter = 0;
        private void t_Tick(object sender, object o)
        {
            counter++;
            if (counter >= counters[counterStep])
            {
                if (NextSprite())
                {
                    counterStep++;
                    if (counterStep == (6 - a))
                        t.Stop();
                }
                counter = 0;
            }
        }

        private bool NextSprite()
        {
            bool nextStep = false;
            currentFrame++;
            if (currentFrame != 0 && currentFrame%(NumberOfFrames/2) == 0)
            {
                nextStep = true;
            }
            if (currentFrame == NumberOfFrames)
                currentFrame = 0;
            SpriteSheetOffset.X = -200*currentFrame;
            return nextStep;
        }






        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (!t.IsEnabled)
            {
                t.Interval = TimeSpan.FromMilliseconds(r.Next(13, 20));
                counter = 0;
                counterStep = 0;
                a = r.Next()%2;
                t.Start();
            }
        }
    }

}
