﻿using System;
using System.Runtime.Serialization;

namespace AutomationCore.Shared.Exceptions
{
    public class InvalidSearchPropertyException : Exception
    {
        public InvalidSearchPropertyException(){}

        public InvalidSearchPropertyException(string message) : base(message){}

        protected InvalidSearchPropertyException(SerializationInfo info, StreamingContext context) : base(info, context){}

        public InvalidSearchPropertyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
