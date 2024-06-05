

using TeamHubServiceProjects.DTOs;
using TeamHubServiceProjects.Entities;
using TeamHubServiceProjects.Gateways.Interfaces;
using TeamHubServiceProjects.UseCases.Interfaces;

namespace TeamHubServiceProjects.UseCases.Providers
{
    public class ProjectManagement : IProjectManagement
    {
        private IProjectServices projectServices;

        public ProjectManagement(IProjectServices projectServices)
        {
            this.projectServices = projectServices;
        }
        public List<project> GetAllProjects()
        {
            return projectServices.GetProjects();
        }

        public bool AddProject(project project)
        {
            project projectNew = new project() 
            {
                IdProject = 0,
                Name = project.Name,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                projectstudent = null,
            };
            return projectServices.AddProject(projectNew);
        }

        public bool RemoveProject(int projectID)
        {
            return projectServices.DeleteProject(projectID);
        }

        public bool UpdateProject(project projectUpdate)
        {
            project projectNew = new project() 
            {
                IdProject = 0,
                Name = projectUpdate.Name,
                StartDate = projectUpdate.StartDate,
                EndDate = projectUpdate.EndDate,
                projectstudent = null,
            };
            return projectServices.UpdateProject(projectNew);
        }

        public project GetProject(int IdProject)
        {
            project project = new project();
            if (IdProject > 0){
                project = projectServices.GetProject(IdProject);
            }

            return project;
        }

        public List<TaskDTO> GetTasksByProject(int idProject)
        {
            return projectServices.GetTasksByProject(idProject);
        }
    } 
}