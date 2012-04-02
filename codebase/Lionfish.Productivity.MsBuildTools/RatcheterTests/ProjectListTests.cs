// created by: Östen Petersson (SIS)
// at: 08:54: 15/03: 2012
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using Ratcheter;

namespace RatcheterTests
{
    using NUnit.Framework;
    using Rhino;

    [TestFixture, Ignore]
    public class ProjectListTests
    {
        [Test]
        public void AProjectListContainsCreatedDate()
        {
            var projectList = new ProjectList();
            Assert.IsTrue(projectList.CreatedDate == DateTime.Now.ToShortDateString());
        }

        [Test]
        public void AProjectListContainsOneOfTheParameterLists()
        {


            //act

            //assert

        }
    }


}