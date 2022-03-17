using Digital_Classroom.Data;
using Digital_Classroom.Models;
using System.Collections.Generic;
using System.Linq;

namespace Digital_Classroom.Services
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ClassroomContext context;

        public SubjectRepository(ClassroomContext context)
        {
            this.context = context;
        }
        public int Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Subject> GetAll()
        {
            return context.Subjects.ToList();
        }

        public Subject GetById(int id)
        {
            return context.Subjects.SingleOrDefault(s => s.Id == id);
        }

        public Subject GetByName(string name)
        {
            return context.Subjects.SingleOrDefault(s => s.Name == name);
        }

        public int Insert(Subject subject)
        {
            context.Subjects.Add(subject);
            return context.SaveChanges();
        }

        public int Update(Subject newSubject, int id)
        {
            var oldSubject = GetById(id);
            if (oldSubject == null)
            {
                oldSubject.Name = newSubject.Name;
                oldSubject.Description = newSubject.Description;
            }
            return context.SaveChanges();
        }
    }
}
