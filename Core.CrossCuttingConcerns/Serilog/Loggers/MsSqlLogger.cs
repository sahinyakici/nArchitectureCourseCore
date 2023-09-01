using Core.CrossCuttingConcerns.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.Serilog.Loggers;

public class MsSqlLogger:LoggerServiceBase
{
    public MsSqlLogger(IConfiguration configuration)
    {
        MsSqlConfiguration msSqlLogConfiguration =
            configuration.GetSection("SerilogLogConfigurations:MsSqlConfiguration").Get<MsSqlConfiguration>() ??
            throw new Exception(SerilogMessages.NullOptionsMessage);
        MSSqlServerSinkOptions msSqlServerSinkOptions = new()
        {
            TableName = msSqlLogConfiguration.TableName,
            AutoCreateSqlDatabase = msSqlLogConfiguration.AutoCreateSqlTable,
        };
        ColumnOptions columnOptions = new();

        Logger seriLogConfig = new LoggerConfiguration().WriteTo.MSSqlServer(msSqlLogConfiguration.ConnectionString,msSqlServerSinkOptions,columnOptions:columnOptions).CreateLogger();
        Logger = seriLogConfig;
    }
}