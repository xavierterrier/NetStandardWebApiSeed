using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using WebAPIToolkit.Dtos;
using WebAPIToolkit.Model;

namespace WebAPIToolkit.DtoMappers
{
    public static class AutoMapperMapping
    {
        public static void Configure()
        {
            Mapper.CreateMap<Project, ProjectDto>();
            Mapper.CreateMap<ProjectDto, Project>();

            Mapper.CreateMap<ProjectTask, TaskDto>();
            Mapper.CreateMap<TaskDto, ProjectTask>();

            //Mapper.CreateMap<TeachingSchoolSubject, TeachingSchoolSubjectDto>()
            //    .ForMember(d => d.Name, opt => opt.MapFrom(u => u.SchoolSubject != null ? u.SchoolSubject.Name : null))
            //    .ForMember(d => d.Group, opt => opt.MapFrom(u => u.SchoolSubject != null ? u.SchoolSubject.Group : null));

            //Mapper.CreateMap<BusinessPreferences, OpenHoursDto>().ConvertUsing<BusinessPreferencesToOpenHoursDto>();

        }


    }
}