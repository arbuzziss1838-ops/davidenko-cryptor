@echo off
echo Building Stub...
"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" Stub\Stub.csproj /p:Configuration=Release /p:Platform=AnyCPU /m

echo.
echo Build complete.
pause
exit /b 0