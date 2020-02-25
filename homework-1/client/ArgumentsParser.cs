using System;
using System.Collections.Generic;
using System.Text;

namespace client
{
    internal class ArgumentsParser
    {
        private readonly string[] _arguments;

        public ArgumentsParser(string[] arguments)
        {
            _arguments = arguments;
        }

        public string GetServerAddress()
        {
            return _arguments[0];
        }

        public int GetPort()
        {
            return int.Parse(_arguments[1]);
        }

        public int GetProtocol()
        {
            return int.Parse(_arguments[2]);
        }

        public string GetDataLocation()
        {
            return _arguments[3];
        }

        public bool IsStopAndWait()
        {
            return bool.Parse(_arguments[4]);
        }

        public bool CheckArgumentsLength()
        {
            return _arguments.Length == 5;
        }
    }
}
