// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tynamix.ObjectFiller;

namespace Demo.Pages
{
    public partial class Index
    {
        public async ValueTask<(IReadOnlyList<Student>, int)> RetrieveStudentsAsync(int location, int count)
        {

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:5001/");

            string data = await httpClient.GetStringAsync($"api/students?$skip={location}");
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(data);


            //List<Student> postStudents = new Filler<Student>().Create(1000).ToList();

            //foreach (var s in postStudents)
            //{
            //    string json = JsonConvert.SerializeObject(s);

            //    StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            //    var result = await httpClient.PostAsync("api/students", httpContent);

            //    result.EnsureSuccessStatusCode();
            //}

            return await ValueTask.FromResult(((IReadOnlyList<Student>)students, students.Count));
        }
    }

    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
    }
}
