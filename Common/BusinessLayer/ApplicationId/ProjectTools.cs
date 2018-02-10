﻿namespace Common.BusinessLayer.ApplicationId
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.DataTransferObjects.ApplicationId;

    public enum enProject
    {
        Common = 1,
        WebAppExample = 2
    }

    public static class ProjectTools
    {
        private static Project ConvertEnum( enProject en )
        {
            Project result = new Project( (long)(en), en.ToString() );
            return result;
        }

        private static Dictionary<long, Project> Projects { get; }
        
        static ProjectTools()
        {
            Projects = new Dictionary<long, Project>();
            foreach (enProject currEn in Enum.GetValues(typeof(enProject)) )
            {
                Project currItem = ConvertEnum(currEn);
                Projects.Add( currItem.Id, currItem );
            }
        }

        public static Project GetOneById(long id)
        {
            Project result = Projects[id];
            return result;
        }
        
        public static List<Project> GetAll()
        {
            var list = Projects.Values.ToList();
            return list;
        }
    }
}
