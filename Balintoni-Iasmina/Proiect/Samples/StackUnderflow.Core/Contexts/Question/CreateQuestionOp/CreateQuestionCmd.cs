using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Access.Primitives.IO;
using EarlyPay.Primitives.ValidationAttributes;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp
{
    public struct CreateQuestionCmd
    {
        public CreateQuestionCmd(Guid organisationId, string userName, string questionTitle, string questionBody, string questionTags, Guid userId)
        {
            OrganisationId = organisationId;
            UserName = userName;
            QuestionTitle = questionTitle;
            QuestionBody = questionBody;
            QuestionTags = questionTags;
            UserId = userId;
        }
        [GuidNotEmpty]
        public Guid OrganisationId { get; set; }
        [GuidNotEmpty]
        public Guid UserId { get; set; }
        [Required]
        public string QuestionTitle { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string QuestionBody { get; set; }
        [Required]
        public string QuestionTags { get; set; }
    }
}
