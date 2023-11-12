﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OMG.SharedKernel.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException() { }

        public EntityAlreadyExistsException(string message) : base(message) { }

        public EntityAlreadyExistsException(string message, Exception inner) : base(message, inner)
        { }
    }
}
