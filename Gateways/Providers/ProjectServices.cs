
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

    public bool AddProject(project projectNew, int studentID)
    {
        try
        {
            dbContext.project.Add(projectNew);
            dbContext.SaveChanges();
            projectstudent projectstudentaux = new projectstudent();
            projectstudentaux.IdStudent = studentID;
            projectstudentaux.IdProject = projectNew.IdProject;
            dbContext.projectstudent.Add(projectstudentaux);
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

    public List<project> GetAllProjectsByStudentID(int studentID)
    {
        return dbContext.project
                        .Where(p => p.projectstudent.Any(ps => ps.IdStudent == studentID))
                        .ToList();
    }

    public bool UpdateProject(project projectUpdate)
    {
        try
        {
            var projectDB = dbContext.project.Find(projectUpdate.IdProject);
            if (projectDB != null)
            {
                projectDB.Name = projectUpdate.Name;
                projectDB.StartDate = projectUpdate.StartDate;
                projectDB.EndDate = projectUpdate.EndDate;
                projectDB.Status = projectUpdate.Status;
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
            Console.Error.WriteLine($"Error updating project: {ex.Message}");
            return false;
        }
    }


    public project GetProjectByID(int projectId)
    {
        return dbContext.project.Find(projectId);
    }
}