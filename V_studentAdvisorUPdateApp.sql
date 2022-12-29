USE [CAMS_Enterprise]
GO

/****** Object:  View [dbo].[V_StudentAdvisorUpdateApp]    Script Date: 9/8/2022 2:34:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[V_StudentAdvisorUpdateApp]
AS
SELECT     dbo.Student.StudentID, dbo.Student.StudentID + '-' + ISNULL(dbo.Student.FirstName, '') + ' ' + ISNULL(dbo.Student.MiddleInitial, '') 
                      + ' ' + ISNULL(dbo.Student.LastName, '') AS studname, dbo.StudentStatus.TermCalendarID, dbo.StudentProgram.AdvisorID, dbo.MajorMinor.MajorMinorName, 
                      ISNULL(dbo.Advisors.FirstName, '') + ' ' + ISNULL(dbo.Advisors.MiddleName, '') + ' ' + ISNULL(dbo.Advisors.LastName, '') AS AdvisorName, 
                      dbo.StudentStatus.StudentRegistered, dbo.StudentAudProg.CreditsCompleted, dbo.StudentAudProg.GPAAttained, dbo.EnrollmentStatus.Status AS EnrollmentStatus, 
                      dbo.MajorMinor.MajorMinorID, dbo.TermCalendar.Term, dbo.TermCalendar.TextTerm
FROM         dbo.EnrollmentStatus INNER JOIN
                      dbo.Student INNER JOIN
                      dbo.StudentStatus ON dbo.Student.StudentUID = dbo.StudentStatus.StudentUID INNER JOIN
                      dbo.StudentProgram ON dbo.StudentStatus.StudentStatusID = dbo.StudentProgram.StudentStatusID INNER JOIN
                      dbo.MajorMinor ON dbo.StudentProgram.MajorProgramID = dbo.MajorMinor.MajorMinorID INNER JOIN
                      dbo.Advisors ON dbo.StudentProgram.AdvisorID = dbo.Advisors.AdvisorID ON 
                      dbo.EnrollmentStatus.EnrollmentStatusID = dbo.StudentStatus.EnrollmentStatusID INNER JOIN
                      dbo.TermCalendar ON dbo.StudentStatus.TermCalendarID = dbo.TermCalendar.TermCalendarID LEFT OUTER JOIN
                      dbo.StudentAudProg ON dbo.Student.StudentUID = dbo.StudentAudProg.StudentUID AND dbo.MajorMinor.MajorMinorID = dbo.StudentAudProg.MajorMinorID
GO


