﻿using DevEdu.Business.Exceptions;
using DevEdu.DAL.Models;
using DevEdu.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using DevEdu.Business.ValidationHelpers;

namespace DevEdu.Business.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserValidationHelper _userValidationHelper;
        private readonly ILessonValidationHelper _lessonValidationHelper;
        private readonly ITopicValidationHelper _topicValidationHelper;

        public LessonService(
            ILessonRepository lessonRepository,
            ICommentRepository commentRepository,
            IUserRepository userRepository,
            IUserValidationHelper userValidationHelper,
            ILessonValidationHelper lessonValidationHelper,
            ITopicValidationHelper topicValidationHelper
        )
        {
            _lessonRepository = lessonRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _userValidationHelper = userValidationHelper;
            _lessonValidationHelper = lessonValidationHelper;
            _topicValidationHelper = topicValidationHelper;
        }

        public void AddCommentToLesson(int lessonId, CommentDto commentDto)
        {
            int commentId =_commentRepository.AddComment(commentDto);

            _lessonRepository.AddCommentToLesson(lessonId, commentId);
        }

        public int AddLesson(LessonDto lessonDto, List<int> topicIds)
        {
            int lessonId = _lessonRepository.AddLesson(lessonDto);

            if(topicIds != null)
            {
               foreach(int topicId in topicIds)
                {
                    _topicValidationHelper.CheckTopicExistence(topicId);
                    topicIds.ForEach(topicId => _lessonRepository.AddTopicToLesson(lessonId, topicId));
                }                
            }

            return lessonId;
        }

        public void DeleteCommentFromLesson(int lessonId, int commentId) => _lessonRepository.DeleteCommentFromLesson(lessonId, commentId);

        public void DeleteLesson(int id)
        {
            _lessonValidationHelper.CheckLessonExistence(id);
            _lessonRepository.DeleteLesson(id);
        }

        public List<LessonDto> SelectAllLessonsByGroupId(int id)
        {
            _lessonValidationHelper.CheckLessonExistence(id);
            return _lessonRepository.SelectAllLessonsByGroupId(id);
        }

        public List<LessonDto> SelectAllLessonsByTeacherId(int id)
        {
            _lessonValidationHelper.CheckLessonExistence(id);
            return _lessonRepository.SelectAllLessonsByTeacherId(id);
        }

        public LessonDto SelectLessonById(int id)
        {
            _lessonValidationHelper.CheckLessonExistence(id);
            return _lessonRepository.SelectLessonById(id);
        }

        public LessonDto SelectLessonWithCommentsById(int id)
        {
            LessonDto result = _lessonRepository.SelectLessonById(id);

            result.Comments = _commentRepository.SelectCommentsFromLessonByLessonId(id);

            return result;
        }

        public LessonDto SelectLessonWithCommentsAndStudentsById(int id)
        {
            _lessonValidationHelper.CheckLessonExistence(id);
            LessonDto result = SelectLessonWithCommentsById(id);
            //check comments
            result.Students = _lessonRepository.SelectStudentsLessonByLessonId(id);

            return result;
        }

        public LessonDto UpdateLesson(LessonDto lessonDto, int id)
        {
            _lessonValidationHelper.CheckLessonExistence(id);
            lessonDto.Id = id;
            _lessonRepository.UpdateLesson(lessonDto);
            return _lessonRepository.SelectLessonById(lessonDto.Id);
        }

        public void DeleteTopicFromLesson(int lessonId, int topicId) => 
            _lessonRepository.DeleteTopicFromLesson(lessonId, topicId);

        public void AddTopicToLesson(int lessonId, int topicId) =>
            _lessonRepository.AddTopicToLesson(lessonId, topicId);

        public void AddStudentToLesson(int lessonId, int userId)
        {
            var studentLessonDto =
               new StudentLessonDto
               {
                   User = new UserDto { Id = userId },
                   Lesson = new LessonDto { Id = lessonId }
               };
            _lessonRepository.AddStudentToLesson(studentLessonDto);
        }

        public void DeleteStudentFromLesson(int lessonId, int userId)
        {
            var studentLessonDto =
                new StudentLessonDto
                {
                    User = new UserDto { Id = userId },
                    Lesson = new LessonDto { Id = lessonId }
                };
            _lessonRepository.DeleteStudentFromLesson(studentLessonDto);
        }

        public void UpdateStudentFeedbackForLesson(int lessonId, int userId, StudentLessonDto studentLessonDto)
        {
            _userValidationHelper.CheckUserExistence(userId);
            _lessonValidationHelper.CheckLessonExistence(lessonId);

            // check if user relates to lesson
            /*
            I.
                var studentLesson = _lessonRepository.GetStudentLessonByStudentAndLesson(userId, lessonId);
                if (studentLesson == default)
                    throw new AuthorizationException($"user with id = {userId} doesn't relate to lesson with id = {lessonId}");
            II.
                var groupsInLesson = _groupRepository.GetGroupsByLessonId(lessonId);
                var studentGroups = _groupRepository.GetGroupsByStudentId(userId);
                var result = groupsInLesson.Where(gl => studentGroups.Any(gs => gs.Id == gl.Id));
                if (result == default)
                    throw new AuthorizationException($"user with id = {userId} doesn't relate to lesson with id = {lessonId}");
            */

            studentLessonDto.Lesson = new LessonDto { Id = lessonId };
            studentLessonDto.User = new UserDto { Id = userId };
            _lessonRepository.UpdateStudentFeedbackForLesson(studentLessonDto);
        }

        public void UpdateStudentAbsenceReasonOnLesson(int lessonId, int userId, StudentLessonDto studentLessonDto)
        {
            studentLessonDto.Lesson = new LessonDto { Id = lessonId };
            studentLessonDto.User = new UserDto { Id = userId };
            _lessonRepository.UpdateStudentAbsenceReasonOnLesson(studentLessonDto);
        }

        public void UpdateStudentAttendanceOnLesson(int lessonId, int userId, StudentLessonDto studentLessonDto)
        {
            studentLessonDto.Lesson = new LessonDto { Id = lessonId };
            studentLessonDto.User = new UserDto { Id = userId };
            _lessonRepository.UpdateStudentAttendanceOnLesson(studentLessonDto);
        }

        public List<StudentLessonDto> SelectAllFeedbackByLessonId(int lessonId)=>
            _lessonRepository.SelectAllFeedbackByLessonId(lessonId);

        public StudentLessonDto GetStudenLessonByLessonAndUserId(int lessonId, int userId) =>
            _lessonRepository.SelectByLessonAndUserId(lessonId, userId);
    }
}