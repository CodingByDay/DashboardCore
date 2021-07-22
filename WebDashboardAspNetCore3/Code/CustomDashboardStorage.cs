using DevExpress.DashboardWeb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

public class CustomDashboardStorage : IEditableDashboardStorage {
    private readonly IHttpContextAccessor contextAccessor;
    private string dashboardStorageFolder;

    public CustomDashboardStorage(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor contextAccessor) {
        this.contextAccessor = contextAccessor;
        this.dashboardStorageFolder = hostingEnvironment.ContentRootFileProvider.GetFileInfo("Data/Dashboards").PhysicalPath;
    }

    public IEnumerable<DashboardInfo> GetAvailableDashboardsInfo() {
        var dashboardInfos = new List<DashboardInfo>();
        var adminInfos = new List<DashboardInfo>();
        

        var userName = contextAccessor.HttpContext.Session.GetString("user"); // User name.
        if (userName == Guid.Empty.ToString())
        {
            var files = Directory.GetFiles(dashboardStorageFolder, "*.xml");

            foreach (var item in files)
            {
                var name = Path.GetFileNameWithoutExtension(item); // Dashboard name.
                adminInfos.Add(new DashboardInfo() { ID = name, Name = name }); // Appends the user ID to the end of the name dashboard name.
            }
            return adminInfos;
        }
        else
        {
            var files = Directory.GetFiles(dashboardStorageFolder, "*.xml");

            foreach (var item in files)
            {
                var name = Path.GetFileNameWithoutExtension(item); // Dashboard name.
                var regularUser = contextAccessor.HttpContext.Session.GetString("user"); // User name.

                if (!string.IsNullOrEmpty(regularUser) && name.EndsWith(regularUser, System.StringComparison.InvariantCultureIgnoreCase))
                    dashboardInfos.Add(new DashboardInfo() { ID = name, Name = name }); // Appends the user ID to the end of the name dashboard name.
            }

            return dashboardInfos;
        }
    }

    public XDocument LoadDashboard(string dashboardID) {
        if (GetAvailableDashboardsInfo().Any(di => di.Name == dashboardID)) {
            var path = Path.Combine(dashboardStorageFolder, dashboardID + ".xml");
            var content = File.ReadAllText(path);
            return XDocument.Parse(content);
        }
        else {
            throw new System.ApplicationException("You are not authorized to view this dashboard.");
        }
    }

    public string AddDashboard(XDocument dashboard, string dashboardName) {
        var userName = contextAccessor.HttpContext.Session.GetString("user");

        // if (string.IsNullOrEmpty(userName) || userName != "Admin")
           // throw new System.ApplicationException("You are not authorized to add dashboards.");

        var path = Path.Combine(dashboardStorageFolder, dashboardName + "_" + userName + ".xml");

        File.WriteAllText(path, dashboard.ToString());

        return Path.GetFileNameWithoutExtension(path);
    }

    public void SaveDashboard(string dashboardID, XDocument dashboard) {
        var userName = contextAccessor.HttpContext.Session.GetString("user"); // Username

        // if (string.IsNullOrEmpty(userName) || userName != "Admin")
        // throw new System.ApplicationException("You are not authorized to save dashboards.");
        string appended = $"{dashboardID}{userName}";
        var something=9;
        var path = Path.Combine(dashboardStorageFolder, dashboardID + ".xml");

        File.WriteAllText(path, dashboard.ToString());
    }
}