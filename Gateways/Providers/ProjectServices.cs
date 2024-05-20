
using TeamHubServiceProjects.Entities;
using TeamHubServiceProjects.Gateways.Interfaces;

namespace TeamHubServiceProjects.Gateways.Providers;

public class ProjectServices : IProjectServices
{
    private TeamHubContext dbContext;

    public ProjectServices(TeamHubContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public bool AddProject(project projectNew)
    {
        try
        {
            dbContext.project.Add(projectNew);
            dbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DeleteProject(int projectId)
    {
        try
        {
            var projectDB = dbContext.project.Find(projectId);
            if (projectDB != null) 
            {
                dbContext.Remove(projectDB);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<project> GetProjects()
    {
        return dbContext.project.ToList();
    }

    public bool UpdateProject(project projectUpdate)
    {
        try
        {
            var projectDB = dbContext.project.Find(projectUpdate.Name);
            if (projectDB != null) 
            {
                projectDB.Name = projectUpdate.Name;
                projectDB.projectstudent = projectUpdate.projectstudent;
                projectDB.StartDate = projectUpdate.StartDate;
                projectDB.EndDate = projectUpdate.EndDate;
                dbContext.project.Update(projectDB);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }   
}