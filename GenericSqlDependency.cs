using GalaSoft.MvvmLight.Messaging;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Delegates;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace Pandap.UI.Modules.OperasyonModule
{
    public class HaftalikPlanDTO
    {
        public string Id { get; set; }
        public string Pazartesi { get; set; }
        public string Sali { get; set; }
        public string Carsamba { get; set; }
        public string Persembe { get; set; }
        public string Cuma { get; set; }
        public string Cumartesi { get; set; }
        public string Pazar { get; set; }

    }

    public class GenericSqlDependency
    {
        public static GenericSqlDependency Default { get; set; }

        SqlTableDependency<HaftalikPlanDTO> tdp;

        static GenericSqlDependency()
        {
            Default = new GenericSqlDependency();
        }

        public void Basla()
        {
            string cnnStr = $"server=localhost,1501;database=PANDAPDB;user id=sa;password=0000;App=Pandap";
            tdp = new SqlTableDependency<HaftalikPlanDTO>(cnnStr, "HaftalikYuklemePlan", "Operasyon");
            tdp.OnChanged += TableDependency_Changed;
            tdp.Start();
        }

        private void TableDependency_Changed(object sender, RecordChangedEventArgs<HaftalikPlanDTO> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                Messenger.Default.Send(e.Entity);

               
            }
        }


        public void Durdur()
        {
            tdp.OnChanged -= TableDependency_Changed;
            tdp.Stop();
        }

    }
}
