﻿using CinemaTicketAutomation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketAutomation.Models
{
    public class Movie : BaseClass
    {
        public Movie()
        {
            SetDefaultSessions();
        }

        public string picturePath { get; set; }
        public int minute { get; set; }
        public decimal price { get; set; }
        public Category category { get; set; }
        public List<Session> sessions { get; set; }

        private void SetDefaultSessions()
        {
            sessions = new List<Session>();
            DateTime currentDate = DateTime.Now;
            TimeSpan timeSpan = new TimeSpan(10, 30, 0);
            for (int i = 0; i < 3; i++)
            {
                currentDate = currentDate.Date + timeSpan;
                for (int k = 0; k < 3; k++)
                {
                    Session session = new Session();
                    session.date = currentDate.ToShortDateString();
                    session.time = currentDate.ToShortTimeString();
                    sessions.Add(session);
                }
            }
            
        }

    }
}