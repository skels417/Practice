using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Newtonsoft.Json;
using System.IO;

public class TaskViewModel : INotifyPropertyChanged
{
    public ObservableCollection<TaskItem> Tasks { get; set; }
    private string _newTaskName;
    private string _filter;
    private TaskItem _selectedTask;

    public string NewTaskName
    {
        get => _newTaskName;
        set
        {
            _newTaskName = value;
            OnPropertyChanged(nameof(NewTaskName));
        }
    }

    public string Filter
    {
        get => _filter;
        set
        {
            _filter = value;
            OnPropertyChanged(nameof(Filter));
            OnPropertyChanged(nameof(FilteredTasks));
        }
    }

    public TaskItem SelectedTask
    {
        get => _selectedTask;
        set
        {
            _selectedTask = value;
            OnPropertyChanged(nameof(SelectedTask));
        }
    }

    public ObservableCollection<string> FilterOptions { get; } = new ObservableCollection<string>
    {
        "Все",
        "Выполненные",
        "Не выполненные"
    };

    public ObservableCollection<TaskItem> FilteredTasks => new ObservableCollection<TaskItem>(
        Tasks.Where(t => Filter == "Все" || (Filter == "Выполненные" && t.IsCompleted) || (Filter == "Не выполненные" && !t.IsCompleted))
    );

    public ICommand AddTaskCommand { get; }
    public ICommand RemoveTaskCommand { get; }
    public ICommand SaveTasksCommand { get; }
    public ICommand LoadTasksCommand { get; }

    public TaskViewModel()
    {
        Tasks = new ObservableCollection<TaskItem>();
        AddTaskCommand = new RelayCommand(_ => AddTask());
        RemoveTaskCommand = new RelayCommand(RemoveTask);
        SaveTasksCommand = new RelayCommand(_ => SaveTasks());
        LoadTasksCommand = new RelayCommand(_ => LoadTasks());
        Filter = "Все"; 
    }

    private void AddTask()
    {
        if (!string.IsNullOrWhiteSpace(NewTaskName))
        {
            
            var newTask = new TaskItem { Name = NewTaskName };
            Tasks.Add(newTask); 
            NewTaskName = string.Empty; 
            OnPropertyChanged(nameof(FilteredTasks)); 
        }
    }

    private void RemoveTask(object task)
    {
        if (task is TaskItem taskItem)
        {
            Tasks.Remove(taskItem);
            OnPropertyChanged(nameof(FilteredTasks));
        }
    }

    private void SaveTasks()
    {
        var json = JsonConvert.SerializeObject(Tasks);
        File.WriteAllText("tasks.json", json);
    }

    private void LoadTasks()
    {
        if (File.Exists("tasks.json"))
        {
            var json = File.ReadAllText("tasks.json");
            var tasks = JsonConvert.DeserializeObject<ObservableCollection<TaskItem>>(json);
            Tasks.Clear();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
            OnPropertyChanged(nameof(FilteredTasks));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}