// created by: Östen Petersson 
// at: 08:53: 15/03: 2012
// ------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace  Ratcheter
{
    [Obsolete]
    public class ProjectList
    {
        private DateTime _createdDate;
        private List<IProject> _projects;

        public ProjectList()
        {
            _createdDate = DateTime.Now;
        }

        public string CreatedDate
        {
            get {
                return _createdDate.ToShortDateString() ;
            }
        }

        public List<IProject> Projects
        {
            get {
                return _projects;
            }
            set {
                _projects = value;
            }
        }
    }
}