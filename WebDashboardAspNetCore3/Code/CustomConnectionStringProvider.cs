using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

public class CustomConnectionStringProvider : IDataSourceWizardConnectionStringsProvider {
    private readonly IHttpContextAccessor contextAccessor;
    private Dictionary<string, string> connectionStrings = new Dictionary<string, string>();

    public CustomConnectionStringProvider(IHttpContextAccessor contextAccessor) {
        this.contextAccessor = contextAccessor;

        connectionStrings.Add("main", "XpoProvider=MSSqlServer; Data Source=10.100.100.25/SPLAHOST; Database=zenas;Application Name=Dashboard;Integrated Security=false;User ID=petpakn;Password=net123tnet!;");

    }

    public Dictionary<string, string> GetConnectionDescriptions() {
        var connections = new Dictionary<string, string>();
        var userName = contextAccessor.HttpContext.Session.GetString("CurrentUser");

        if (userName == "Admin") {
            connections.Add("NorthwindConnectionString", "Northwind Connection");
            connections.Add("CarsXtraSchedulingConnectionString", "CarsXtraScheduling Connection");
        }
        else if (userName == "User") {
            connections.Add("CarsXtraSchedulingConnectionString", "CarsXtraScheduling Connection");
        }

        return connections;
    }

    public DataConnectionParametersBase GetDataConnectionParameters(string name) {
        if (GetConnectionDescriptions().ContainsKey(name)) {
            return new CustomStringConnectionParameters(connectionStrings[name]);
        }
        else {
            throw new System.ApplicationException("You are not authorized to use this connection.");
        }
    }
}