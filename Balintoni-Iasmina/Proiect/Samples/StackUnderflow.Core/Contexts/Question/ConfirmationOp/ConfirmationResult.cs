using CSharp.Choices;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.ConfirmationOp
{
    [AsChoice]
    public static partial class ConfirmationResult
    {
        public interface IConfirmationResult { }

        public class QuestionConfirmed : IConfirmationResult
        {
            public User QuestionUser { get; }

            public string InvitationAcknowlwedgement { get; set; }

            public QuestionConfirmed(User adminUser, string invitationAcknowledgement)
            {
                QuestionUser = adminUser;
                InvitationAcknowlwedgement = invitationAcknowledgement;
            }
        }
        public class QuestionNotConfirmed : IConfirmationResult
        {
            ///TODO
        }

        public class InvalidRequest : IConfirmationResult
        {
            public string Message { get; }

            public InvalidRequest(string message)
            {
                Message = message;
            }

        }
    }

}
