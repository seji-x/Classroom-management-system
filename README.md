# Classroom system management project

## Overview
[VN] Hệ thống quản lý lớp học bao gồm quản trị viên, giáo viên và học sinh. Quản trị viên có trách nhiệm quản lý tài khoản, phân quyền, và quản lý thông tin lớp học. Giáo viên có thể quản lý thông tin lớp, điểm số, và tương tác với học sinh. Học sinh có thể xem thông tin cá nhân, kết quả học tập và tham gia các khóa học.

[EN] The classroom management system includes administrators, teachers, and students. Administrators are responsible for account management, authorization, and class information management. Teachers can manage class information, scores, and interact with students. Students can view personal information, academic results, and take courses.

## Implementation environment
- Framework: https://dotnet.microsoft.com/en-us/apps/aspnet
- Integrated Development Environment: https://visualstudio.microsoft.com/
- Language: C#, Js, Css, Html

## Database
- Use: SQL SEVER / https://learn.microsoft.com/en-us/sql/ssms
- Demo: https://github.com/ddryuu/Information-system-design/blob/main/QuanLyLop.sql

## Theoretical basis
- Introducing .NET Technology: [Asp.NET MVC](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/overview/asp-net-mvc-overview)
> ASP.NET Core is a collection of libraries as a new Framework for building applications
web when there is an internet connection, it is a Microsoft product that is quite famous in the community
co-programming nowadays when there are so many bloggers, tech vblogs showing attention and
care about it. As soon as it appeared, there were a series of ASP.NET Core tutorials and articles
Comparative writing, instructions, and discussions are dissected.
- Build ASP.NET Core Web UI and ASP.NET Core Web API using [ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio)
> For a long time, web programming has always been the playground of PHP, but with its appearance
ASP.NET Core has proven its power enough to compete with him
large PHP. And here are the reasons why .NET can confidently compare with PHP.
The Web applications you create can be tested according to the MVC (Model-View
Controller)
Razor provides us with an efficient language to create Views
Tag Helper for server side code involved in creating and rendering HTML elements
Automatically map data from the HTTP request to the parameters of the above action method
Model Binding.
Model Validation automatically performs validation and the server
- Developed on client-side: https://developer.mozilla.org/en-US/docs/Learn/Tools_and_testing/Understanding_client-side_tools
> ASP.NET Core is designed to seamlessly integrate with multiple client-sides
frameworks, including AngularJS, KnockoutJS and Bootstrap.

## System analysis and design
### Use case specification
1. Xác định tác nhân
- Học sinh (user)
- Quản lý (admin)
- Giáo viên (user)
2. Xác định Use Case
```
ADMIN
- Use Case đăng nhập (login)
- Use Case đăng xuất (logout)
- Use Case quản lí tài khoản học sinh
- Use Case quản lí tài khoản giáo viên
- Use Case quản lí tài khoản admin
- Use Case quản lí môn học
- Use Case quản lý lớp
- Use Case quản lí Điểm
- Use Case quản lí học sinh
- Use Case quản lí giáo viên
Hoc sinh
- Use Case đăng nhập (login)
- Use Case đăng xuất (logout)
- Use Case xem điểm
- Use Case xem thông tin
Giao Vien
- Use Case đăng nhập (login)
- Use Case đăng xuất (logout)
- Use Case xem danh sách hs
- Use Case xem lịch dạy
```

