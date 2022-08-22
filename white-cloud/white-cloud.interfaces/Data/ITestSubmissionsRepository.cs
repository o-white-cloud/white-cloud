using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using white_cloud.entities.Tests;

namespace white_cloud.interfaces.Data
{
    public interface ITestSubmissionsRepository
    {
        Task InsertTestSubmission(TestSubmission testSubmission, TestSubmissionShare? submissionShare = null, int? testRequestId = null);
    }
}
