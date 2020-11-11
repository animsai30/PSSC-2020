using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Profile.Domain.CreateProfileWorkflow.EmailAddress;

namespace Profile.Domain.CreateProfileWorkflow
{
    public class RestPasswordService
    {
        public Task SendRestPasswordLink(VerifiedEmail email)
        {

            return Task.CompletedTask;
        }
    }
}
