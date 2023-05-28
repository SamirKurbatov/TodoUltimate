using TodoList.CLI;
using TodoList.CLI.Models;

var issueRepository = new JsonRepository<IssueModel>("issue.json");
var groupRepository = new JsonRepository<GroupModel>("group.json");

var issueCreator = new BaseConsoleCreator<IssueModel>();
var groupCreator = new BaseConsoleCreator<GroupModel>();

var issueManager = new IssueManager(issueRepository, issueCreator);
var groupManager = new GroupManager(groupRepository, groupCreator);

MainMenu menu = new MainMenu(issueManager, groupManager);

menu.Start();
