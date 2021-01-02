using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Access.Primitives.IO;
using LanguageExt;
using StackUnderflow.Domain.Schema.Backoffice.CreateTenantOp;
using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO.Attributes;
using Access.Primitives.IO.Mocking;
using StackUnderflow.Domain.Core.Contexts;
using StackUnderflow.EF.Models;

using StackUnderflow.Domain.Schema.Backoffice;
using static StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp.CreateQuestionResult;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp;
using StackUnderflow.Domain.Schema.Models;

namespace StackUnderflow.Backoffice.Adapters.CreateQuestion
{
    public partial class CreateQuestionAdapter : Adapter<CreateQuestionCmd, ICreateQuestionResult, BackofficeWriteContext, BackofficeDependencies>
    {
        private readonly IExecutionContext _ex;

        public CreateQuestionAdapter(IExecutionContext ex)
        {
            _ex = ex;
        }
        public override async Task<ICreateQuestionResult> Work(CreateQuestionCmd command, BackofficeWriteContext state, BackofficeDependencies dependencies)
        {
            var workflow = from valid in command.TryValidate()
                           let t = AddQuestionIfMissing(state, CreateQuestionFromCommand(command))
                           select t;


            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new InvalidRequest(ex.ToString()));

            return result;
        }

        public ICreateQuestionResult AddQuestionIfMissing(BackofficeWriteContext state, QuestionSummary question)
        {
            if (state.Questions.Any(p => p.QuestionId.Equals(question.QuestionId)))
                return new QuestionNotCreated();

            if (state.Questions.All(p => p.QuestionId != question.QuestionId))
                state.Questions.Add(question);
            return new QuestionCreated(question, tenant.TenantUser.Single().User);
        }

        private Tenant CreateQuestionFromCommand(CreateQuestionCmd cmd)
        {
            var question = new QuestionSummary()
            {
                Description = cmd.Description,
                Name = cmd.TenantName,
                OrganisationId = cmd.OrganisationId,
            };
            tenant.TenantUser.Add(new TenantUser()
            {
                User = new User()
                {
                    UserId = cmd.UserId,
                    Name = cmd.AdminName,
                    Email = cmd.AdminEmail,
                    DisplayName = cmd.AdminName,
                    WorkspaceId = cmd.UserId
                }
            });
            return tenant;
        }

        public override Task PostConditions(CreateTenantCmd op, CreateTenantResult.ICreateTenantResult result, BackofficeWriteContext state)
        {
            return Task.CompletedTask;
        }

    }
