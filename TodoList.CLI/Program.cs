using TodoList.CLI;
using TodoList.CLI.BaseAbstractions;
using TodoList.CLI.Models;

IFactoryModel<IssueModel> issueCreator = new IssueModelFactory();
IFactoryModel<IssueGroupModel> groupCreator = new GroupModelFactory();

IDataRepository<IssueModel> issueRepository = new JsonRepository<IssueModel>("issue.json");
IDataRepository<IssueGroupModel> groupRepository = new JsonRepository<IssueGroupModel>("group.json");

var issueManager = new IssueController(issueRepository, issueCreator);
var groupManager = new IssueGroupController(groupRepository, groupCreator);

MainMenu menu = new MainMenu(issueManager, groupManager);

menu.Start();