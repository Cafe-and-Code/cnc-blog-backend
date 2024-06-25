docker pull mcr.microsoft.com/azure-sql-edge

docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=admin12345" -e "MSSQL_PID=Developer" -e "MSSQL_USER=sa" -p 1433:1433 -d --name=cncsqlserver2022 mcr.microsoft.com/azure-sql-edge