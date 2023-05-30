using TodoList.CLI;
using TodoList.CLI.BaseAbstractions;
using TodoList.CLI.Models;

IModelFactory issueCreator = new IssueModelFactory();
IModelFactory groupCreator = new GroupModelFactory();

IDataRepository<IssueModel> issueRepository = new JsonRepository<IssueModel>("issue.json");
IDataRepository<GroupModel> groupRepository = new JsonRepository<GroupModel>("group.json");

var issueManager = new IssueController(issueRepository, issueCreator);
var groupManager = new GroupController(groupRepository, groupCreator);

MainMenu menu = new MainMenu(issueManager, groupManager);

menu.Start();