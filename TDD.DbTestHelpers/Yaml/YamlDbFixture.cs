using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using System.Text;
using TDD.DbTestHelpers.Core;
using TDD.DbTestHelpers.EF;
using TDD.DbTestHelpers.Helpers;
using YamlDotNet.RepresentationModel.Serialization;

namespace TDD.DbTestHelpers.Yaml
{
    public class YamlDbFixture<TContext, TFixtureType> : DbFixture<TContext> where TContext : DbContext, new()
    {
        private readonly FileHelper _fileHelper;
        private string _yamlFolderName = "Fixtures";
        private string[] _yamlFilesNames = new[] {"fixtures.yaml"};

        public YamlDbFixture()
            : this(new FileHelper())
        {

        }

        public YamlDbFixture(FileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }

        public override void PrepareDatabase()
        {
            _fileHelper.ClearTables<TFixtureType>(Context);
        }

        public override void FillFixtures()
        {
            _fileHelper.FillFixturesFileFiles<TFixtureType>(Context, _yamlFolderName, _yamlFilesNames);
        }


        protected void SetYamlFolderName(string yamlFolderName)
        {
            _yamlFolderName = yamlFolderName;
        }

        protected void SetYamlFiles(params string[] yamlFiles)
        {
            _yamlFilesNames = yamlFiles;
        }
    }
}
