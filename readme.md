Schemazen - Script and create SQL Server objects quickly
--------------------------------------------------------

Schemazen has two commands:

**script**

    schemazen.exe script --server localhost --database db --scriptDir c:\somedir

This will generate sql scripts for all objects in the database in the
following directory structure:

    c:\somedir\
      foreign_keys
	  functions
	  procs
	  tables
	  triggers
	  props.sql

**create**

    schemazen.exe create --server localhost --database db --scriptDir c:\somedir

This will create a database named db from the sql scripts in c:\somedir.

