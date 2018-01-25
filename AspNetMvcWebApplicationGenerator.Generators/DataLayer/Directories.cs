﻿namespace AspNetMvcWebApplicationGenerator.Generators.DataLayer
{
    using AspNetMvcWebApplicationGenerator.Configuration.DataLayer;
    using AspNetMvcWebApplicationGenerator.Generators.DataLayer.Helpers;

    internal class Directories
    {
        private void CreateTblDirectory()
        {
            StringFileWriter FileWriter = new StringFileWriter(DataConfiguration.OutputPath, "tblDirectory", OutputFileType.SqlScript);
            FileWriter.WriteString("CREATE TABLE tblDirectory (");
            FileWriter.WriteString("    Id BIGINT IDENTITY (1,1) NOT NULL PRIMARY KEY, ");
            FileWriter.WriteString("    EnumName NVARCHAR(16), ");
            FileWriter.WriteString("    IsReadOnly BIT ");
            FileWriter.WriteString(");");
            FileWriter.WriteString("GO");
            FileWriter.Close();
        }

        private void FillOneTblDirectoryRow(Directory dirItem, StringFileWriter fileWriter)
        {
            fileWriter.WriteString("");
            fileWriter.WriteString("INSERT INTO tblDirectory ");
            fileWriter.WriteString("    ( Id, EnumName, IsReadOnly )");
            fileWriter.WriteString("VALUES");
            fileWriter.WriteString("    ( " + dirItem.Id.ToString() + ", " + dirItem.EnumName + ", " + dirItem.IsReadOnly.ToString() + " );");
            fileWriter.WriteString("GO");
        }

        private void CreateTblDirectoryValue()
        {
            StringFileWriter FileWriter = new StringFileWriter(DataConfiguration.OutputPath, "tblDirectoryValue", OutputFileType.SqlScript);
            FileWriter.WriteString("CREATE TABLE tblDirectoryValue (");
            FileWriter.WriteString("    Id BIGINT IDENTITY (1,1) NOT NULL PRIMARY KEY, ");
            FileWriter.WriteString("    EnumName NVARCHAR(16), ");
            FileWriter.WriteString("    IsReadOnly BIT, ");
            FileWriter.WriteString("    DirectoryId BIGINT FOREIGN KEY REFERENCES tblDirectory(Id)");            
            FileWriter.WriteString(");");
            FileWriter.WriteString("GO");
            FileWriter.Close();
        }

        private void CreateIndexes4TblDirectory()
        {
            StringFileWriter FileWriter = new StringFileWriter(DataConfiguration.OutputPath, "idxDirectoryValue", OutputFileType.SqlScript);
            FileWriter.WriteString("CREATE INDEX idxDirectoryValue_DirectoryId ");
            FileWriter.WriteString("    ON tblDirectoryValue( DirectoryId ); ");
            FileWriter.WriteString("GO");
            FileWriter.Close();
        }

        private void FillOneTblDirectoryValue(DirectoryValue dirValue, StringFileWriter fileWriter)
        {
            fileWriter.WriteString("");
            fileWriter.WriteString("INSERT INTO tblDirectoryValue ");
            fileWriter.WriteString("    ( Id, EnumName, IsReadOnly, DirectoryId )");
            fileWriter.WriteString("VALUES");
            fileWriter.WriteString("    ( " + dirValue.Id.ToString() + ", " + dirValue.EnumName + ", " 
                                            + dirValue.IsReadOnly.ToString() + ", " + dirValue.DirectoryId.ToString() + " );");
            fileWriter.WriteString("GO");
        }
        
        private void CreateTblTranslatedString()
        {
            StringFileWriter FileWriter = new StringFileWriter(DataConfiguration.OutputPath, "tblTranslatedString", OutputFileType.SqlScript);
            FileWriter.WriteString("CREATE TABLE tblTranslatedString (");
            FileWriter.WriteString("    Id BIGINT IDENTITY (1,1) NOT NULL PRIMARY KEY, ");
            FileWriter.WriteString("    Language BIGINT, ");
            FileWriter.WriteString("    Type INT, ");
            FileWriter.WriteString("    ReferencedItemId BIGINT, ");
            FileWriter.WriteString("    Value NVARCHAR(MAX) ");
            FileWriter.WriteString(");");
            FileWriter.WriteString("GO");
            FileWriter.Close();
        }

        private void CreateIndexes4TblTranslatedString()
        {
            StringFileWriter FileWriter = new StringFileWriter(DataConfiguration.OutputPath, "idxTranslatedString", OutputFileType.SqlScript);
            FileWriter.WriteString("CREATE INDEX idxTranslatedString_Language ");
            FileWriter.WriteString("    ON tblTranslatedString( Language ); ");
            FileWriter.WriteString("GO");
            FileWriter.WriteString("");
            FileWriter.WriteString("CREATE INDEX idxTranslatedString_TypeReferencedItemId ");
            FileWriter.WriteString("    ON tblTranslatedString( Type, ReferencedItemId ); ");
            FileWriter.WriteString("GO");
            FileWriter.Close();
        }

        private void FillOneTblTranslatedString(TranslatedString transStr, StringFileWriter fileWriter)
        {
            fileWriter.WriteString("");
            fileWriter.WriteString("INSERT INTO tblTranslatedString ");
            fileWriter.WriteString("    ( Language, Type, ReferencedItemId, Value )");
            fileWriter.WriteString("VALUES");
            fileWriter.WriteString("    ( " + transStr.Language.ToString() + ", " + ((int)transStr.Type).ToString() + ", "
                                            + transStr.ReferencedItemId.ToString() + ", " + transStr.Value.ToString() + " );");
            fileWriter.WriteString("GO");
        }

        public void Generate()
        {
            CreateTblDirectory();

            CreateTblDirectoryValue();
            CreateIndexes4TblDirectory();

            CreateTblTranslatedString();
            CreateIndexes4TblTranslatedString();
            
            StringFileWriter FileWriter_tblDirectory = new StringFileWriter(DataConfiguration.OutputPath, "fill_tblDirectory", OutputFileType.SqlScript);
            StringFileWriter FileWriter_tblDirectoryValue = new StringFileWriter(DataConfiguration.OutputPath, "fill_tblDirectoryValue", OutputFileType.SqlScript);
            StringFileWriter FileWriter_tblTranslatedString = new StringFileWriter(DataConfiguration.OutputPath, "fill_tblTranslatedString", OutputFileType.SqlScript);

            foreach (var currDirectory in DataConfiguration.DirectoryConfigurations.Values)
            {
                FillOneTblDirectoryRow(currDirectory, FileWriter_tblDirectory);

                foreach (var currTranslatedString in currDirectory.TranslatedUINames)
                    FillOneTblTranslatedString(currTranslatedString, FileWriter_tblTranslatedString);

                foreach (var currDirectoryValue in currDirectory.Items.Values)
                {
                    FillOneTblDirectoryValue(currDirectoryValue, FileWriter_tblDirectoryValue);

                    foreach (var currTranslatedUIName in currDirectoryValue.TranslatedUINames)
                        FillOneTblTranslatedString(currTranslatedUIName, FileWriter_tblTranslatedString);

                    foreach (var currTranslatedUIComment in currDirectoryValue.TranslatedUIComments)
                        FillOneTblTranslatedString(currTranslatedUIComment, FileWriter_tblTranslatedString);
                }
            }
            
            FileWriter_tblTranslatedString.Close();
            FileWriter_tblDirectoryValue.Close();
            FileWriter_tblDirectory.Close();
        }
    }
}