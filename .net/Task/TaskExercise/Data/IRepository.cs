using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskExercise.Models;

namespace TaskExercise.Data
{
  

        public interface IRepository
        {
            Task<IEnumerable<ItemTask>> GetTasksAsync();
            Task CreateTaskAsync(string title);
            Task UpdateTaskAsync(int id, bool isCompleted);     
    }
}
