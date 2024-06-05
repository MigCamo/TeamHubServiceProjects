
using TeamHubServiceProjects.DTOs;
using TeamHubServiceProjects.Entities;

namespace TeamHubServiceProjects.Gateways.Interfaces;

public interface IProjectServices 
{
    public bool AddProject(project projectNew);
    public bool UpdateProject(project projectUpdate);
    public bool DeleteProject(int projectId);
    public List<project> GetProjects();
    public project GetProject(int IdProject);
    public List<TaskDTO> GetTasksByProject(int idProject); 
}