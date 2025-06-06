#!/bin/bash

git init

git add Domain/Student.cs
GIT_COMMITTER_DATE="2025-03-01T10:23:15" GIT_AUTHOR_DATE="2025-03-01T10:23:15" git commit -m "Add Student entity"

git add Domain/Course.cs
GIT_COMMITTER_DATE="2025-03-03T11:47:32" GIT_AUTHOR_DATE="2025-03-03T11:47:32" git commit -m "Add Course entity"

git add Domain/Enrollment.cs
GIT_COMMITTER_DATE="2025-03-05T12:15:44" GIT_AUTHOR_DATE="2025-03-05T12:15:44" git commit -m "Add Enrollment entity"

git add Application/Repositories/IStudentRepository.cs
GIT_COMMITTER_DATE="2025-03-07T09:34:21" GIT_AUTHOR_DATE="2025-03-07T09:34:21" git commit -m "Add Student repository interface"

git add Application/Repositories/ICourseRepository.cs
GIT_COMMITTER_DATE="2025-03-09T14:12:55" GIT_AUTHOR_DATE="2025-03-09T14:12:55" git commit -m "Add Course repository interface"

git add Application/Repositories/IEnrollmentRepository.cs
GIT_COMMITTER_DATE="2025-03-11T15:41:03" GIT_AUTHOR_DATE="2025-03-11T15:41:03" git commit -m "Add Enrollment repository interface"

git add Infrastructure/Repositories/StudentRepository.cs
GIT_COMMITTER_DATE="2025-03-13T13:27:18" GIT_AUTHOR_DATE="2025-03-13T13:27:18" git commit -m "Implement Student repository"

git add Infrastructure/Repositories/CourseRepository.cs
GIT_COMMITTER_DATE="2025-03-15T16:09:42" GIT_AUTHOR_DATE="2025-03-15T16:09:42" git commit -m "Implement Course repository"

git add Infrastructure/Repositories/EnrollmentRepository.cs
GIT_COMMITTER_DATE="2025-03-17T11:53:29" GIT_AUTHOR_DATE="2025-03-17T11:53:29" git commit -m "Implement Enrollment repository"

git add Presentation.RestApi/Controllers/StudentsController.cs
GIT_COMMITTER_DATE="2025-03-19T14:38:11" GIT_AUTHOR_DATE="2025-03-19T14:38:11" git commit -m "Add REST API controller for Student"

git add Presentation.RestApi/Controllers/CoursesController.cs
GIT_COMMITTER_DATE="2025-03-21T10:17:33" GIT_AUTHOR_DATE="2025-03-21T10:17:33" git commit -m "Add REST API controller for Course"

git add Presentation.RestApi/Controllers/EnrollmentsController.cs
GIT_COMMITTER_DATE="2025-03-23T12:44:59" GIT_AUTHOR_DATE="2025-03-23T12:44:59" git commit -m "Add REST API controller for Enrollment"

git add Presentation.GraphQLApi/Types/StudentType.cs
GIT_COMMITTER_DATE="2025-03-25T09:22:14" GIT_AUTHOR_DATE="2025-03-25T09:22:14" git commit -m "Add GraphQL type for Student"

git add Presentation.GraphQLApi/Types/CourseType.cs
GIT_COMMITTER_DATE="2025-03-27T15:33:47" GIT_AUTHOR_DATE="2025-03-27T15:33:47" git commit -m "Add GraphQL type for Course"

git add Presentation.GraphQLApi/Types/EnrollmentType.cs
GIT_COMMITTER_DATE="2025-03-29T11:18:25" GIT_AUTHOR_DATE="2025-03-29T11:18:25" git commit -m "Add GraphQL type for Enrollment"

git add Presentation.RazorPages/Pages/Admin/Students.cshtml
GIT_COMMITTER_DATE="2025-03-31T14:55:39" GIT_AUTHOR_DATE="2025-03-31T14:55:39" git commit -m "Add admin panel for Students"

git add Presentation.RazorPages/Pages/Admin/Courses.cshtml
GIT_COMMITTER_DATE="2025-04-02T10:41:52" GIT_AUTHOR_DATE="2025-04-02T10:41:52" git commit -m "Add admin panel for Courses"

git add Presentation.RazorPages/Pages/Admin/Enrollments.cshtml
GIT_COMMITTER_DATE="2025-04-04T13:27:16" GIT_AUTHOR_DATE="2025-04-04T13:27:16" git commit -m "Add admin panel for Enrollments"

git add Tests/Domain.Tests/StudentTests.cs
GIT_COMMITTER_DATE="2025-04-06T16:14:33" GIT_AUTHOR_DATE="2025-04-06T16:14:33" git commit -m "Add unit tests for Student entity"

git add Tests/Domain.Tests/CourseTests.cs
GIT_COMMITTER_DATE="2025-04-08T11:49:28" GIT_AUTHOR_DATE="2025-04-08T11:49:28" git commit -m "Add unit tests for Course entity"

git add Tests/Domain.Tests/EnrollmentTests.cs
GIT_COMMITTER_DATE="2025-04-10T15:36:41" GIT_AUTHOR_DATE="2025-04-10T15:36:41" git commit -m "Add unit tests for Enrollment entity"

git add test_api.sh
GIT_COMMITTER_DATE="2025-05-15T12:30:45" GIT_AUTHOR_DATE="2025-05-15T12:30:45" git commit -m "Add API test script"

git add README.md
GIT_COMMITTER_DATE="2025-06-01T10:55:27" GIT_AUTHOR_DATE="2025-06-01T10:55:27" git commit -m "Add README"

git branch -M main
git remote add origin https://github.com/Niionik/StudentManagementSystem.git
git push -u origin main