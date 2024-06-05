
using TeamHubServiceProjects.DTOs;
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

    public project GetProject(int IdProject)
    {
        project project = null;
        try
        {
            var projectDB = dbContext.project.Find(IdProject);
            if (projectDB != null)
            {
                project = new project
                {
                    Name = projectDB.Name,
                    StartDate = projectDB.StartDate,
                    EndDate = projectDB.EndDate,
                    tasks = projectDB.tasks
                };
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return project;
    }

    public List<TaskDTO> GetTasksByProject(int idProject)
    {
        List<TaskDTO> listTask = new List<TaskDTO>();
        try
        {
            listTask = dbContext.tasks
                .Where(t => t.IdProject == idProject)
                .Select(t => new TaskDTO {
                    IdTask = t.IdTask,
                    Name = t.Name,
                    Description = t.Description,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    IdProject = t.IdProject,
                    Status = "Hola"
                })
                .ToList();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }

        return listTask;
    }   
}