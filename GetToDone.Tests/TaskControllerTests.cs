using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Moq;
using System.Web.Mvc;
using GetItDone;

namespace GetToDone.Tests
{
    public class TaskControllerTest
    {
        public TaskControllerTest()
        {
        }

        [Fact()]
        public void TestFinishTask()
        {
            Guid memberGuid = Guid.NewGuid();

            System.Data.Objects.IObjectSet<GetItDone.Task> tasks = new InMemoryObjectSet<GetItDone.Task>(new GetItDone.Task[] { new GetItDone.Task() { Id = 1, Title = "Blah", ProjectId = null, OwnerId = memberGuid, Notes = "", Project = null } });
            TestTaskModelContainer container = new TestTaskModelContainer(tasks);

            Assert.Equal(1, container.Tasks.Count());

            //container.Tasks.AddObject(new GetItDone.Task { Id = 1, ProjectId = null, Project = null, Title = "Finishing task", OwnerId = memberGuid });

            GetItDone.ITaskService taskService = new GetItDone.TaskService(container, memberGuid);

            GetItDone.GetItDone.TaskController tc = new GetItDone.GetItDone.TaskController(taskService);

            ViewResult ar = tc.Finish(1) as ViewResult;

            Assert.NotNull(ar);

            TaskModel tm = ar.Model as TaskModel;

            Assert.NotNull(tm);
            Assert.Equal("Blah", tm.Title);
            Assert.Null(tm.ProjectName);
        }
    }
}