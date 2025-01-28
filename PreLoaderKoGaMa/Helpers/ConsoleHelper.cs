namespace PreLoaderKoGaMa.Helpers
{
    internal class ConsoleHelper
    {
        enum MessageInfo
        {
            Log,
            Error,
            Warn
        }
        static ConsoleColor GetByMessageInfo(MessageInfo messageInfo)
        {
            return messageInfo switch
            {
                MessageInfo.Log => ConsoleColor.Blue,
                MessageInfo.Error => ConsoleColor.Red,
                MessageInfo.Warn => ConsoleColor.Yellow,
                _ => ConsoleColor.White,
            };
        }
        static char GetSigla(MessageInfo messageInfo)
        {
            return messageInfo switch
            {
                MessageInfo.Log => 'I',
                MessageInfo.Error => 'E',
                MessageInfo.Warn => 'W',
                _ => ' ',
            };
        }
        static void WriteLine(string title, string message, MessageInfo messagetype)
        {
            Console.Write(DateTime.Now);
            Console.Write(" ");
            Console.Write(GetSigla(messagetype));
            Console.Write(" ");

            Console.ForegroundColor = GetByMessageInfo(messagetype);
            Console.Write($"[{title}]: ");
            Console.ResetColor();
            Console.WriteLine(message);
        }
        public static void Log(string title, string message)
        {
            WriteLine(title, message, MessageInfo.Log); 
        }
        public static void Error(string title, string message)
        {
            WriteLine(title, message, MessageInfo.Error);

        }
        public static void Warn(string title, string message)
        {
            WriteLine(title, message, MessageInfo.Warn);

        }



    }
}
