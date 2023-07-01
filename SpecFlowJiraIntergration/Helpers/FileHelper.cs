﻿using NUnit.Framework;
using System;

namespace SpecFlowJiraIntegration.Helpers
{
    public class FileHelper
    {
        public static string GetProjectFolderPath()
        {
            var dir = TestContext.CurrentContext.TestDirectory + "\\";
            var actualPath = dir.Substring(0, dir.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;

            return projectPath;
        }
    }
}
