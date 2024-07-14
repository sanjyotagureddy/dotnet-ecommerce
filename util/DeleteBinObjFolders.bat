@ECHO OFF
cls

SET "parentDir=.."  REM Set the parent directory path (adjust as needed)

ECHO Deleting all BIN and OBJ folders from %parentDir% ...
ECHO.

FOR /d /r "%parentDir%" %%d IN (bin,obj) DO (
	IF EXIST "%%d" (		 	 
		ECHO %%d | FIND /I "\node_modules\" > Nul && ( 
			ECHO.Skipping: %%d
		) || (
			ECHO.  Deleting: %%d
			rd /s/q "%%d"
		)
	)
)

ECHO.
ECHO BIN and OBJ folders have been successfully deleted from %parentDir%. Press any key to exit.
PAUSE > nul
