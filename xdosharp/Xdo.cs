using System;
using System.Diagnostics;
using System.Xml;

namespace xdosharp
{
    public class Xdo
    {
        public void SendKey(string Key, string WindowName = null)
        {
            if (WindowName != null)
            {
                FocusWindow(WindowName);
            }
            DoXDo("key " + Key);
        }

        public void SendSentence(string Sentence, string WindowName = null)
        {
            if (WindowName != null)
            {
                FocusWindow(WindowName);
            }

            foreach (char P in Sentence)
            {
                SendKey(P.ToString(), WindowName);
            }
        }

        public void Click(string WindowName, bool Right = false )
        {
            string Window = "";
            if (WindowName != null)
            {
                Window = FocusWindow(WindowName);
            }
            if (Right)
            {
                DoXDo("getactivewindow mousemove 0 0 mousemove --window " + Window + " 10 10 click 2 mousemove restore");
            }
            else
            {
                DoXDo("getactivewindow mousemove 0 0 mousemove --window " + Window + " 10 10 click 1 mousemove restore");
            }
        }

        private string FocusWindow(string WindowName)
        {
            ProcessStartInfo WID = new ProcessStartInfo();
                WID.FileName = "xdotool";
                WID.Arguments = "search --name " + WindowName;
                WID.UseShellExecute = false;
                WID.RedirectStandardOutput = true;
                var P = Process.Start(WID);
                P.WaitForExit();
                var Output = P.StandardOutput.ReadLine();
#if DEBUG
                Console.WriteLine("ID is " + Output);
#endif
                DoXDo("windowactivate --sync " + Output);
                //System.Threading.Thread.Sleep(100);
                return Output;
        }

        private static void DoXDo(string arguments)
        {
            ProcessStartInfo XDoTool = new ProcessStartInfo();
            XDoTool.FileName = "xdotool";
            XDoTool.Arguments = arguments;
            Process.Start(XDoTool).WaitForExit();
        }
    }
}