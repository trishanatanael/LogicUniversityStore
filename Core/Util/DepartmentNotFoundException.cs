﻿using System;
using System.Runtime.Serialization;

namespace LogicUniversityStore.Utill
{
    [Serializable]
    internal class DepartmentNotFoundException : Exception
    {
        public DepartmentNotFoundException()
        {
        }

        public DepartmentNotFoundException(string message) : base(message)
        {
        }

        public DepartmentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DepartmentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}