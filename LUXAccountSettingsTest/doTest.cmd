@echo off 
setlocal EnableDelayedExpansion 
cls
color 2F
echo ===================================================================
echo Automation Test                 
echo ===================================================================
echo.
for /l %%i in (1,1,10000) do echo %%i >nul

:begin
echo.
set /p varDVT=Do you want to execute the DVT test?  (Y,N):
if not defined varDVT goto begin
echo %varDVT%|findstr "[^nyNY]" >nul&&goto begin
if /i %varDVT%==n echo.&echo DVT is set to OFF...&echo.&goto envChoice2
if /i %varDVT%==y echo.&echo DVT is set to ON...&echo.&goto envChoice1

:envChoice1
echo ==================================================================
=
set /p varQ1=Please enter the NO. for the testing environment you want for DVT (1:qa7, 2:maui, 3:prod, 4:local):
if not defined varQ1 goto envChoice1
echo %varQ1%|findstr "[^1-4]" >nul&&goto envChoice1
if /i %varQ1% equ 1 (set varEnv=qa7&&echo.&echo The testing environment is set to Lisqa7...)
if /i %varQ1% equ 2 (set varEnv=maui&&echo.&echo The testing environment is set to INT-Maui...)
if /i %varQ1% equ 3 (set varEnv=prod&&echo.&echo The testing environment is set to Production...)
if /i %varQ1% equ 4 (set varEnv=local&&echo.&echo The testing environment is set to Local...)
goto browserChoice

:envChoice2
echo ==================================================================
set /p varQ2=Please enter the NO. for the testing environment you want (1:Lisqa7, 2:Maui, 3:Local):
if not defined varQ2 goto envChoice2
echo %varQ2%|findstr "[^1-3]" >nul&&goto envChoice2
if /i %varQ2% equ 1 (set varEnv=qa7&&echo.&echo The testing environment is set to LISQA7...)
if /i %varQ2% equ 2 (set varEnv=maui&&echo.&echo The testing environment is set to INT-Maui...)
if /i %varQ2% equ 3 (set varEnv=local&&echo.&echo The testing environment is set to Local...)

:caseChoice
echo ==================================================================
cd src\test\java\com\expedia\lux\promotionstest\tests\fst
dir /w/b
echo ==================================================================
echo All the test cases for functionality are listed above...&echo.
for /l %%i in (1,1,10000) do echo %%i >nul
set /p varCase=Please type the full name of the case without suffix you want to test:
if not defined varCase goto caseChoice&&echo.
for /f "delims=" %%i in ('dir /b *.java') do if /i %varCase:~0,-5%==%%~ni echo Ready to test %%~ni in cloud...&pause&cd..>nul&cd..>nul&cd..>nul&cd..>nul&cd..>nul&cd..>nul&cd..>nul&cd..>nul&cd..>nul&&goto browserChoice
echo No case found in list with this name!&echo Retry please...&pause&echo.&goto caseChoice
 
:browserChoice
echo.
echo ==================================================================
set /p varQ3=Please enter the NO. for the testing browser you want (1:Firefox, 2:IE7, 3:IE8, 4:IE9, 5:IE10, 6:IE11, 7:Chrome, 8:Safari):
if not defined varQ3 goto browserChoice
echo %varQ3%|findstr "[^1-8]" >nul&&goto browserChoice
if /i %varQ3% equ 1 (set varBrowser=ff&&echo.&echo The testing browser is set to Firefox...)
if /i %varQ3% equ 2 (set varBrowser=ie7&&echo.&echo The testing browser is set to Internet Explorer 7...)
if /i %varQ3% equ 3 (set varBrowser=ie8&&echo.&echo The testing browser is set to Internet Explorer 8...)
if /i %varQ3% equ 4 (set varBrowser=ie9&&echo.&echo The testing browser is set to Internet Explorer 9...)
if /i %varQ3% equ 5 (set varBrowser=ie10&&echo.&echo The testing browser is set to Internet Explorer 10...)
if /i %varQ3% equ 6 (set varBrowser=ie11&&echo.&echo The testing browser is set to Internet Explorer 11...)
if /i %varQ3% equ 7 (set varBrowser=cr&&echo.&echo The testing browser is set to Chrome...)
if /i %varQ3% equ 8 (set varBrowser=sf&&echo.&echo The testing browser is set to Safari...)
echo.
 
:ready
echo ======================================================================
echo Preparing to execute the automation testing...
echo ==================================================================
echo.&pause
if defined varEnv (if defined varBrowser (if defined varCase goto doTest2 else goto doTest1) else goto exception) else goto exception)

:doTest1
echo ==================================================================
echo Type: DVT Test    Environment:  %varEnv%     Browser: %varBrowser%
echo.
pause
for /l %%i in (1,1,12000) do echo %%i >nul
mvn -Dtest=DVTTests test -Denv=%varEnv% -Dbrowser=%varBrowser% -Dcloud=true -DskipTests=false

:doTest2
echo ==================================================================
echo Type: Functional Test   Case: %varCase%  Environment:  %varEnv%   Browser: %varBrowser%
echo.
pause
for /l %%i in (1,1,12000) do echo %%i >nul
mvn -Dtest=%varCase% test -Denv=%varEnv% -Dbrowser=%varBrowser% -Dcloud=true -DskipTests=false

:exception
echo.
echo ******************************************************************************
echo Wrong input for the parameters!&echo.
set /p varAgain=Try again?(Y,N):
if not defined varAgain goto begin
echo %varAgain%|findstr "[^nyNY]" >nul&&goto exit
if /i %varAgain%==y goto begin else goto exit

:exit
echo.
set /p varExit=Finish the test? (Y,N):
if not defined varExit exit
echo %varExit%|findstr "[^nyNY]" >nul&&goto begin
if /i %varExit%==y exit else goto begin