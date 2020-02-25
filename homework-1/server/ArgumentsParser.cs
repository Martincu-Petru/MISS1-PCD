using System;
using System.Collections.Generic;
using System.Text;

namespace server
{
    class ArgumentsParser
    {
        private readonly string[] _arguments;

        public ArgumentsParser(string[] arguments)
        {
            _arguments = arguments;
        }

        public int GetPort()
        {
            return int.Parse(_arguments[0]);
        }

        public bool IsStopAndWait()
        {
            return bool.Parse(_arguments[1]);
        }

        public bool CheckArgumentsLength()
        {
            return _arguments.Length == 2;
        }
    }
}
