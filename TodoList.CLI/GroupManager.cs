using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.CLI.Models;

namespace TodoList.CLI.Repositories
{
    public class GroupManager : BaseManager<GroupModel>
    {
        public GroupManager(BaseRepository repository) : base(repository)
        {
        }
    }
}