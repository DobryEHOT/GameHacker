using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameOnWinForms
{
    internal class HackerConsole
    {
        public Form2 secondForm { get; private set; }
        public ControllPanel panel { get; private set; }
        private string mainCommand;
        public string buffer = "";
        public bool programmWorking { get; private set; }
        public bool noBuildExeption { get; set; }
        public bool noLastBuildExeption { get; set; }

        public Queue<InfoCommand> inputCommands = new Queue<InfoCommand>();

        public HackerConsole(ControllPanel panel)
        {
            this.panel = panel;
        }

        public void OpenConsole()
        {
            secondForm = new Form2(this);
            secondForm.Show();
        }

        public void CompilingProgramm()
        {
            System.IO.File.Delete(Environment.CurrentDirectory + "\\Levels\\" + panel.lvlIndex + "\\mainText.exe");
            programmWorking = true;
            buffer = "";

            mainCommand = "/c chcp 65001 & cd " + Environment.CurrentDirectory + "\\Levels\\" + panel.lvlIndex + " & " +
                Environment.CurrentDirectory + "\\Compiller\\Compiller\\csc.exe " +
                "/reference:\"" + Environment.CurrentDirectory + "\\Compiller\\Compiller\\Lab\\SecurityFramework.dll\" " +
                "*.txt & mainText.exe";

            int countSkip = 9;

            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = mainCommand,
                UseShellExecute = false,
               // CreateNoWindow = true,
                RedirectStandardOutput = true
            });

            process.BeginOutputReadLine();
            process.OutputDataReceived += (s, e) =>
            {
                if (e.Data == null)
                {
                    noLastBuildExeption = noBuildExeption;

                    programmWorking = false;
                    noBuildExeption = false;
                    return;
                }

                if (countSkip == 0)
                {
                    buffer += e.Data + "\r\n";
                    ParserCommand.Parse(e.Data, this);
                }
                else
                  countSkip--; 
            };
        }

        void UpdateConsoleText(string text)
        {
            secondForm.textBox1.Text = text;
        }

        public void ComeCommand(InfoCommand info)
        {
            if (info == null) return;

            if (info.typeObj == CommandType.Door)
            {
                Door t = panel.objects.
                      Where(o => o is Door && ((Door)o).indexObj == info.indexObject).
                      Select(o => o as Door).
                      ToArray()[0];

                if (info.inputCommand == AllCommands.Open)
                    t.Open();

                if (info.inputCommand == AllCommands.Closed)
                    t.Closed();
            }
            if (info.typeObj == CommandType.MovePanel)
            {
                MovePanel t = panel.objects.
                      Where(o => o is MovePanel && ((MovePanel)o).indexObj == info.indexObject).
                      Select(o => o as MovePanel).
                      ToArray()[0];

                if (info.inputCommand == AllCommands.Move && t != null)
                    t.Move();
            }
        }
    }

    static class ParserCommand
    {
        public static void Parse(string command, HackerConsole console)
        {
            string[] commands = command.Split();

            if (!(command.Length > 4 && commands[0].Equals("(SF)")))
                return;

            if (commands.Length <= 2 && commands[1].Equals("Start"))
            {
                console.noBuildExeption = true;
                return;
            }
            var inputCommand = (AllCommands)int.Parse(commands[3]);
            var indexObject = int.Parse(commands[2]);
            var typeObj = (CommandType)int.Parse(commands[1]);

            console.inputCommands.Enqueue(new InfoCommand(typeObj, indexObject, inputCommand));
        }
    }

    public class InfoCommand
    {
        internal CommandType typeObj { get; private set; }
        internal int indexObject { get; private set; }
        internal AllCommands inputCommand { get; private set; }

        internal InfoCommand(CommandType typeObj, int indexObject, AllCommands inputCommand)
        {
            this.typeObj = typeObj;
            this.indexObject = indexObject;
            this.inputCommand = inputCommand;
        }
    }
}
