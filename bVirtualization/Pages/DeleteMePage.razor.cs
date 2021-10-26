// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bVirtualization.Pages
{
    public partial class DeleteMePage
    {
        public ValueTask<(IReadOnlyList<Student>, int)> RetrieveStudentsAsync(int location, int count)
        {
            List<Student> students = new List<Student>
        {
            new Student
            {
                Id = Guid.NewGuid(),
                FirstName = "Hassan"
            },
            new Student
            {
                   Id = Guid.NewGuid(),
                FirstName = "Brian"
            },
            new Student
            {
                   Id = Guid.NewGuid(),
                FirstName = "Josh"
            },
            new Student
            {
                   Id = Guid.NewGuid(),
                FirstName = "Robert"
            }
        };

            return ValueTask.FromResult(((IReadOnlyList<Student>)students, students.Count));
        }
    }

    

    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
    }
}
