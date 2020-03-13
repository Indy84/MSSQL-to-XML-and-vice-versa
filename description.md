# MSSQL-to-XML-and-vice-versa

The application should allow user to import data from the data base, save it on hard disk in XML format,
load XML files in given format from hard disk and export its data to data base.

The structure of data base:

Table "ODesign" (in the GUI the table should be named "formularze"):
1. "Id" (PK)
2. "Name" (up to 100 chars)
3. "Description" (up to 300 chars)
4. "Content" (any length)

Table "OConfigPack" (in the GUI the table should be named "paczki konfiguracyjne"):
1. "Id" (PK)
2. "Name" (up to 30 chars)
3. "Description" (up to 100 chars)

Table "RConfigPackDesign"
1. "Id" (PK)
2. "R1ConfigPackId (FK of OConfigPack)
3. "R2DesignId" (FK of ODesign)

Features:
1. The application should allow user to select exactly one record from "OConfigPack" table, select any number 
of related records from "ODesign" table, and then to save all selected records with relating RConfigPackDesign records to XML.

2. The application should allow user to load XML file from hard disk and export its data to data base.
