using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetToDone.Tests
{
    class TestTaskModelContainer : GetItDone.ITaskModelContainer
    {
        public TestTaskModelContainer(System.Data.Objects.IObjectSet<GetItDone.Task> tasks)
        {
            _Tasks = tasks;
        }

        public System.Data.Objects.IObjectSet<GetItDone.ChecklistItem> ChecklistItems
        {
            get { throw new NotImplementedException(); }
        }

        public System.Data.Objects.IObjectSet<GetItDone.ChecklistTemplate> ChecklistTemplates
        {
            get { throw new NotImplementedException(); }
        }

        public System.Data.Objects.IObjectSet<GetItDone.Context> Contexts
        {
            get { throw new NotImplementedException(); }
        }

        public System.Data.Objects.IObjectSet<GetItDone.UserEmail> Emails
        {
            get { throw new NotImplementedException(); }
        }

        public System.Data.Objects.IObjectSet<GetItDone.Person> People
        {
            get { throw new NotImplementedException(); }
        }

        public System.Data.Objects.IObjectSet<GetItDone.Project> Projects
        {
            get { throw new NotImplementedException(); }
        }

        public System.Data.Objects.IObjectSet<GetItDone.Review> Reviews
        {
            get { throw new NotImplementedException(); }
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        private System.Data.Objects.IObjectSet<GetItDone.Task> _Tasks;
        public System.Data.Objects.IObjectSet<GetItDone.Task> Tasks
        {
            get
            {
                return _Tasks;
            }
        }
    }
}
