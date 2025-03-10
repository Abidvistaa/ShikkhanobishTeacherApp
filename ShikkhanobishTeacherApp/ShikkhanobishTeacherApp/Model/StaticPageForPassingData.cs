﻿using Flurl.Http;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShikkhanobishTeacherApp.Model
{
    public class StaticPageForPassingData
    {
        public static Teacher thisTeacher { get; set; }
        public static List<TeacherTuitionHistory> tuitionList { get; set; }
        public static List<Subject> allSubList { get; set; }
        public static setTeacher ThisRegTeacher { get; set; }
        public static CousrList thisTeacherCourseList { get; set; }
        public static List<SubList> thisTeacherSubListName { get; set; }
        public static bool isTeacherOnBackground { get; set; }
        public static TuitionFoundClass tuitionFoundClass { get; set; }
        public static bool isTeacherAlive { get; set; }
        public static bool isTuitionFound { get; set; }
        public static bool freomReg { get; set; }
        public static int thisVideoCallStudentID { get; set; }
        public static TuitionEvent regEvent = new TuitionEvent();
        public static bool LoginOK { get; set; }
       static HubConnection _connection = null;
       static string url = "https://shikkhanobishRealTimeAPi.shikkhanobish.com/ShikkhanobishHub";
        public static int GenarateNewID()
        {
            Random rnd = new Random();
            int newID = rnd.Next(10000000, 99999999);
            return newID;
        }
        public static async Task GetALlTeacherInfo(string pass, string pn)
        {
            thisTeacher = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/loginTeacher".PostUrlEncodedAsync(new { password = pass, phonenumber = pn })
      .ReceiveJson<Teacher>();

            if(thisTeacher.teacherID  != 0)
            {
                thisTeacherCourseList = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/getCousrListWithID".PostUrlEncodedAsync(new { teacherID = thisTeacher.teacherID })
                      .ReceiveJson<CousrList>();


                thisTeacherSubListName = await "https://api.shikkhanobish.com/api/ShikkhanobishTeacher/GetSubListInfo".PostUrlEncodedAsync(new
                {
                    sub1 = thisTeacherCourseList.sub1,
                    sub2 = thisTeacherCourseList.sub2,
                    sub3 = thisTeacherCourseList.sub3,
                    sub4 = thisTeacherCourseList.sub4,
                    sub5 = thisTeacherCourseList.sub5,
                    sub6 = thisTeacherCourseList.sub6,
                    sub7 = thisTeacherCourseList.sub7,
                    sub8 = thisTeacherCourseList.sub8,
                    sub9 = thisTeacherCourseList.sub9
                })
           .ReceiveJson<List<SubList>>();
                LoginOK = true;
            }
            else
            {
                LoginOK = false;
            }
            
        }

        public static async Task ConnectToRealTimeApiServer()
        {
            _connection = new HubConnectionBuilder()
                 .WithUrl(url)
                 .Build();
            await _connection.StartAsync();


            _connection.Closed += async (s) =>
            {
                await _connection.StartAsync();
            };


            _connection.On<int,string,string,int>("CallTeacherForTuition", async (teacherID, tuitionID, name, studentID) =>
            {
                if(teacherID == StaticPageForPassingData.thisTeacher.teacherID)
                {
                    CustomEventArgs thisargs = new CustomEventArgs();
                    thisargs.name = name;
                    thisargs.tuitionLogID = tuitionID;
                    regEvent.OnCall(thisargs);
                }
                
            });

        }
    }
}

