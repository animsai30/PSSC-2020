﻿using System;
using System.Collections.Generic;
using System.Text;
using Access.Primitives.Extensions.Cloning;
using Access.Primitives.IO;
using CSharp.Choices;
using LanguageExt;
using StackUnderflow.Domain.Schema.Models;
using StackUnderflow.EF.Models;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult : IDynClonable { }
        public class QuestionCreated : ICreateQuestionResult
        {
            public QuestionSummary questionSummary { get; }
            public User AdminUser { get; }

            public QuestionCreated(QuestionSummary questionsummary, User adminUser)
            {
                questionSummary = questionsummary;
                AdminUser = adminUser;
            }

            public object Clone() => this.ShallowClone();

        }
        public class QuestionNotCreated : ICreateQuestionResult
        {
            public string Reason { get; private set; }

            ///TODO
            public object Clone() => this.ShallowClone();
        }
        public class InvalidRequest : ICreateQuestionResult
        {
            public string Message { get; }

            public InvalidRequest(string message)
            {
                Message = message;
            }

            ///TODO
            public object Clone() => this.ShallowClone();
        }
    }
}