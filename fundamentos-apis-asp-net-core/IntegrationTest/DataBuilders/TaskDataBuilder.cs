using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListApi.Models;

namespace IntegrationTest.DataBuilders;

internal class TaskDataBuilder : Faker<TaskItem>
{
    public TaskDataBuilder(Guid userId)
    {
        CustomInstantiator(f =>
        {
            string Title = f.Lorem.Sentence(10);
            string Description = f.Lorem.Sentence(10);

            return new TaskItem
            {
                Title = Title,
                Description = Description,
                UserId = userId,
            };
        });
    }


    public string Title { get; set; }
    public string Description { get; set; }

    public TaskItem Build() => Generate();
}
