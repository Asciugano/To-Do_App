using SQLite;
using To_Do_List.Model;

namespace To_Do_List.Services;

public static class DatabaseService
{
    private static SQLiteAsyncConnection _db;
    
    public static async Task InitDB()
    {
        if(_db != null)
            return ;   
        
        try
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Database", "todo.db");

            var folder = Path.GetDirectoryName(dbPath);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            _db = new SQLiteAsyncConnection(dbPath);

            await _db.CreateTableAsync<TodoItem>();
            await _db.CreateTableAsync<ListTodo>();
            
            await Shell.Current.DisplayAlert($"Path DB:",$"{dbPath}", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}", "OK");
        }
    }
    
    public static Task<List<TodoItem>> GetTodoItemsInListTodo(int listId)
    {
        return _db.Table<TodoItem>().Where(i => i.ListId == listId).ToListAsync();
    }
    
    public static Task<TodoItem> GetItemByID(int id) 
    {
        return _db.Table<TodoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }
    
    public static Task<ListTodo> GetListTodoByID(int id) => _db.Table<ListTodo>().Where(l => l.Id == id).FirstOrDefaultAsync();
    
    public static Task<List<ListTodo>> GetAllListTodo() => _db.Table<ListTodo>().ToListAsync();
    
    public static Task<int> AddTodoItem(TodoItem item) => _db.InsertAsync(item);
    
    public static Task<int> DeleteTodoItem(TodoItem item) => _db.DeleteAsync(item);
    
    public static Task<int> AddListTodo(ListTodo list) => _db.InsertAsync(list);

    public static Task<int> DeleteListTodo(ListTodo list) => _db.DeleteAsync(list);
}