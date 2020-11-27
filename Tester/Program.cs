using System;
using xdosharp;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Xdo x = new Xdo();
            x.SendKey("s", "minecraft");
            x.SendSentence("t spaces don't work yet");
            x.Click("1.16.4", false);
        }
    }
}