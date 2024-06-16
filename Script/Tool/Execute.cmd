@echo off

set ToolName=%~1
set Arg0=%~2
set ModuleName=Z.Tool.%ToolName%
pushd Out\net8.0
dotnet %ModuleName%.dll %Arg0%
echo Status: %errorlevel%
popd