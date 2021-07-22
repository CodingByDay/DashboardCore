using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

public class CustomConnectionStringProvider : IDataSourceWizardConnectionStringsProvider
{
    private readonly IHttpContextAccessor contextAccessor;
    private Dictionary<string, string> connectionStrings = new Dictionary<string, string>();

    public CustomConnectionStringProvider(IHttpContextAccessor contextAccessor)
    {
      
        this.contextAccessor = contextAccessor;
        connectionStrings.Add("zenas", @"XpoProvider=MSSqlServer;Data Source=172.17.1.38;initial catalog=TrendNET_ZENASv4;User ID=TrendNET_ZENASv4;Password=net321tnet;persist security info=False;packet size=4096;");
        connectionStrings.Add("empty", @"XpoProvider=MSSqlServer; Data Source=10.100.100.25/SPLAHOST; Database=zenas;Application Name=Dashboard;Integrated Security=false;User ID=petpakn;Password=net123tnet!;");
    }

    public Dictionary<string, string> GetConnectionDescriptions()
    {
        var connections = new Dictionary<string, string>();
        var userName = contextAccessor.HttpContext.Session.GetString("CurrentUser");
        connections.Add("zenas", "Zenas database production.");
        connections.Add("empty", "Empty database for future use.");

        return connections;
    }

    public DataConnectionParametersBase GetDataConnectionParameters(string name)
    {
     
     return new CustomStringConnectionParameters(connectionStrings[name]);
       
     
    }
}