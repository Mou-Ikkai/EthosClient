using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthosClient.Utils
{
    public static class ConsoleUtil
    {
        public static void Info(string text) => MelonModLogger.Log(ConsoleColor.Cyan, $"[Ethos] [INFO] {text}");

        public static void Error(string text) => MelonModLogger.Log(ConsoleColor.Red, $"[Ethos] [ERROR] {text}");

        public static void Success(string text) => MelonModLogger.Log(ConsoleColor.Green, $"[Ethos] [SUCCESS] {text}");

        public static void Exception(Exception e)
        {
            MelonModLogger.Log(ConsoleColor.Yellow, $"[Ethos] [EXCEPTION (REPORT TO YAEKITH)]: ");
            MelonModLogger.Log(ConsoleColor.Red, $"============= STACK TRACE ====================");
            MelonModLogger.Log(ConsoleColor.White, e.StackTrace.ToString());
            MelonModLogger.Log(ConsoleColor.Red, "===============================================");
            MelonModLogger.Log(ConsoleColor.Red, "============== MESSAGE ========================");
            MelonModLogger.Log(ConsoleColor.White, e.Message.ToString());
            MelonModLogger.Log(ConsoleColor.Red, "===============================================");
        }

        public static void SetTitle(string title) => System.Console.Title = title;
    }
}
