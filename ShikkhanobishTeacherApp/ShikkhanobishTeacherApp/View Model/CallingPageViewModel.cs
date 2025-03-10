﻿using Flurl.Http;
using ShikkhanobishTeacherApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShikkhanobishTeacherApp.View_Model
{
    public class CallingPageViewModel : BaseViewMode, INotifyPropertyChanged   
    {
        List<OnCallingmsg> thisMockList = new List<OnCallingmsg>();
        string thistuitionLog { get; set; }
        CostClass Allcost { get; set; }
        List<Subject> allSubject { get; set; }
        public CallingPageViewModel(string _tuitionLogID)
        {
            thistuitionLog = _tuitionLogID;
            GetThisTuition();
        }
        public async Task GetThisTuition()
        {
            await GetAllCost();
            await GetAllSubJect();
            var thislog = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getTuiTionLogWithID".PostUrlEncodedAsync(new { tuitionLogID = thistuitionLog}).ReceiveJson<TuiTionLog>();
            subjectName = thislog.subjectName;
            chapterName = thislog.chapterName;
            description = thislog.description;         
            foreach(var sub in allSubject)
            {
                if(sub.subjectID == thislog.subjectID)
                {
                    if(thislog.subjectID == 101)
                    {
                        cost = Allcost.SchoolCost;
                    }
                    else if (thislog.subjectID == 102)
                    {
                        cost = Allcost.CollegeCost;
                    }
                }
            }
            
            studntName = thislog.studentName;
            teacherName = StaticPageForPassingData.thisTeacher.name;
        }
        private void PerformsendSms()
        {
            OnCallingmsg thisSms = new OnCallingmsg();
            thisSms.msg = smstxt;
            thisSms.name = StaticPageForPassingData.thisTeacher.name;
            thisSms.lblColor = System.Drawing.Color.FromArgb(58, 17, 160);
            thisSms.date = DateTime.Now.ToString("hh:mm tt");
            thisSms.isOtherUser = false;
            thisSms.isThisUser = true;
            thisMockList.Add(thisSms);
            RefreshMsg();
        }
        public void RefreshMsg()
        {
            smsList = new List<OnCallingmsg>();
            smsList = thisMockList;
        }
        public async Task GetAllCost()
        {
            Allcost = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/GetCost".GetJsonAsync<CostClass>();
        }
        public async Task GetAllSubJect()
        {
            allSubject = await "https://api.shikkhanobish.com/api/ShikkhanobishLogin/getSubject".GetJsonAsync<List<Subject>>();
        }
        private string subjectName1;

        public string subjectName { get => subjectName1; set => SetProperty(ref subjectName1, value); }

        private string chapterName1;

        public string chapterName { get => chapterName1; set => SetProperty(ref chapterName1, value); }

        private string description1;

        public string description { get => description1; set => SetProperty(ref description1, value); }

        private int cost1;

        public int cost { get => cost1; set => SetProperty(ref cost1, value); }

        private string studntName1;

        public string studntName { get => studntName1; set => SetProperty(ref studntName1, value); }

        private string teacherName1;

        public string teacherName { get => teacherName1; set => SetProperty(ref teacherName1, value); }
        private List<OnCallingmsg> smsList1;

        public List<OnCallingmsg> smsList { get => smsList1; set => SetProperty(ref smsList1, value); }

        private string smstxt1;

        public string smstxt { get => smstxt1; set => SetProperty(ref smstxt1, value); }

        private Command sendSms1;

        public ICommand sendSms
        {
            get
            {
                if (sendSms1 == null)
                {
                    sendSms1 = new Command(PerformsendSms);
                }

                return sendSms1;
            }
        }
    }
}
