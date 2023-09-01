using System.Security.AccessControl;

namespace Core.CrossCuttingConcerns.Logging;

public class LogParemeter
{
    public string Name { get; set; }
    public object Value { get; set; }
    public string Type { get; set; }

    public LogParemeter()
    {
        Name = string.Empty;
        Value=string.Empty;
        Type = string.Empty;
    }

    public LogParemeter(string name, object value, string type)
    {
        Name = name;
        Value = value;
        Type = type;
    }
}