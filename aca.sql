drop table UserAccount



select Courses.Title, CourseCategory.Category, OverView, Rate, CreatedAt from Courses inner join CourseCategory on Courses.Category=CourseCategory.CourseCatId