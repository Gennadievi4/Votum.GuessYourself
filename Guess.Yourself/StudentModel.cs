﻿using RLib;
using RLib.Remotes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace Guess.Yourself
{
    public class StudentModel : NotifyPropertyChanged
    {
        private string nameOfTheStudentsTextFile;
        public string NameOfTheStudentsTextFile
        {
            get => nameOfTheStudentsTextFile;
            set
            {
                if (nameOfTheStudentsTextFile == value) return;
                nameOfTheStudentsTextFile = value;
                OnPropertyChanged();
            }
        }

        private string character;
        public string Character
        {
            get => character;
            set
            {
                if (character == value) return;
                character = value;
                OnPropertyChanged();
            }
        }
        public bool IsAccess { get; private set; } = true;
        public List<string> textString { get; set; } = new List<string>();

        private object txtString;
        public object TextString
        {
            get => txtString;
            set
            {
                if (txtString == value) return;
                txtString = value;
                OnPropertyChanged();
            }
        }
        public TRemotePacket remotePacket { get; set; } = new TRemotePacket();
        //THIDMessageID

        //SendbackCommand sendback = new SendbackCommand(int RemoteCommand.CMD_DISPLAY_LOGO);
        private DateTime? StopWatch;
        //DateTime start = DateTime.Now;
        //public DateTime? Time => StopWatch;
        public DateTime? Time
        {
            get => StopWatch;
            set
            {
                if (StopWatch == value) return;
                StopWatch = value;
                OnPropertyChanged();
            }
        }

        //public string TimeStr => StopWatch.Hour == 0 ? 
        //    StopWatch.Equals(new DateTime()) ? 
        //    string.Empty : 
        //    StopWatch.ToString("mm : ss") : 
        //    StopWatch.ToString("hh : mm : ss");


        //public string RatingStr
        //{
        //    set
        //    {
        //        if (rating == default)
        //            rating.ToString();
        //    }
        //}
        private int? rating;
        public int? Rating
        {
            set
            {
                if (rating == value) return;
                rating = value;
                OnPropertyChanged();
            }
            get
            {
                //return rating;
                if (rating == 0) return null;
                else return rating;
            }
        }

        private string question;
        public string Question
        {
            get => question;
            set
            {
                if (question == value) return;
                question = value;
                OnPropertyChanged();
            }
        }

        private int receiverId;
        public int ReceiverId
        {
            get => receiverId;
            set
            {
                if (receiverId == value) return;
                receiverId = value;
            }
        }
        private ushort? remoteId;
        public ushort? RemoteId
        {
            get => remoteId;
            set
            {
                if (remoteId == value) return;
                remoteId = value;
                OnPropertyChanged();
            }
        }

        #region Цвета
        public ObservableCollection<AnswerLog> Questions { get; set; } = new ObservableCollection<AnswerLog>();

        public static SolidColorBrush defaultColor = new SolidColorBrush(Colors.Black);
        public SolidColorBrush AnswerColor { get; set; } = defaultColor;

        private AnswerType userAnswer;
        public AnswerType UserAnswer
        {
            get => userAnswer;
            set
            {
                userAnswer = value;
                //AnswerColor = ChangeColor(value);

                //OnPropertyChanged(nameof(AnswerColor));
                OnPropertyChanged();
            }
        }
        #endregion
        public StudentModel(int remoteId, int receiverId)
        {
            RemoteId = (ushort)remoteId;
            ReceiverId = receiverId;
        }
        
        public void QuestionsAdd(string text, AnswerType answerType)
        {
            Questions.Add(new AnswerLog { Question = $"{Questions.Count + 1}. {text}", UserAnswer = answerType });
        }
        public void UpTime()
        {
            //TimeSpan time = (DateTime.Now - start).Duration();
            //long? dt = DateTime.Now.Ticks - StopWatch?.Ticks;
            //DateTime? now = DateTime.Now;
            //TimeSpan? elapsed = new TimeSpan(0, 0, 1);
            if (StopWatch is null) StopWatch = new DateTime();
            else StopWatch = ((DateTime)StopWatch).AddSeconds(1);
            OnPropertyChanged(nameof(Time));
        }
        public StudentModel()
        {
            SaveTextCommand  = new RelayCommand<StudentModel>(OnSaveTextCommandExecute, CanSaveTextCommandExecute);
        }
        public ICommand SaveTextCommand { get; }

        private bool CanSaveTextCommandExecute(StudentModel student) => true;
        private void OnSaveTextCommandExecute(StudentModel student)
        {
            if (MainWindowViewModel.DialogService.SaveDialog() == true)
            {
                MainWindowViewModel.FileService.Save(MainWindowViewModel.DialogService.FilePath, Questions);
            }
        }

        //private ICommand saveTextCommand;
        //public ICommand SaveTextCommand => saveTextCommand ?? (saveTextCommand = new RelayCommand<StudentModel>(student =>
        //{
        //    if (MainWindowViewModel.DialogService.SaveDialog())
        //    {
        //        MainWindowViewModel.FileService.Save(MainWindowViewModel.DialogService.FilePath, Questions);
        //    }
        //}));

        //SolidColorBrush ChangeColor(AnswerType answer)
        //{
        //    switch (answer)
        //    {
        //        case AnswerType.NotGuessed:
        //            return new SolidColorBrush(Colors.Transparent);
        //        case AnswerType.Correct:
        //            return new SolidColorBrush(Colors.Green);
        //        case AnswerType.NotCorrect:
        //            return new SolidColorBrush(Colors.Red);
        //        case AnswerType.DontKnow:
        //            return new SolidColorBrush(Colors.Yellow);
        //        case AnswerType.NotSet:
        //            return defaultColor;
        //        default:
        //            throw new Exception("Не выбран цвет!");
        //    }
        //}
    }
}
