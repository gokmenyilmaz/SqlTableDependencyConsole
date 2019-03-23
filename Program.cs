using System;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

public class HaftalikPlan
{
    public string Id { get; set; }
    public string Pazartesi { get; set; }
}

public class Program
{
    private static string _con = $"server=localhost,1501;database=PANDAPDB;user id=sa;password=0000;App=Pandap";

    public static void Main()
    {

        using (var dep = new SqlTableDependency<HaftalikPlan>(_con, "HaftalikYuklemePlan","Operasyon")) 
        {
            dep.OnChanged += Changed;
            dep.Start();

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();

            dep.Stop();
        }
    }

    public static void Changed(object sender, RecordChangedEventArgs<HaftalikPlan> e)
    {
        var changedEntity = e.Entity;

        Console.WriteLine("DML operation: " + e.ChangeType);
        Console.WriteLine("ID: " + changedEntity.Id);
      
    }
}