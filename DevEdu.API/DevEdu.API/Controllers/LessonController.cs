﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : Controller
    {
        public LessonController()
        {

        }

        [HttpPost("lesson/{date}/{teacherComment}/{teacherId}")]
        public string AddLesson(DateTime date, String teacherComment, int teacherId)
        {
            return $"Date {date} TeacherComment {teacherComment}  TeacherId {teacherId}";
        }



    }
}
