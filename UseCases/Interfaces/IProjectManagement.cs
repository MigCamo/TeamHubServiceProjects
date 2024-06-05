
using TeamHubServiceProjects.DTOs;
using TeamHubServiceProjects.Entities;

namespace TeamHubServiceProjects.UseCases.Interfaces
{
    public interface IProjectManagement
    {
        public List<project> GetAllProjects();
        public bool AddProject(project project);
        public bool RemoveProject(int projectID);
        public bool UpdateProject(project projectUpdate);
        public project GetProject(int IdProject);
        public List<TaskDTO> GetTasksByProject(int idProject); 
    }
}