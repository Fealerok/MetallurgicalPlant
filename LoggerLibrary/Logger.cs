namespace LoggerLibrary
{
    public class Logger
    {
        public void MakeLog(string log)
        {
            File.AppendAllText("MetallurgicalPlantLogs.txt", $"[{DateTime.Now}] - {log}\n");
        }
    }
}
