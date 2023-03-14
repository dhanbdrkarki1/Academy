
create database AcademyDB
use AcademyDB

create table UserAccount(
	AccountId int primary key identity(1,1),
	FullName varchar(200) null,
	Email varchar(150) not null unique,
	Username varchar(150) not null unique,
	AccountType varchar(50) not null,
	Password varchar(50) not null,
	Profile_Img varchar(400)
)

create table CourseCategory(
	CourseCatId int primary key identity(1,1),
	Category varchar(250) not null unique,
)

 insert into CourseCategory values('Computer Science')
 insert into CourseCategory values('Data Science')
 insert into CourseCategory values('Business')
 insert into CourseCategory values('Personality Development')
 insert into CourseCategory values('Information Technology')

create table Courses(
	CourseId int primary key identity(1,1),
	Title varchar(200) not null,
	Category int not null references CourseCategory(CourseCatId),
	OverView varchar(400) null,
	Rate int not null,
	CreatedAt date not null,
	InstructorId int not null references UserAccount(AccountId),
)

 insert into Courses values('Java Programming: Solving Problems with Software',1,'Learn to code in Java and improve your programming and problem-solving skills', 10,'2022-02-03')
 insert into Courses Values('Interactivity with JavaScript', 1, 'This is the third course in the Web Design For Everybody specialization.  A basic understanding of HTML and CSS is expected when you enroll in this class.    Additional courses focus on enhancing the styling with responsive design and completing a capstone project.', 15, '2022-04-05')


-- select CourseID, Courses.Title, CourseCategory.Category, OverView, Rate, CratedAt  from Courses inner join CourseCategory on Courses.Category = CourseCategory.CourseCatId 



create table CourseContent(
	CourseContId int primary key identity(1,1),
	CId int not null references Courses(CourseId),
	ContTitle varchar(400) not null,
	TextCont varchar(1000) not null,
	ImageCont varchar(300) null,
	FileCont varchar(300) null,
	ContentUrl varchar(400) null,
)



create table EnrollCourse(
	EnrollId int primary key identity(1,1),
	CourseId int not null references Courses(CourseId),
	StudentId int not null references UserAccount(AccountId),
	InstructorId int not null references UserAccount(AccountId),
	TotalAmt int not null,
	EnrollmentDate date not null,
)

-- drop table EnrollCourse