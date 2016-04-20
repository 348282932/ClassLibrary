using System;

namespace MyClassLibrary
{
    public interface ILogger
    {
        void Log(string message);
        void Log(string[] categories, string message);
        void LogException(Exception x);
    }
}
