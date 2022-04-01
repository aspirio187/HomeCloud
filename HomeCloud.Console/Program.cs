using HomeCloud.FSWatcher;

Watcher watcher = new Watcher(@"C:\Users\soult\Desktop\Dossier à surveiller");

watcher.OnErrorOnccured += Watcher_OnErrorOnccured;
watcher.OnChangesOccured += Watcher_OnChangesOccured;

watcher.Start();

void Watcher_OnChangesOccured(Change change)
{
    Console.WriteLine("Un changement est survenu:");
    Console.WriteLine($"\t{change.ChangeType} - {change.FileFullPath}");
    if (change.OldPath is not null)
    {
        Console.WriteLine($"\t{change.ChangeType} - {change.OldPath}");
    }
}

void Watcher_OnErrorOnccured(Exception e)
{
    Console.WriteLine($"Une erreur est survenue : {e.Message}");
}

Console.ReadKey();