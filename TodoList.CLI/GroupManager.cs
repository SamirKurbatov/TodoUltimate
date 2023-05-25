using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.CLI.Models;

namespace TodoList.CLI
{
    public class GroupManager : BaseManager<GroupModel>
    {
        public GroupManager(BaseRepository repository, BaseCreator<GroupModel> creator) : base(repository, creator)
        {
        }

        public override void Add()
        {
            GroupModel model = creator.Create();

            var modelIndex = models.Data.Keys.Count + 1;

            models.Data.Add(modelIndex, model);

            repository.Save(models);
        }
    }
}