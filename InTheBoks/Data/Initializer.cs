using InTheBoks.Data.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace InTheBoks.Data
{
    // Based upon http://stackoverflow.com/a/10566485

    public class Initializer : IDatabaseInitializer<DataContext>
    {
        public void InitializeDatabase(DataContext context)
        {
            if (System.Diagnostics.Debugger.IsAttached && context.Database.Exists())
            {

                try
                {
                    // This require to open connection and can potentially fail whenever there is an issue.
                    var isCompatible = context.Database.CompatibleWithModel(false);

                    if (!isCompatible)
                    {
                        context.Database.Delete();
                    }
                }
                catch (System.Data.EntityException ex)
                {
                    // Verify if the connection open failed and delete anyway.
                    // This will obviously fail if the culture is not english on the host machine.
                    // TODO: Figure out a way to get some kind of error ID from this exception.
                    if (ex.Message == "The underlying provider failed on Open.")
                    {
                        //context.Database.Delete();
                    }
                }
            }

            if (!context.Database.Exists())
            {
                context.Database.Create();

                var contextObject = context as System.Object;
                var contextType = contextObject.GetType();
                var properties = contextType.GetProperties();
                System.Type t = null;
                string tableName = null;
                string fieldName = null;
                foreach (var pi in properties)
                {
                    if (pi.PropertyType.IsGenericType && pi.PropertyType.Name.Contains("DbSet"))
                    {
                        t = pi.PropertyType.GetGenericArguments()[0];

                        var mytableName = t.GetCustomAttributes(typeof(TableAttribute), true);
                        if (mytableName.Length > 0)
                        {
                            TableAttribute mytable = mytableName[0] as TableAttribute;
                            tableName = mytable.Name;
                        }
                        else
                        {
                            tableName = pi.Name;
                        }

                        foreach (var piEntity in t.GetProperties())
                        {
                            if (piEntity.GetCustomAttributes(typeof(UniqueAttribute), true).Length > 0)
                            {
                                fieldName = piEntity.Name;
                                context.Database.ExecuteSqlCommand("ALTER TABLE " + tableName + " ADD CONSTRAINT con_Unique_" + tableName + "_" + fieldName + " UNIQUE (" + fieldName + ")");
                            }
                        }
                    }
                }
            }
        }
    }
}
