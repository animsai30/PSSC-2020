﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.ConfirmationOp
{
    public class ConfirmationAcknowledgement
    {
        public string Receipt { get; private set; }

        public ConfirmationAcknowledgement(string receipt)
        {
            Receipt = receipt;
        }
    }
}
